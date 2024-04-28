using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_Red : GLOBAL_color
{
    private bool isOn;
    //Rigidbody blocks;
    //BoxCollider bc;

    // Start is called before the first frame update
    void Start()
    {
        //blocks = GetComponent<Rigidbody>();
        //bc = GetComponent<BoxCollider>();
        Physics.IgnoreLayerCollision(9, 11, true);
        isOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        //col_num associates with global color script, an integer value that tells the system what color something is. 
        if (col_num == 1 || col_num == 2 || col_num == 6){
            if (isOn == false){
                isOn = true;
                Physics.IgnoreLayerCollision(9, 11, false);
            }
        }else{
            if (isOn == true){
                isOn = false;
                Physics.IgnoreLayerCollision(9, 11, true);
            }
        }
    }
}
