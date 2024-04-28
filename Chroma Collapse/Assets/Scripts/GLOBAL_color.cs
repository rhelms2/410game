using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GLOBAL_color : MonoBehaviour
{
    public bool red;   
    public bool yellow;  
    public bool blue;  
    public static UnityEngine.Color current_color;
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
    public static int color;
    bool changed;
    UnityEngine.Color purple = new Color(0.6f,0,0.9f,0);
    UnityEngine.Color orange = new Color(1.0f, 0.5f, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        red = false;
        yellow = false;
        blue = false;
        changed = false;
        current_color = Color.grey;
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

        Debug.Log("current_color = " + current_color);
    }

    void changeColor(){
        if (red){
                if (yellow){
                    if (blue){
                        current_color = Color.white;
                        color = (int) color_enum.white;
                    } else {
                    current_color = orange;
                    color = 2;
                }}
                else if (blue){
                    current_color = purple;
                    color = (int) color_enum.purple;
                }
                else {
                    current_color = Color.red;
                    color = (int) color_enum.red;
                }
            }
            else if (yellow){
                if (blue) {
                    current_color = Color.green;
                    color = (int) color_enum.green;
                }
                else {
                    current_color = Color.yellow;
                    color = (int) color_enum.yellow;
                }
            } else if (blue){
                current_color = Color.blue;
                color = (int) color_enum.blue;
            } else {
                current_color = Color.grey;
                color = (int) color_enum.grey;
            }
    }
}
