using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private WaypointPath path;
    [SerializeField] private float speed;

    private int targetIndex;        //Next waypoint to move to
    private Transform previousWaypoint;             //location of waypoints
    private Transform targetWaypoint;

    private float timeToWaypoint;       //time for reach other waypoint
    private float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        NextWaypoint();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        elapsedTime += Time.deltaTime;

        float elaspedPercentage = elapsedTime / timeToWaypoint;
        elaspedPercentage = Mathf.SmoothStep(0, 1, elaspedPercentage);      //smooths movement so its slower at beginning and the end

        //changes platforms position based on distance of journey elapsed 
        transform.position = Vector3.Lerp(previousWaypoint.position, targetWaypoint.position, elaspedPercentage);
        transform.rotation = Quaternion.Lerp(previousWaypoint.rotation, targetWaypoint.rotation, elaspedPercentage);

        if (elaspedPercentage >= 1){        //platform has reached it target waypoint
            NextWaypoint();
        }
    }

    private void NextWaypoint(){                                    //changes target to next wapoint in path when called
        previousWaypoint = path.GetWaypoint(targetIndex);
        targetIndex = path.GetNextIndex(targetIndex);
        targetWaypoint = path.GetWaypoint(targetIndex);

        elapsedTime = 0;

        float distanceToWaypoint = Vector3.Distance(previousWaypoint.position, targetWaypoint.position);        //distance to waypoint
        timeToWaypoint = distanceToWaypoint / speed;        //calculate speed
    }

    private void OnTriggerEnter(Collider other){            //sets the player to be a child of the moving platform, so they move with it
        other.transform.SetParent(transform);
        Debug.Log("platform trigger");
    }

    private void OnTriggerExit(Collider other){
        other.transform.SetParent(null);
    }
}
