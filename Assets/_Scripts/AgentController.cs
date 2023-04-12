using System.Collections;
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
    private int attackingHashId;
    private int waypointIndex;

    [SerializeField] private int distanceToStartHeadingToNextWayPoint = 1;
    [SerializeField] private Transform target;
    [SerializeField] private int distanceToStartChasingTarget = 5;
    [SerializeField] private float distanceToStartAttackingTarget = 3;
    public float timePursuingTarget = 5;
    public float rotationSpeed = 5.0f;

    [SerializeField] AudioSource punchSound;
    

    void Awake() {
        speedHashId = Animator.StringToHash ("walkingSpeed");
        attackingHashId = Animator.StringToHash ("attack");
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

    private float oldRemainingDistance = float.PositiveInfinity;
    private float RemainingDistance()
	{
		if(navMeshAgent.pathPending)
		{
			return oldRemainingDistance;
		}
		else if (!navMeshAgent.hasPath)
		{
			oldRemainingDistance = float.PositiveInfinity;
			return oldRemainingDistance;
		}
		else
		{
			float distance = 0;
			Vector3[] corners = navMeshAgent.path.corners;
			for (int i = 0; i < corners.Length - 1; i++)
			{
				distance += Vector3.Distance(corners[i],corners[i + 1]);
			}
			oldRemainingDistance = distance;
			return distance;
		}
	}

    private bool TargetWithinAngle (float angle)
	{
		Vector3 planarDifference = target.position - transform.position;
		planarDifference.y = 0;
		float actualAngle = Vector3.Angle(planarDifference, transform.forward);
		return actualAngle <= angle;
	}

    private bool IsAwareOfTarget()
	{
		return RemainingDistance() <= distanceToStartChasingTarget
			   && TargetWithinAngle(90);
	}

    private float timeSinceLastSeenTarget = float.PositiveInfinity;
    void Chase () {
        navMeshAgent.stoppingDistance = 2.1f;
        //Debug.Log(distanceToStartChasingTarget + "---- " + RemainingDistance());
        Attack();

        navMeshAgent.SetDestination (target.position);
		
        timeSinceLastSeenTarget += Time.deltaTime;
		if (IsAwareOfTarget()){
			timeSinceLastSeenTarget = 0;
        }
        

		if (RemainingDistance() <= navMeshAgent.stoppingDistance)
		{
			navMeshAgent.isStopped = true;
			animController.SetFloat (speedHashId, 0.0f);
			RoateTowardsTarget();
		}
		else 
		{
			navMeshAgent.isStopped = false;
			animController.SetFloat (speedHashId, 1.0f);
		}
    }

    void RoateTowardsTarget()
	{
		Vector3 planarDifference = (target.position - transform.position);
		planarDifference.y 	     = 0;
		Quaternion targetRotation = Quaternion.LookRotation(planarDifference.normalized);
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
	}

    private bool ShouldAttack()
	{
		return RemainingDistance() <= distanceToStartAttackingTarget
			&& TargetWithinAngle(45);
	}

	void Attack ()
	{
		if (ShouldAttack())
		{
            punchSound.Play();
            animController.SetTrigger(attackingHashId);
            //StartCoroutine(attackPunch());
		}
	}


    void Idle() {
        animController.SetFloat (speedHashId, 0.0f);
        navMeshAgent.isStopped = true;

        StartCoroutine(waitToPatrol());
    }

    void Patrol() {
        animController.SetFloat (speedHashId, 1.0f);
        navMeshAgent.isStopped = false;
        navMeshAgent.stoppingDistance = 0;

        if(navMeshAgent.remainingDistance < distanceToStartHeadingToNextWayPoint){
            waypointIndex = (waypointIndex + 1) % waypoints.Length;
            navMeshAgent.SetDestination(waypoints[waypointIndex].position);
            //Debug.Log(navMeshAgent.remainingDistance + "---- ");
        }

        int chance = Random.Range(0,1000);
        if (chance < 1){
            state = AgentState.Idle;
        }

    }

    IEnumerator waitToPatrol(){
        yield return new WaitForSeconds(5);
        state = AgentState.Patrolling;
    }

    IEnumerator attackPunch(){
        Debug.Log("attack couroutine");
        yield return new WaitForSeconds(2);
        punchSound.Play();
        animController.SetTrigger(attackingHashId);
    }


    public void setChase(){
        state = AgentState.Chasing;
        Debug.Log("chasing");
    }

    public void setPatrol(){
        state = AgentState.Patrolling;
        StartCoroutine(waitToPatrol());
        Debug.Log("patrol");
    }
}
