using UnityEngine;
using UnityEngine.AI;
public class AgentController : MonoBehaviour {
    public enum AgentState {
        Idle = 0,
        Patrolling,
        Chasing
    }

    public AgentState state;
    public Transform[] waypoints;
    private NavMeshAgent navMeshAgent;
    private Animator animController;
    private int speedHashId;
    private int waypointIndex;
    [SerializeField] private int distanceToStartHeadingToNextWayPoint = 1;
    [SerializeField] private Transform target;
    [SerializeField] private int distanceToStartChasingTarget = 15;

    void Awake() {
        speedHashId = Animator.StringToHash ("walkingSpeed");
        navMeshAgent = GetComponent<NavMeshAgent>();
        animController = GetComponent<Animator>();
        
        if (waypoints.Length == 0) {
            Debug.LogError("Error: list of waypoints is empty.");
        }
    }

    void Update() {
        if (state == AgentState.Idle) {
            Idle ();
        } 
        else if (state == AgentState.Patrolling) {
            Patrol ();
        } 
        else {
            Chase ();
        }
    }

    void Chase () {
        animController.SetFloat (speedHashId, 1.0f);
        navMeshAgent.isStopped = false;
        navMeshAgent.stoppingDistance = 2;

        if(navMeshAgent.remainingDistance < distanceToStartChasingTarget){
            navMeshAgent.SetDestination(target.position);
        }
        else if(navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance){
            animController.SetFloat (speedHashId, 0.0f);
        } 
        else {
            Idle();
        }

    }

    void Idle() {
        animController.SetFloat (speedHashId, 0.0f);
        navMeshAgent.isStopped = true;
    }

    void Patrol() {
        animController.SetFloat (speedHashId, 1.0f);
        navMeshAgent.isStopped = false;
        navMeshAgent.stoppingDistance = 0;

        if(navMeshAgent.remainingDistance < distanceToStartHeadingToNextWayPoint){
            waypointIndex = (waypointIndex + 1) % waypoints.Length;
            navMeshAgent.SetDestination(waypoints[waypointIndex].position);
        }
    }
}
