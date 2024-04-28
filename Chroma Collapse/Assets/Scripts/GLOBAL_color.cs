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
    public static int col_num;
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
        col_num = 0;
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
                    current_color = orange;
                    col_num = 2;
                }
                else if (blue){
                    current_color = purple;
                    col_num = 6;
                }
                else {
                    current_color = Color.red;
                    col_num = 1;
                }
            }
            else if (yellow){
                if (blue) {
                    current_color = Color.green;
                    col_num = 4;
                }
                else {
                    current_color = Color.yellow;
                    col_num = 3;
                }
            } else if (blue){
                current_color = Color.blue;
                col_num = 5;
            } else {
                current_color = Color.grey;
                col_num = 0;
            }
    }
}
