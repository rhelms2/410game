using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class ColorSwitcherRotation : GLOBAL_color
{
    private UnityEngine.Vector3 rotate_degrees = new UnityEngine.Vector3(0, 120, 0);
    bool changed;
    bool negate;
    int spire = 0;      // Keeps track of which spire has been selected by the user. 0 - red, 1 - yellow, 2 - blue

    // Start is called before the first frame update
    void Start()
    {
        changed = false;
    }

    // Update is called once per frame
    void Update()
    {

        changed = false;
        negate = false;

        if (Input.GetKeyDown("e")) {
            changed = true;
        }
        else if (Input.GetKeyDown("q")) {
            changed = true;
            negate = true;
        }
        else if (Input.GetMouseButtonDown(1)) {     // Right click

            // Change active base color based on which spire the user has selected

            if (spire == 0) {
                base_color_table[0] = !base_color_table[0];
                changed = true;
            }
            else if (spire == 1) {
                base_color_table[1] = !base_color_table[1];
                changed = true;
            }
            else if (spire == 2) {
                base_color_table[2] = !base_color_table[2];
                changed = true;
            }
            
            if (changed) {
                Changecolor();
                changed = false;
            }
        }

        if (changed) {
            RotateSwitcher();
        }

    }

    void Changecolor() {
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

    void RotateSwitcher() {
        if (negate) {
            transform.Rotate(-rotate_degrees);
            spire--;
            if (spire < 0) {
                spire = 2;
            }
        }
        else {
            transform.Rotate(rotate_degrees);
            spire++;
            if (spire > 2) {
                spire = 0;
            }
        }
    }
}
