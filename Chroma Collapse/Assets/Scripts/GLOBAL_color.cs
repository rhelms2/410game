using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GLOBAL_color : MonoBehaviour
{
    bool red;   
    bool yellow;  
    bool blue;  
    public enum color_enum {
        grey,
        red,
        orange,
        yellow,
        green,
        blue,
        purple,
        white
    }

    // Each entry corresponds to the color enum
    public UnityEngine.Color[] color_array = {
        Color.grey,
        Color.red,
        new Color(1.0f, 0.5f, 0, 0),
        Color.yellow,
        Color.green,
        Color.blue,
        new Color(1.0f, 0.5f, 0, 0),
        Color.white
    };

    // This will contain the currently active color, stored as an int. The actual color corresponds to the color_enum
    public static int color; 
    bool changed;

    // Start is called before the first frame update
    void Start()
    {
        red = false;
        yellow = false;
        blue = false;
        changed = false;
        color = (int) color_enum.grey;
    }

    // Update is called once per frame
    void Update()
    {
        changed = false;
        if (Input.GetKeyDown("2")) {
            red = !red;
            changed = true;
        }
        else if (Input.GetKeyDown("1")) {
            yellow = !yellow;
            changed = true;
        }
        else if (Input.GetKeyDown("3")) {
            blue = !blue;
            changed = true;
        }
        if (changed){
            changeColor();
        }

        // Debug.Log("current_color = " + current_color);
    }

    void changeColor(){
        if (red){
                if (yellow){
                    if (blue){
                        color = (int) color_enum.white;
                    } else {
                    color = 2;
                }}
                else if (blue){
                    color = (int) color_enum.purple;
                }
                else {
                    color = (int) color_enum.red;
                }
            }
            else if (yellow){
                if (blue) {
                    color = (int) color_enum.green;
                }
                else {
                    color = (int) color_enum.yellow;
                }
            } else if (blue){
                color = (int) color_enum.blue;
            } else {
                color = (int) color_enum.grey;
            }
    }
}
