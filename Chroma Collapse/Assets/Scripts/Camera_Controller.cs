using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    /* movement and camera code from 'Dave / GameDevelopment' on Youtube.
     link to video: https://www.youtube.com/watch?v=f473C43s8nE */

    //create variables for camera movement
    public float sensX;
    public float sensY;

    //keep track of player position
    public Transform orientation;

    //set up rotation values to hold the movement data of the mouse
    float rotationX;
    float rotationY;


    // Start is called before the first frame update
    void Start()
    {
        //set cursor to be invisble and lock it to the center
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        //apply mous input
        rotationY += mouseX;
        rotationX -= mouseY;

        //clamp the x-axis
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        //rotate plaer and camera
        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
        orientation.rotation = Quaternion.Euler(0, rotationY, 0);

    }
}