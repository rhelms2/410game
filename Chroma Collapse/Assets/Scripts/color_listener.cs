using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Color_swap : GLOBAL_color
{        
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Call SetColor using the shader property name "_Color" and setting the color to red
        this.GetComponent<Renderer>().material.color = color_array[color];
        // Debug.Log("IN COLOR_SWAP: " + current_color);
    }
}
