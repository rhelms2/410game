using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_Red : GLOBAL_color
{

    private bool isOn;
    public Rigidbody rb;
    //Collider obj_Collider;

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
        if (col_num == 1 || col_num == 2 || col_num == 6){
            if (isOn == false){
                isOn = true;

                rb.detectCollisions = true;
                //obj_Collider.enabled = true;

                 

            }
        }else{
             isOn = false;
             rb.detectCollisions = false;

            //obj_Collider.enabled = false;
        }
    }
}
