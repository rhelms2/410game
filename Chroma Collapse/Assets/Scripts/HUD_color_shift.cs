using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_color_shift : GLOBAL_color
{
    //0 - black, 1 - red, 3 - yellow, 5 - blue
    public Image[] armImg;
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
            foreach(Image i in armImg) i.enabled = false;
            foreach(Image j in gunImg) j.enabled = false;
            armImg[color].enabled = true;
            gunImg[color].enabled = true;
            curcol = color_array[color];
        }
    }
}
