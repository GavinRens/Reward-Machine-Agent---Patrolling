using UnityEngine;
using System.Collections.Generic;


public class Environment : MonoBehaviour
{
    static System.Random rand;
    public static List<Observation> the3Routes;


    void Start()
    {
        GameObject agentGO = GameObject.FindGameObjectWithTag("agent");
        rand = new System.Random();
        the3Routes = new List<Observation> { Observation.Route_A, Observation.Route_B, Observation.Route_C };
    }


    /// <summary>
    /// The state the agent will end up in if it executes the action in the current state
    /// This is the `ground truth', not a model of what is expected <see cref="Agent.GetNextState(Action, State)"/>
    /// </summary>
    /// <param name="action">An action</param>
    /// <param name="currentState">An environment state</param>
    /// <returns>A successor state</returns>
    public static State GetRealNextState(State currentState, Action action)
    {
        if (currentState.waypoint == 0)  // in office
        {
            if (action == Action.GotoWP1)
                foreach (State s in Patrolling_NMRDP_Agent.States)
                    if (s.waypoint == 1)
                        return s;
            if (action == Action.GotoWP4)
                foreach (State s in Patrolling_NMRDP_Agent.States)
                    if (s.waypoint == 4)
                        return s;
            if (action == Action.GotoWP7)
                foreach (State s in Patrolling_NMRDP_Agent.States)
                    if (s.waypoint == 7)
                        return s;
        }

        if (currentState.waypoint == 1)
        {
            if (action == Action.GotoWP2)
                foreach (State s in Patrolling_NMRDP_Agent.States)
                    if (s.waypoint == 2)
                        return s;
        }

        if (currentState.waypoint == 2)
        {
            if (action == Action.GotoWP3)
                foreach (State s in Patrolling_NMRDP_Agent.States)
                    if (s.waypoint == 3)
                        return s;
        }

        if (currentState.waypoint == 3)
        {
            if (action == Action.GotoWP4)
                foreach (State s in Patrolling_NMRDP_Agent.States)
                    if (s.waypoint == 4)
                        return s;
        }

        if (currentState.waypoint == 4)
        {
            if (action == Action.GotoWP5)
                foreach (State s in Patrolling_NMRDP_Agent.States)
                    if (s.waypoint == 5)
                        return s;
            if (action == Action.GotoOffice)
                foreach (State s in Patrolling_NMRDP_Agent.States)
                    if (s.waypoint == 0)
                        return s;
        }

        if (currentState.waypoint == 5)
        {
            if (action == Action.GotoWP6)
                foreach (State s in Patrolling_NMRDP_Agent.States)
                    if (s.waypoint == 6)
                        return s;
        }

        if (currentState.waypoint == 6)
        {
            if (action == Action.GotoOffice)
                foreach (State s in Patrolling_NMRDP_Agent.States)
                    if (s.waypoint == 0)
                        return s;
        }

        if (currentState.waypoint == 7)
        {
            if (action == Action.GotoWP8)
                foreach (State s in Patrolling_NMRDP_Agent.States)
                    if (s.waypoint == 8)
                        return s;
        }

        if (currentState.waypoint == 8)
        {
            if (action == Action.GotoWP9)
                foreach (State s in Patrolling_NMRDP_Agent.States)
                    if (s.waypoint == 9)
                        return s;
        }

        if (currentState.waypoint == 9)
        {
            if (action == Action.GotoOffice)
                foreach (State s in Patrolling_NMRDP_Agent.States)
                    if (s.waypoint == 0)
                        return s;
        }

        return currentState;  // including if action == Action.GetRoute or action == Action.No_Op
    }


    /// <summary>
    /// The observation the agent will perceive in the state it reaches by executing the action
    /// This is the `ground truth', not a model of what is expected <see cref="NMRDP_Interface.GetObservation(Action, State)"/>
    /// </summary>
    /// <param name="a">An action</param>
    /// <param name="s">An environment state</param>
    /// <returns>An observation</returns>
    public static Observation GetRealObservation(Action a, State s)
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
                return the3Routes[index];
            }

        if (a == Action.No_Op)
            return Observation.Null;

        return Observation.Null;  // all other possibilities produce the null observation
    }
}

