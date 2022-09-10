using System;
using System.Collections.Generic;


public class Patrolling_NMRDP_Agent : NMRDP_Agent
{
    MCTS mctsPlanner;
    Random rand;
    

    public Patrolling_NMRDP_Agent() : base()
    {
        InitializeAgentState();
        InitializeAgentBelief();
        mctsPlanner = new MCTS(this);
        rand = new Random();
    }


    public override RewardMachine DefineRewardMachine()
    {
        RewardMachine rm = new();

        var office = new rmNode("office");
        rm.AddNode(office);
        var doRouteA = new rmNode("doRouteA");
        rm.AddNode(doRouteA);
        var doRouteB = new rmNode("doRouteB");
        rm.AddNode(doRouteB);
        var doRouteC = new rmNode("doRouteC");
        rm.AddNode(doRouteC);
        var waypoint1 = new rmNode("waypoint1");
        rm.AddNode(waypoint1);
        var waypoint2 = new rmNode("waypoint2");
        rm.AddNode(waypoint2);
        var waypoint3 = new rmNode("waypoint3");
        rm.AddNode(waypoint3);
        var waypoint4a = new rmNode("waypoint4a");
        rm.AddNode(waypoint4a);
        var waypoint4b = new rmNode("waypoint4b");
        rm.AddNode(waypoint4b);
        var waypoint5 = new rmNode("waypoint5");
        rm.AddNode(waypoint5);
        var waypoint6 = new rmNode("waypoint6");
        rm.AddNode(waypoint6);
        var waypoint7 = new rmNode("waypoint7");
        rm.AddNode(waypoint7);
        var waypoint8 = new rmNode("waypoint8");
        rm.AddNode(waypoint8);
        var waypoint9 = new rmNode("waypoint9");
        rm.AddNode(waypoint9);

        rm.ActiveNode = office;

        rm.AddEdge(office, doRouteA, Observation.Route_A, 1f);
        rm.AddEdge(office, doRouteB, Observation.Route_B, 1f);
        rm.AddEdge(office, doRouteC, Observation.Route_C, 1f);
        rm.AddEdge(doRouteA, waypoint1, Observation.AtWP1, 1f);
        rm.AddEdge(doRouteB, waypoint4a, Observation.AtWP4, 1f);
        rm.AddEdge(doRouteC, waypoint7, Observation.AtWP7, 1f);
        rm.AddEdge(waypoint1, waypoint2, Observation.AtWP2, 1f);
        rm.AddEdge(waypoint2, waypoint3, Observation.AtWP3, 1f);
        rm.AddEdge(waypoint3, waypoint4b, Observation.AtWP4, 1f);
        rm.AddEdge(waypoint4b, office, Observation.AtOffice, 1f);
        rm.AddEdge(waypoint4a, waypoint5, Observation.AtWP5, 1f);
        rm.AddEdge(waypoint5, waypoint6, Observation.AtWP6, 1f);
        rm.AddEdge(waypoint6, office, Observation.AtOffice, 1f);
        rm.AddEdge(waypoint7, waypoint8, Observation.AtWP8, 1f);
        rm.AddEdge(waypoint8, waypoint9, Observation.AtWP9, 1f);
        rm.AddEdge(waypoint9, office, Observation.AtOffice, 1f);
            
        return rm;
    }


    public override List<State> GenerateStates()
    {
        states = new List<State>();
        states.Add(new State(0));
        states.Add(new State(1));
        states.Add(new State(2));
        states.Add(new State(3));
        states.Add(new State(4));
        states.Add(new State(5));
        states.Add(new State(6));
        states.Add(new State(7));
        states.Add(new State(8));
        states.Add(new State(9));
        return states;
    }


    public override void InitializeAgentState()
    {
        CurrentState = States[0];
    }


    public override void InitializeAgentBelief()
    {
        CurrentBelief = new Dictionary<State, float>() { { States[0], 1f } };
    }


    public override bool HasFinished(State state)
    {
        return false;
    }


    public override float TransitionFunction(State stateFrom, Action action, State stateTo)
    {
        if (stateFrom.waypoint == 0)
        {
            if (action == Action.GotoWP1)
            {
                if (stateTo.waypoint == 1)
                    return 1f;
            }
            else if (action == Action.GotoWP4)
            {
                if (stateTo.waypoint == 4)
                    return 1f;
            }
            else if (action == Action.GotoWP7)
            {
                if (stateTo.waypoint == 7)
                    return 1f;
            }
            else if (stateTo.waypoint == 0)
                return 1f;  // for all other actions, the agent remains in same state
        }

        if (stateFrom.waypoint == 1)
        {
            if (action == Action.GotoWP2)
            {
                if (stateTo.waypoint == 2)
                    return 1f;
            }
            else if (stateTo.waypoint == 1)
                return 1f;  // for all other actions, the agent remains in same state
        }

        if (stateFrom.waypoint == 2)
        {
            if (action == Action.GotoWP3)
            {
                if (stateTo.waypoint == 3)
                    return 1f;
            }
            else if (stateTo.waypoint == 2)
                return 1f;  // for all other actions, the agent remains in same state
        }

        if (stateFrom.waypoint == 3)
        {
            if (action == Action.GotoWP4)
            {
                if (stateTo.waypoint == 4)
                    return 1f;
            }
            else if (stateTo.waypoint == 3)
                return 1f;  // for all other actions, the agent remains in same state
        }

        if (stateFrom.waypoint == 4)
        {
            if (action == Action.GotoWP5)
            {
                if (stateTo.waypoint == 5)
                    return 1f;
            }
            else if (action == Action.GotoOffice)
            {
                if (stateTo.waypoint == 0)
                    return 1f;
            }
            else if (stateTo.waypoint == 4)
                return 1f;  // for all other actions, the agent remains in same state
        }

        if (stateFrom.waypoint == 5)
        {
            if (action == Action.GotoWP6)
            {
                if (stateTo.waypoint == 6)
                    return 1f;
            }
            else if (stateTo.waypoint == 5)
                return 1f;  // for all other actions, the agent remains in same state
        }

        if (stateFrom.waypoint == 6)
        {
            if (action == Action.GotoOffice)
            {
                if (stateTo.waypoint == 0)
                    return 1f;
            }
            else if (stateTo.waypoint == 6)
                return 1f;  // for all other actions, the agent remains in same state
        }

        if (stateFrom.waypoint == 7)
        {
            if (action == Action.GotoWP8)
            {
                if (stateTo.waypoint == 8)
                    return 1f;
            }
            else if (stateTo.waypoint == 7)
                return 1f;  // for all other actions, the agent remains in same state
        }

        if (stateFrom.waypoint == 8)
        {
            if (action == Action.GotoWP9)
            {
                if (stateTo.waypoint == 9)
                    return 1f;
            }
            else if (stateTo.waypoint == 8)
                return 1f;  // for all other actions, the agent remains in same state
        }

        if (stateFrom.waypoint == 9)
        {
            if (action == Action.GotoOffice)
            {
                if (stateTo.waypoint == 0)
                    return 1f;
            }
            else if (stateTo.waypoint == 9)
                return 1f;  // for all other actions, the agent remains in same state
        }

        return 0f;
    }


    // Return the observation perceived in (next state) s after performing a
    public override Observation GetObservation(Action a, State s)
    {
        if (a == Action.GotoOffice)
            if (s.waypoint == 0)
                return Observation.AtOffice;

        if (a == Action.GotoWP1)
            if (s.waypoint == 1)
                return Observation.AtWP1;

        if (a == Action.GotoWP2)
            if (s.waypoint == 2)
                return Observation.AtWP2;

        if (a == Action.GotoWP3)
            if (s.waypoint == 3)
                return Observation.AtWP3;

        if (a == Action.GotoWP4)
            if (s.waypoint == 4)
                return Observation.AtWP4;

        if (a == Action.GotoWP5)
            if (s.waypoint == 5)
                return Observation.AtWP5;

        if (a == Action.GotoWP6)
            if (s.waypoint == 6)
                return Observation.AtWP6;

        if (a == Action.GotoWP7)
            if (s.waypoint == 7)
                return Observation.AtWP7;

        if (a == Action.GotoWP8)
            if (s.waypoint == 8)
                return Observation.AtWP8;

        if (a == Action.GotoWP9)
            if (s.waypoint == 9)
                return Observation.AtWP9;

        if (a == Action.GetRoute)
            if (s.waypoint == 0)
        {
            int index;
            index = rand.Next(3);
            return Environment.the3Routes[index];
        }
                    
        if (a == Action.No_Op)
            return Observation.Null;

        return Observation.Null;  // all other possibilities produce the null observation
    }


    public override float ObservationFunction(Observation z, Action a, State s)
    {
        if (a == Action.GotoOffice)
            if (s.waypoint == 0)
                if (z == Observation.AtOffice)
                    return 1;

        if (a == Action.GotoWP1)
            if (s.waypoint == 1)
                if (z == Observation.AtWP1)
                    return 1;

        if (a == Action.GotoWP2)
            if (s.waypoint == 2)
                if (z == Observation.AtWP2)
                    return 1;

        if (a == Action.GotoWP3)
            if (s.waypoint == 3)
                if (z == Observation.AtWP3)
                    return 1;

        if (a == Action.GotoWP4)
            if (s.waypoint == 4)
                if (z == Observation.AtWP4)
                    return 1;

        if (a == Action.GotoWP5)
            if (s.waypoint == 5)
                if (z == Observation.AtWP5)
                    return 1;

        if (a == Action.GotoWP6)
            if (s.waypoint == 6)
                if (z == Observation.AtWP6)
                    return 1;

        if (a == Action.GotoWP7)
            if (s.waypoint == 7)
                if (z == Observation.AtWP7)
                    return 1;

        if (a == Action.GotoWP8)
            if (s.waypoint == 8)
                if (z == Observation.AtWP8)
                    return 1;

        if (a == Action.GotoWP9)
            if (s.waypoint == 9)
                if (z == Observation.AtWP9)
                    return 1;

        if (a == Action.GetRoute)
            if (s.waypoint == 0)
                if (Environment.the3Routes.Contains(z))
                    return 1f / 3f;
            
        if (z == Observation.Null)
            return 1;

        return 0;  // all other possibilities are impossible
    }


    public override Action SelectAction(State currentState)
    {
        return mctsPlanner.SelectAction(currentState);
    }


    public bool isNavigationAction(Action a)
    {
        if (a == Action.GetRoute)
            return false;
        return true;  // in this environ, all actions are nav actions
    }
}
