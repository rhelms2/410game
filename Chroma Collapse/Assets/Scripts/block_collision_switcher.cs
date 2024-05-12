using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
// using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
// using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class block_collision_switcher : GLOBAL_color
{
    int curcol;     // Keeps track of if color has changed between frames so material change isn't computed every frame
    
    /* This is an int from 0 to 3 that keeps track of which material to set to the object based off of the active colors
    if this is 0 and negate is false, then it will set the collision to true and the material to fill. The opposite
    is the case if negate is true - the material will be wire and collision will be set to false. If this variable is 1,
    the material is set to the transparent material and the collision settings from when transparency is 0 will be kept.
    When transparency is 2, the cases from 0 are reversed. */
    private int transparency = 0; 
    public int trigger_color;
    public int tolerance = 0;   // This determines how many other colors can be on with the trigger color
    public bool negate = false;     // If this is set to true, the trigger color turns to a deactivation color
    public Rigidbody rb;
    public Material fill;
    public Material transparent;
    public Material wire;
    public GameObject obj;

    // Start is called before the first frame update
    void Start()
    {
        if ((tolerance < 0) || (tolerance > 3)) {
            Debug.LogError("tolerance must be an integer between 0 and 3!");
        }     
        else {
            curcol = color;
            UpdateObject(2);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Only do computations if color has changed from the previous frame

        if (curcol != color) {

            if (trigger_color == color) {
                transparency = 0;
            }
            else if (tolerance > 0) {
                if ((trigger_color == (int) color_enum.red) || (trigger_color == (int) color_enum.yellow) || (trigger_color == (int) color_enum.blue)) {
                    // table is indexed by trigger color - 1 since grey is not present in this table
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
            UpdateObject(transparency);
            curcol = color;     // update curcol for future comparisons
        }

        Debug.Log("Color: " + color + "Curcol: " + curcol);
        Debug.Log("transparency: " + transparency);
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

    void UpdateObject(int transparency) {
        if (transparency == 0) {
            if (negate == false) {
                obj.GetComponent<Renderer>().material = fill;
                rb.detectCollisions = true;
            }
            else {
                obj.GetComponent<Renderer>().material = wire;
                rb.detectCollisions = false;
            }
        } 
        else if (transparency == 1) {
            obj.GetComponent<Renderer>().material = transparent;
            rb.detectCollisions = !negate;
        }
        else {
            if (negate == false) {
                obj.GetComponent<Renderer>().material = wire;
                rb.detectCollisions = false;
            }
            else {
                obj.GetComponent<Renderer>().material = fill;
                rb.detectCollisions = true;
            }
        }

        obj.GetComponent<Renderer>().material.color = color_array[trigger_color];
        // Debug.Log("Material is " + obj.GetComponent<Renderer>().material);
    }

}
