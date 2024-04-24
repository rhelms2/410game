using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GLOBAL_color : MonoBehaviour
{
    public bool red;    // hex 0xFF0000
    public bool green;  // hex 0x00FF00
    public bool blue;   // hex 0x0000FF
    public int current_color;

    // Start is called before the first frame update
    void Start()
    {
        red = false;
        green = false;
        blue = false;
        current_color = 0x808080;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("1")) {
            red = !red;
            current_color = current_color ^ (Convert.ToInt32(red) * 0xFF0000);
        }
        else if (Input.GetKey("2")) {
            green = !green;
            current_color = current_color ^ (Convert.ToInt32(green) * 0x00FF00);
        }
        else if (Input.GetKey("3")) {
            blue = !blue;
            current_color = current_color ^ (Convert.ToInt32(blue) * 0x0000FF);
        }
    }
}
