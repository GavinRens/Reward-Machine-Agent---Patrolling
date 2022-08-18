using System.Collections.Generic;

// Belief-Based Planner Interface
public interface BB_Planner_Interface
{
    /// <summary>
    /// The interface for a planning algorithm
    /// </summary>
    /// <param name="currentBelief">The belief-state in which the agent is currently</param>
    /// <param name="agent">An optional reference to the agent</param>
    /// <returns>The action that should be executed in the agent's current belief-state</returns>
    public Action SelectAction(Dictionary<State, float> currentBelief, Agent agent = null);
}
