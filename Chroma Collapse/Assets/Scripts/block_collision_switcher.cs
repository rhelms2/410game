using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using Unity.VisualScripting;
using UnityEngine;

public class block_collision_switcher : GLOBAL_color
{
    private bool isOn;
    private int transparency = 0;
    public int trigger_color;
    public int tolerance = 0;   // This determines how many other colors can be on with the trigger color
    public bool negate = false;     // If this is set to true, the trigger color turns to a deactivation color
    public Rigidbody rb;
    public Material fill;
    public Material transparent;
    public Material wire;
    public GameObject obj;

    public Camera mainCamera; // Reference to the main camera
    public float minThickness = 0f;
    public float maxThickness = .3f;
    public float minDistance = 1f;
    public float maxDistance = 50f;

    // Start is called before the first frame update
    void Start()
    {
        isOn = negate; 
        if ((tolerance < 0) || (tolerance > 3)) {
            Debug.LogError("tolerance must be an integer between 0 and 3!");
        }     
    }

    // Update is called once per frame
    void Update()
    {   
        if (trigger_color == color) {
            transparency = 0;
        }
        else if (tolerance > 0) {
            if ((trigger_color == (int) color_enum.red) || (trigger_color == (int) color_enum.yellow) || (trigger_color == (int) color_enum.blue)) {
                if ((base_color_table[trigger_color - 1]) && ((NumActiveColors() - tolerance) <= 1)) {
                    transparency = 1;
                }
                else {
                    transparency = 2;
                }
            }
            else {
                if (color == (int) color_enum.white) {
                    transparency = 1;
                }
                else {
                    transparency = 2;
                }
            }
        }
        else {
            transparency = 2;
        }
        SetMaterial(transparency);
    }

    int NumActiveColors() {

        int active_colors = 0;

        for (int i = 0; i < 3; i++) {
            if (base_color_table[i]) {
                active_colors++;
            }
        }

        return active_colors;
    }

    void SetMaterial(int transparency) {
        if (transparency == 0) {
            obj.GetComponent<Renderer>().material = fill;
            SetCollision();
        }
        else if (transparency == 1) {
            obj.GetComponent<Renderer>().material = transparent;
            SetCollision();
        }
        else {
            Debug.Log("entered");
            obj.GetComponent<Renderer>().material = wire;
            isOn = negate;
            rb.detectCollisions = negate;

            // Calculate distance between camera and object
            float distance = Vector3.Distance(mainCamera.transform.position, transform.position);
            // Map distance to thickness
            float thickness = Map(1 / distance, 1 / maxDistance, minDistance, minThickness, maxThickness);

            wire.SetFloat("_Wireframe_Thickness", thickness);
        }

        obj.GetComponent<Renderer>().material.color = color_array[trigger_color];
        // Debug.Log("Material is " + obj.GetComponent<Renderer>().material);
    }

    void SetCollision() {
        if (isOn == negate) {
            isOn = !negate;
            rb.detectCollisions = !negate;
            //obj_Collider.enabled = true;
        }
    }

    private float Map(float value, float fromMin, float fromMax, float toMin, float toMax)
    {
        Debug.Log("distance = " + value);


        if (value <= fromMin)
        {
            return 0f;
        }
        return toMin + (value - fromMin) * (toMax - toMin) / (fromMax - fromMin);
    }
}
