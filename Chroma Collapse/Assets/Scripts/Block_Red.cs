using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_Red : GLOBAL_color
{
    private bool isOn;
<<<<<<< HEAD
    //Rigidbody blocks;
    //BoxCollider bc;
=======
    public Rigidbody rb;
    //Collider obj_Collider;
>>>>>>> ba9bbf324b12d6b974092becfcda14fa419f5ad3

    // Start is called before the first frame update
    void Start()
    {
        //blocks = GetComponent<Rigidbody>();
        //bc = GetComponent<BoxCollider>();
        Physics.IgnoreLayerCollision(9, 11, true);
        isOn = false;
<<<<<<< HEAD
=======
        //rb.detectCollisions = false;

        //obj_Collider = GetComponent<Collider>();
>>>>>>> ba9bbf324b12d6b974092becfcda14fa419f5ad3
    }

    // Update is called once per frame
    void Update()
    {
        //col_num associates with global color script, an integer value that tells the system what color something is. 
        if (col_num == 1 || col_num == 2 || col_num == 6){
            if (isOn == false){
                isOn = true;
<<<<<<< HEAD
                Physics.IgnoreLayerCollision(9, 11, false);
            }
        }else{
            if (isOn == true){
                isOn = false;
                Physics.IgnoreLayerCollision(9, 11, true);
            }
=======

                rb.detectCollisions = true;
                //obj_Collider.enabled = true;

                 

            }
        }else{
             isOn = false;
             rb.detectCollisions = false;

            //obj_Collider.enabled = false;
>>>>>>> ba9bbf324b12d6b974092becfcda14fa419f5ad3
        }
    }
}
