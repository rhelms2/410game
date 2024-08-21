using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GLOBAL_color : MonoBehaviour
{
    private static UnityEngine.Color orange = new Color(1.0f, 0.5f, 0, 0);
    private static UnityEngine.Color purple = new Color(1.0f, 0, 1.0f, 0);
    
    // This table keeps track of the active base colors
    public static bool[] base_color_table = {
        false,   // red
        false,   // yellow
        false    // blue
    };  
    
    public enum color_enum {
        grey,
        red,
        yellow,
        blue,
        orange,
        green,
        purple,
        white
    }

    // Each entry corresponds to the color enum
    public static UnityEngine.Color[] color_array = {
        Color.grey,
        Color.red,
        Color.yellow,
        Color.blue,
        orange,    // orange
        Color.green,                    // green
        purple,    // purple
        Color.white
    };

    // This will contain the currently active color, stored as an int. The actual color corresponds to the color_enum
    public static int color; 

    // Start is called before the first frame update
    void Start()
    {
        color = (int) color_enum.grey;
    }

}
