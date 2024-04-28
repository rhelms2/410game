using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_Red : GLOBAL_color
{

    private bool isOn;
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        isOn = false;
        rb.detectCollisions = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (color == 1 || color == 2 || color == 6){
            if (isOn == false){
                isOn = true;
                rb.detectCollisions = true;
            }
        }else{
             isOn = false;
             rb.detectCollisions = false;
        }
    }
}
