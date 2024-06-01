using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightLogic : GLOBAL_color
{
    public GameObject lightBulb;
    public Light actualLight;
    public float colorTimeDelay = 1f; // Time in between color flashes
    public bool active = false; // Dont Change! Modified by ButtonPressed.cs
    public bool finished = false; // Dont Change! Modified by ButtonPressed.cs
    public bool win; // Dont Change! Modified by ButtonPressed.cs

    private float colorTimePassed = 0;
    private int numColors;
    private int prevColor;
    private int currentColor;
    private bool roundPassed;
    private List<int> colorOrder;

    // Start is called before the first frame update
    void Start()
    {
        actualLight = lightBulb.GetComponent<Light>();
        actualLight.intensity = 0;
        colorOrder = new List<int> { 2, 5, 7, 6, 1, 4 };
        numColors = colorOrder.Count;
        prevColor = colorOrder[0];
        Debug.Log("numColors " + numColors);
        win = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (active) {
            int time = (int)colorTimePassed;
            Debug.Log(time + " --- " + colorTimePassed);
            if (time == numColors)
            {
                finished = true;
                active = false;
                colorTimePassed = 0f;
                actualLight.intensity = 0;
            }

            else
            {
                currentColor = colorOrder[time];
                actualLight.color = color_array[colorOrder[time]];
                if (color == currentColor)
                {
                    roundPassed = true;
                }
                if (prevColor != currentColor)
                {
                    if (!roundPassed)
                    {
                        win = false;
                    }
                    prevColor = colorOrder[time];
                    roundPassed = false;
                }
                colorTimePassed += Time.deltaTime;
            }

        }
    }
}
