using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Camera : MonoBehaviour
{
    /*simple script to keep track of camera also from 'Dave / GameDevelopment' 
     YT link: https://www.youtube.com/watch?v=f473C43s8nE */

    public Transform cameraPos;
    // Start is called before the first frame update
    void Start()
    {
        //nothing needs to be done here
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = cameraPos.position;
    }
}
