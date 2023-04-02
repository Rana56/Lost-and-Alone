using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointPath : MonoBehaviour
{
    public Transform GetWaypoint(int waypointIndex){
        return transform.GetChild(waypointIndex);           //waypoints are children are indexed 
    }

    public int GetNextIndex(int currentWaypointIndex){
        int nextWaypointIndex = currentWaypointIndex + 1;  

        if (nextWaypointIndex == transform.childCount){                 //checks if the next waypoint is the last one - resets if true
            nextWaypointIndex = 0;
        }

        return nextWaypointIndex;
    }
}
