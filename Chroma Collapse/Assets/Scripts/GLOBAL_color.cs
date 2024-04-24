using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GLOBAL_color : MonoBehaviour
{
    public bool red;    // hex 0xFF0000
    public bool green;  // hex 0x00FF00
    public bool blue;   // hex 0x0000FF
    public static UnityEngine.Color current_color;

    // Start is called before the first frame update
    void Start()
    {
        red = false;
        green = false;
        blue = false;
        current_color = Color.grey;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("1")) {
            red = !red;
            current_color[0] = Convert.ToInt32(red);
        }
        else if (Input.GetKey("2")) {
            green = !green;
            current_color[1] = Convert.ToInt32(green);
        }
        else if (Input.GetKey("3")) {
            blue = !blue;
            current_color[2] = Convert.ToInt32(red);
        }

        /*
        if (!(red || green || blue)) {
            current_color = Color.grey;
        }
        */

        Debug.Log("current_color = " + current_color);
    }
}
