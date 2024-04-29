using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class block_collision_switcher : GLOBAL_color
{
    private bool isOn;
    public int trigger_color;
    public Rigidbody rb;
    //Collider obj_Collider;
    public Material fill;
    public Material wire;
    public GameObject obj;

    // Start is called before the first frame update
    void Start()
    {
        isOn = false;
        //rb.detectCollisions = false;

        //obj_Collider = GetComponent<Collider>();        
    }

    // Update is called once per frame
    void Update()
    {
        //col_num associates with global color script, an integer value that tells the system what color something is. 
        if (color == trigger_color){
            if (isOn == false){
                isOn = true;

                rb.detectCollisions = true;
                //obj_Collider.enabled = true;
                 

            }
        }
        else{
             isOn = false;
             rb.detectCollisions = false;
            //obj_Collider.enabled = false;
        }
        SetMaterial();
    }

    void SetMaterial() {
        if (isOn) {
            obj.GetComponent<Renderer>().material = fill;
        }
        else {
            obj.GetComponent<Renderer>().material = wire;
        }
        obj.GetComponent<Renderer>().material.color = color_array[color];
        Debug.Log("Material is " + obj.GetComponent<Renderer>().material);
    }
}
