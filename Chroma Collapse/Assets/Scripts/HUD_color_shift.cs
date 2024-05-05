using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_color_shift : GLOBAL_color
{
    public Image[] gunImg;
    UnityEngine.Color curcol;

    // Start is called before the first frame update
    void Start()
    {
        curcol = color_array[color];
    }

    // Update is called once per frame
    void Update()
    {
        if (curcol != color_array[color]){
            foreach(Image j in gunImg) j.enabled = false;
            gunImg[color].enabled = true;
            curcol = color_array[color];
        }
    }
}
