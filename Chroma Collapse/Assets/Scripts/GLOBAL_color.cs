using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GLOBAL_color : MonoBehaviour
{
    // This table keeps track of if the player has access to these colors on the color switcher
    public static bool[] inventory_activation = {
        false,   // red
        false,   // yellow
        false    // blue
    };
    
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
    public UnityEngine.Color[] color_array = {
        Color.grey,
        Color.red,
        Color.yellow,
        Color.blue,
        new Color(1.0f, 0.5f, 0, 0),    // orange
        Color.green,                    // green
        new Color(1.0f, 0, 1.0f, 0),    // purple
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
