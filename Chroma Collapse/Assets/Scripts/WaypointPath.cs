using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointPath : MonoBehaviour
{
    // Template script/logic sourced from https://www.youtube.com/watch?v=ly9mK0TGJJo 
    public Transform GetWaypoint(int waypoint_index) {
        return transform.GetChild(waypoint_index);
    }

    public int GetNextWaypointIndex(int current_index) {
        int next_index = current_index + 1;

        if (next_index == transform.childCount) {
            next_index = 0;
        }

        return next_index;
    }
}
