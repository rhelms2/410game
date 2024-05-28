using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class elevator : GLOBAL_color
{
    // Template script/logic sourced from https://www.youtube.com/watch?v=ly9mK0TGJJo 
    // This derivation can only move between 2 waypoints
    
    [SerializeField]
    private WaypointPath _waypointPath;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private int up_color;

    [SerializeField]
    private int down_color;

    private int curcol = 0;

    private Transform old_parent;
    private Transform _previousWaypoint;
    private Transform _targetWaypoint;

    private float _timeToWaypoint;
    private bool moving;

    // Start is called before the first frame update
    void Start()
    {
        moving = false;
        curcol = color;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (curcol != color) {

            if (color == up_color) {
                setWaypoint(0);
                moving = true;
            }
            else if (color == down_color) {
                setWaypoint(1);
                moving = true;
            }
            else {
                moving = false;
            }

        }

        if (moving) {
            transform.position = Vector3.MoveTowards(transform.position, _targetWaypoint.position, _speed);
            transform.rotation = Quaternion.Lerp(transform.rotation, _targetWaypoint.rotation, _speed);

            float distance = (transform.position - _targetWaypoint.position).magnitude;

            if (distance <= 1) {
                moving = false;
            }
        }
    }

    private void setWaypoint(int index) {
        _targetWaypoint = _waypointPath.transform.GetChild(index);
        float distanceToWaypoint = Vector3.Distance(transform.position, _targetWaypoint.position);
        _timeToWaypoint = distanceToWaypoint / _speed;
    }

/*
    private void TargetNextWaypoint() {
        _previousWaypoint = _waypointPath.GetWaypoint(_target_index);
        _target_index = _waypointPath.GetNextWaypointIndex(_target_index);
        _targetWaypoint = _waypointPath.GetWaypoint(_target_index);

        _elapsedTime = 0;

        float distanceToWaypoint = Vector3.Distance(_previousWaypoint.position, _targetWaypoint.position);
        _timeToWaypoint = distanceToWaypoint / _speed;
    }
*/
    private void OnTriggerEnter(Collider other) {

        // Debug.Log("Other collider " + other.tag + " has entered collider");

        if (other.tag == "Player") {
            old_parent = other.transform.parent.parent;
            other.transform.parent.SetParent(transform);
        }
        else if (other.tag == "Gravity Sensitive Object") {
             other.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit(Collider other) {

        // Debug.Log("Other collider " + other.tag + " has exited collider");

        if (other.tag == "Player") {
            other.transform.parent.SetParent(old_parent);
        }
        else if (other.tag == "Gravity Sensitive Object") {
             other.transform.SetParent(null);
        }
    }
}
