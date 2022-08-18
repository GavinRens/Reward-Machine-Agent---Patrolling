using System.Collections.Generic;
//using TreasureHunting;
//using Patrolling;


public interface NMRDP_Interface
{
    static HashSet<Action> Actions { get; }  // Define enum Action in the code (and same namespace) instantiating this NMRDP
    static List<Observation> Observations { get; }  // Define enum Observation in the code (and same namespace) instantiating this NMRDP
    static List<State> States { get; }  // Define class State in the code (and same namespace) instantiating this NMRDP
    RewardMachine RewardMachine { get; }

    // Define the reward machine
    RewardMachine DefineRewardMachine();

    // Define the transition function; the probability that an action performed in 'from' will end up in 'to'
    float TransitionFunction(State from, Action a, State to);

    // Define the (possibly non-deterministic or probabilistic) function that returns an observation given an action and a state reached
    public Observation GetObservation(Action a, State s);

    // Calculate immediate reward, given the reward machine and the current action taken in the agent's current state
    public float ImmediateReward(Action a, State s);
}







