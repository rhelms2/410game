using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GLOBAL_color : MonoBehaviour
{
    public static bool[] base_color_table = {
        false,   // red
        false,   // yellow
        false,    // blue
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
        Color.black,
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
    bool changed;

    // Start is called before the first frame update
    void Start()
    {
        changed = false;
        color = (int) color_enum.grey;
    }

    // Update is called once per frame
    void Update()
    {
        changed = false;
        if (Input.GetKeyDown("2")) {
            base_color_table[0] = !base_color_table[0];
            changed = true;
        }
        else if (Input.GetKeyDown("1")) {
            base_color_table[1] = !base_color_table[1];
            changed = true;
        }
        else if (Input.GetKeyDown("3")) {
            base_color_table[2] = !base_color_table[2];
            changed = true;
        }
        if (changed){
            changeColor();
        }

        // Debug.Log("red = " + base_color_table[0]);
        // Debug.Log("yellow = " + base_color_table[1]);
        // Debug.Log("blue = " + base_color_table[2]);
    }

    void changeColor(){
        if (base_color_table[0]){
                if (base_color_table[1]){
                    if (base_color_table[2]){
                        color = (int) color_enum.white;
                    } 
                    else {
                        color = (int) color_enum.orange;
                    }   
                }
                else if (base_color_table[2]){
                    color = (int) color_enum.purple;
                }
                else {
                    color = (int) color_enum.red;
                }
            }
            else if (base_color_table[1]){
                if (base_color_table[2]) {
                    color = (int) color_enum.green;
                }
                else {
                    color = (int) color_enum.yellow;
                }
            } else if (base_color_table[2]){
                color = (int) color_enum.blue;
            } else {
                color = (int) color_enum.grey;
            }
    }
}
