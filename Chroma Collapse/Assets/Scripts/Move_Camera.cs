using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Camera : MonoBehaviour
{
    /*simple script to keep track of camera also from 'Dave / GameDevelopment' 
     YT link: https://www.youtube.com/watch?v=f473C43s8nE */

    public Transform cameraPos;

    //

    [SerializeField] private bool _enable = false;

    [SerializeField, Range(0, 0.1f)] private float _amp = 0.015f;
    [SerializeField, Range(0, 30)] private float _freq = 10.0f;

    [SerializeField] private Transform _cam = null;
    [SerializeField] private Transform _camHolder = null;

    private float _toggleSpeed = 3.0f;
    private Vector3 _startPos;
    private CharacterController _controller;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _startPos = _cam.localPosition;
    }

    private Vector3 FootMotion()
    {
        Vector3 pos = Vector3.zero;
        pos.y += Mathf.Sin(Time.time * _freq) * _amp;
        pos.x += Mathf.Cos(Time.time * _freq / 2) * _amp * 2;
        return pos;
    }

    private void CheckMotion()
    {
        float speed = new Vector3(_controller.velocity.x, 0, _controller.velocity.z).magnitude;
        if (speed < _toggleSpeed) return;
        if (!_controller.isGrounded) return;
        PlayMotion(FootMotion());
    }

    private void ResetPos()
    {
        if (_cam.localPosition == _startPos) return;
        _cam.localPosition = Vector3.Lerp(_cam.localPosition, _startPos, 1 * Time.deltaTime);
    }

    private Vector3 FocusTarget()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + _camHolder.localPosition.y, transform.position.z);
        pos += _camHolder.forward * 15.0f;
        return pos;
    }

    private void PlayMotion(Vector3 motion) { _cam.localPosition += motion; }

    // Start is called before the first frame update
    void Start()
    {
        //nothing needs to be done here
    }

    // Update is called once per frame
    void Update()
    {
        if (_enable)
        {
            CheckMotion();
            ResetPos();
            _cam.LookAt(FocusTarget());
        }
        transform.position = cameraPos.position;
    }
}