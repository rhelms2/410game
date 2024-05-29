using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteColor : Goomber_All
{
    SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = color_array[defense_color];
    }
}
