using TMPro;
using UnityEngine;
using UnityEngine.AI;


public class AgentController : MonoBehaviour
{
    public GameObject[] waypoints;
    public GameObject actionStatus;
    public GameObject routeToTake;
    public Patrolling_NMRDP_Agent nmrdpAgent;

    TextMeshPro actionStatusText;
    TextMeshPro routeToTakeText;
    enum Phase { Planning, Execution, Updating }
    Phase phase;
    NavMeshAgent navMeshAgent;
    bool alreadyPlanning;
    bool alreadyExecuting;
    bool waitingToGetPath;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        navMeshAgent.stoppingDistance = 1.9f;

        nmrdpAgent = new Patrolling_NMRDP_Agent();

        phase = Phase.Planning;

        alreadyPlanning = false;
        alreadyExecuting = false;
        waitingToGetPath = false;

        actionStatusText = actionStatus.GetComponent<TextMeshPro>();
        routeToTakeText = routeToTake.GetComponent<TextMeshPro>();

        Time.timeScale = 4f;
    }


    void LateUpdate()
    {
        if (phase == Phase.Planning)
        {
            //Debug.Log("----------------------------------");
            //Debug.Log("Entered Planning Phase");
            //Debug.Log("CurrentState: " + nmrdpAgent.CurrentState.name);
            //Debug.Log("waitingToGetPath: " + waitingToGetPath);
            //Debug.Log("alreadyPlanning: " + alreadyPlanning);

            if (!waitingToGetPath && !alreadyPlanning)
            {
                alreadyPlanning = true;
                (State s, float p) = nmrdpAgent.SampleFromBelief(nmrdpAgent.CurrentBelief);
                nmrdpAgent.CurrentAction = nmrdpAgent.SelectAction(s);
                //if(nmrdpAgent.CurrentAction != null)
                actionStatusText.text = nmrdpAgent.CurrentAction.ToString();
                Debug.Log("CurrentAction: " + nmrdpAgent.CurrentAction);

                //  This switch applies only to navigation actions
                switch (nmrdpAgent.CurrentAction)
                {
                    case Action.GotoOffice:
                        navMeshAgent.SetDestination(waypoints[0].transform.position);
                        waitingToGetPath = true;  // computation of the path might take longer than one frame
                        break;
                    case Action.GotoWP1:
                        navMeshAgent.SetDestination(waypoints[1].transform.position);
                        waitingToGetPath = true;  // computation of the path might take longer than one frame
                        break;
                    case Action.GotoWP2:
                        navMeshAgent.SetDestination(waypoints[2].transform.position);
                        waitingToGetPath = true;  // computation of the path might take longer than one frame
                        break;
                    case Action.GotoWP3:
                        navMeshAgent.SetDestination(waypoints[3].transform.position);
                        waitingToGetPath = true;  // computation of the path might take longer than one frame
                        break;
                    case Action.GotoWP4:
                        navMeshAgent.SetDestination(waypoints[4].transform.position);
                        waitingToGetPath = true;  // computation of the path might take longer than one frame
                        break;
                    case Action.GotoWP5:
                        navMeshAgent.SetDestination(waypoints[5].transform.position);
                        waitingToGetPath = true;  // computation of the path might take longer than one frame
                        break;
                    case Action.GotoWP6:
                        navMeshAgent.SetDestination(waypoints[6].transform.position);
                        waitingToGetPath = true;  // computation of the path might take longer than one frame
                        break;
                    case Action.GotoWP7:
                        navMeshAgent.SetDestination(waypoints[7].transform.position);
                        waitingToGetPath = true;  // computation of the path might take longer than one frame
                        break;
                    case Action.GotoWP8:
                        navMeshAgent.SetDestination(waypoints[8].transform.position);
                        waitingToGetPath = true;  // computation of the path might take longer than one frame
                        break;
                    case Action.GotoWP9:
                        navMeshAgent.SetDestination(waypoints[9].transform.position);
                        waitingToGetPath = true;  // computation of the path might take longer than one frame
                        break;

                }
                alreadyPlanning = false;
            }

            if (nmrdpAgent.isNavigationAction(nmrdpAgent.CurrentAction))
            {
                if (navMeshAgent.hasPath)
                {
                    waitingToGetPath = false;
                    phase = Phase.Execution;
                    //Debug.Log("----------------------------------");
                    //Debug.Log("Entered Execution Phase");
                }
            }
            else
            {
                phase = Phase.Execution;
                //Debug.Log("----------------------------------");
                //Debug.Log("Entered Execution Phase");
            }
        }

        if (phase == Phase.Execution)
        {
            if (nmrdpAgent.isNavigationAction(nmrdpAgent.CurrentAction))
            {
                //Debug.Log("----------------------------------");
                //Debug.Log("Entered Execution Phase");
                //Debug.Log("remainingDistance: " + navMeshAgent.remainingDistance);
                //Debug.Log("hasPath: " + navMeshAgent.hasPath);

                if (navMeshAgent.remainingDistance < Parameters.AT_TARGET_DISTANCE)
                {
                    navMeshAgent.ResetPath();
                    phase = Phase.Updating;
                }
            }
            else if (!alreadyExecuting)
            {
                switch (nmrdpAgent.CurrentAction)
                {
                    case Action.GetRoute:
                        if (nmrdpAgent.CurrentState.waypoint == 0)
                            Debug.Log("Getting which route to take");
                        break;
                        //case Action.No_Op:
                        //    Debug.Log("Doing nothing");
                        //    break;
                }
                alreadyExecuting = true;
                Invoke("ChangePhaseToUpdateAfterSeconds", 2f);
            }
        }

        if (phase == Phase.Updating)
        {
            //Debug.Log("----------------------------------");
            //Debug.Log("Entered Updating Phase");
            State nextState = Environment.GetRealNextState(nmrdpAgent.CurrentState, nmrdpAgent.CurrentAction);
            Observation obs = Environment.GetRealObservation(nmrdpAgent.CurrentAction, nextState);
            if(nmrdpAgent.CurrentAction == Action.GetRoute)
                routeToTakeText.text = "Do " + obs.ToString();
            nmrdpAgent.RewardMachine.AdvanceActiveNode(obs);
            nmrdpAgent.CurrentState = nextState;
            nmrdpAgent.CurrentBelief = nmrdpAgent.GetNextBelief(obs, nmrdpAgent.CurrentAction, nmrdpAgent.CurrentBelief);
            phase = Phase.Planning;
        }
    }

    void ChangePhaseToUpdateAfterSeconds()
    {
        phase = Phase.Updating;
        alreadyExecuting = false;
    }
}
