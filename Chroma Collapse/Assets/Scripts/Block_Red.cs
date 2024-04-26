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
        if (col_num == 1 || col_num == 2 || col_num == 6){
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
