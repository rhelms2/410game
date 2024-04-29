using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Color_swap : GLOBAL_color
{    
    public GameObject Object;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Call SetColor using the shader property name "_Color" and setting the color to red
       Object.GetComponent<Renderer>().material.color = current_color;
       // Debug.Log("IN COLOR_SWAP: " + current_color);
    }
}
