using System.Collections.Generic;


public abstract class NMRDP_Agent : Agent, PO_Agent_Interface, NMRDP_Interface, Planner_Interface
{
    RewardMachine rewardMachine;
    System.Random rand;
    static int B;
    Dictionary<State, float> currentBelief;


    public NMRDP_Agent() : base()
    {
        rewardMachine = DefineRewardMachine();
        rand = new System.Random();
        B = States.Count / Parameters.BELIEF_SIZE_FACTOR;
    }

    
    public rmNode GetNextActiveRMNode(Observation observation, rmNode currentActiveNode)
    {
        foreach (rmEdge e in currentActiveNode.edges)
            if (e.observation == observation)
            {
                //UnityEngine.Debug.Log("Next active node: " + e.end.name);
                return e.end;
            }

        //UnityEngine.Debug.Log("Active node not advanced !!");
        return currentActiveNode;  // If observation does not point to another node, then by default, the active node does not chnage
    }


    // For Agent (other methods to be implemented in final agent instance)

    // The functionality of this method is defined by SampleNextState()
    public override State GetNextState(Action action, State state)
    {
        (State ss, float p) = SampleNextState(state, action);
        return ss;
    }


    // For PO_Agent_Interface

    public Dictionary<State, float> CurrentBelief { get { return currentBelief; } set { currentBelief = value; } }

    public abstract float ObservationFunction(Observation z, Action a, State s);

    public Dictionary<State, float> GetNextBelief(Observation z, Action a, Dictionary<State, float> b)
    {
        /* Belief update via particle filtering */
        var bb = new Dictionary<State, float>(); // New belief to be returned
        float mass = 0;
        for (int i = 0; i < B; i++)
        {
            (State s, float p) = SampleFromBelief(b);
            (State ss, float pp) = SampleNextState(s, a);
            float ppp = (float)ObservationFunction(z, a, ss);
            float weight = p * pp * ppp;
            if (weight != 0)
                if (bb.ContainsKey(ss))
                    bb[ss] += weight;
                else
                    bb.Add(ss, weight);
            mass += weight;
        }

        if (mass == 0)  // ignore observations
        {
            foreach (KeyValuePair<State, float> kvp in b)
            {
                (State ss, float pp) = SampleNextState(kvp.Key, a);
                float weight = kvp.Value * pp;
                if (weight != 0)
                    if (bb.ContainsKey(ss))
                        bb[ss] += weight;
                    else
                        bb.Add(ss, weight);
                mass += weight;
            }
        }
        var bbb = new Dictionary<State, float>(); // New belief to be returned
        foreach (KeyValuePair<State, float> kvp in bb)
            bbb.Add(kvp.Key, kvp.Value / mass); // bb.[kvp.Key] = kvp.Value / mass;

        return bbb;
    }

    /// <summary>
    /// Sample the transision function for the successor state
    /// </summary>
    /// <param name="s">The current state</param>
    /// <param name="a">The current action</param>
    /// <returns>A possible successor state</returns>
    (State, float) SampleNextState(State s, Action a)
    {
        float r = (float)rand.NextDouble();
        float mass = 0;
        foreach (State ss in States)
        {
            mass += TransitionFunction(s, a, ss);
            if (r <= mass)
                return (ss, TransitionFunction(s, a, ss));
        }
        return (new State(0), 0f);
    }

    /// <summary>
    /// Sample a belief for a likely state
    /// </summary>
    /// <param name="b">A belief - a probability distribution</param>
    /// <returns>A (likely) state</returns>
    public (State s, float p) SampleFromBelief(Dictionary<State, float> b)
    {
        float mass = 0;
        float r = (float)rand.NextDouble();  // shoulf r be > 0?
        foreach (KeyValuePair<State, float> kvp in b)
        {
            mass += kvp.Value;
            if (r <= mass)
                return (kvp.Key, kvp.Value);
        }
        return (new State(0), 0f);
    }

    public abstract void InitializeAgentBelief();


    // For NMRDP_Interface

    public RewardMachine RewardMachine { get { return rewardMachine; } }

    public abstract RewardMachine DefineRewardMachine();

    public abstract float TransitionFunction(State stateFrom, Action action, State stateTo);

    public abstract Observation GetObservation(Action a, State s);

    public float ImmediateReward(Action action, State state)
    {// Note that state is the state reached via action
        Observation obsrv = GetObservation(action, state);
        foreach (rmEdge e in RewardMachine.ActiveNode.edges)
            if (e.observation == obsrv)
                return e.reward;

        //UnityEngine.Debug.Log(string.Format("No edge with an observation matching LabelingFunction(" + action + ", " + state.number + ")"));
        return 0;
    }

    // Overloaded for use in MCTS algorithm
    public float ImmediateReward(Action action, State state, rmNode activeNode)
    {// Note that state is the state reached via action
        Observation obsrv = GetObservation(action, state);
        foreach (rmEdge e in activeNode.edges)
            if (e.observation == obsrv)
                return e.reward;

        //UnityEngine.Debug.Log(string.Format("No edge with an observation matching LabelingFunction(" + action + ", " + state.number + ")"));
        return 0;
    }


    // For Planner_Interface

    public abstract Action SelectAction(State currentState);
    
}



