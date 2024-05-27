using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevator : GLOBAL_color
{
    // Template script/logic sourced from https://www.youtube.com/watch?v=ly9mK0TGJJo 
    
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
    private int _target_index;
    private Transform _previousWaypoint;
    private Transform _targetWaypoint;

    private float _timeToWaypoint;
    private float _elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        TargetNextWaypoint();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _elapsedTime += Time.deltaTime;

        float elapsedPercentage = _elapsedTime / _timeToWaypoint;
        // Smooth arrival to waypoints
        elapsedPercentage = Mathf.SmoothStep(0, 1, elapsedPercentage);
        transform.position = Vector3.Lerp(_previousWaypoint.position, _targetWaypoint.position, elapsedPercentage);
        transform.rotation = Quaternion.Lerp(_previousWaypoint.rotation, _targetWaypoint.rotation, elapsedPercentage);

        if (elapsedPercentage >= 1 && color == up_color) {
            TargetNextWaypoint();
        }
    }

    private void TargetNextWaypoint() {
        _previousWaypoint = _waypointPath.GetWaypoint(_target_index);
        _target_index = _waypointPath.GetNextWaypointIndex(_target_index);
        _targetWaypoint = _waypointPath.GetWaypoint(_target_index);

        _elapsedTime = 0;

        float distanceToWaypoint = Vector3.Distance(_previousWaypoint.position, _targetWaypoint.position);
        _timeToWaypoint = distanceToWaypoint / _speed;
    }

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