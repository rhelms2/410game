using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class ColorSwitcherRotation : GLOBAL_color
{
    private UnityEngine.Vector3 rotate_degrees = new UnityEngine.Vector3(0, 120, 0);
    bool changed;
    bool negate = false;
    bool rotating = false;
    int spire = 0;      // Keeps track of which spire has been selected by the user. 0 - red, 1 - yellow, 2 - blue
    int target_rotation = 120;    // Keeps track of where the controller needs to be rotated towards. Always rotates by 120 degrees
    int actual_rotation = 0;    // Keeps track of the progress towards target_rotation
    public int rotation_speed = 20;      // This is how much the controller will rotate per frame
    public AudioSource color_audio;
    public AudioSource rotate_audio;

    // Start is called before the first frame update
    void Start()
    {
        changed = false;
    }

    // Update is called once per frame
    void Update()
    {

        changed = false;

        if (Input.GetKeyDown("e"))
        {
            changed = true;
            RotateController(false);
        }
        else if (Input.GetKeyDown("q"))
        {
            changed = true;
            RotateController(true);
        }
        else if (Input.GetMouseButtonDown(1))
        {     // Right click

            // Change active base color based on which spire the user has selected

            if ((spire == 0) && inventory_activation[0])
            {
                base_color_table[0] = !base_color_table[0];
                changed = true;
            }
            else if ((spire == 1) && inventory_activation[1])
            {
                base_color_table[1] = !base_color_table[1];
                changed = true;
            }
            else if ((spire == 2) && inventory_activation[2])
            {
                base_color_table[2] = !base_color_table[2];
                changed = true;
            }

            if (changed)
            {
                color_audio.Play();
                Changecolor();
                changed = false;
            }
        }
        if (rotating)
        {
            RotateController();
        }

    }

    void Changecolor()
    {

        if (base_color_table[0])
        {

            if (base_color_table[1])
            {

                if (base_color_table[2])
                {
                    color = (int)color_enum.white;
                }
                else
                {
                    color = (int)color_enum.orange;
                }
            }
            else if (base_color_table[2])
            {
                color = (int)color_enum.purple;
            }
            else
            {
                color = (int)color_enum.red;
            }

        }
        else if (base_color_table[1])
        {

            if (base_color_table[2])
            {
                color = (int)color_enum.green;
            }
            else
            {
                color = (int)color_enum.yellow;
            }
        }
        else if (base_color_table[2])
        {
            color = (int)color_enum.blue;
        }
        else
        {
            color = (int)color_enum.grey;
        }

    }

    void RotateController(bool counterclockwise = true)
    {
        if (!rotating)
        {
            // Debug.Log(spire);
            spire += 1 + 1 * Convert.ToInt32(counterclockwise);
            // Debug.Log(spire);
            spire %= 3;
            // Debug.Log(spire);
            actual_rotation = 0;
            rotating = true;
            if (counterclockwise)
            {
                negate = true;
            }
        }

        if (actual_rotation == target_rotation)
        {     // target_rotation reached, don't need to rotate anymore
            rotating = false;
            negate = false;
        }
        else
        {

            if (negate)
            {
                transform.Rotate(new UnityEngine.Vector3(0, -rotation_speed, 0));
            }
            else
            {
                transform.Rotate(new UnityEngine.Vector3(0, rotation_speed, 0));
            }

            actual_rotation += rotation_speed;
        }
    }
}
