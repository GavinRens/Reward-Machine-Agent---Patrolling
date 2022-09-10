using UnityEngine;


/// <summary>
/// Use this code to validate that the implemented transition function is correct. Whenever "mass != 1f" (see below) is true, the transition function is ill-dfined.
/// </summary>
public class ModelValidation : MonoBehaviour
{
    NMRDP_Agent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = new Patrolling_NMRDP_Agent();

        Debug.Log("-------- START TRANS FUNC VALIDATION --------");

        foreach(State s in Agent.States)
            foreach(Action a in agent.Actions)
            {
                float mass = 0;
                foreach (State ss in Agent.States)
                {
                    mass += agent.TransitionFunction(s, a, ss);
                }
                if(mass != 1f)
                    Debug.Log("s, a, mass :: " + s.waypoint + ", " + a + ", " + mass);
            }

        Debug.Log("-------- END TRANS FUNC VALIDATION --------");

        Debug.Log("-------- START OBS FUNC VALIDATION --------");

        foreach (State s in Agent.States)
        {
            foreach (Action a in agent.Actions)
            {
                double mass = 0;
                foreach (Observation z in Agent.Observations)
                    mass += agent.ObservationFunction(z, a, s);
                if (mass > 0.9999999)
                    mass = 1.0d;
                if (mass != 1.0d)
                {
                    print("a, s, mass :: " + a + ", " + s.waypoint + ", " + mass);
                }
            }
        }

        Debug.Log("-------- END OBS FUNC VALIDATION --------");

    }
}
