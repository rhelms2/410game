using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteColor : GLOBAL_color
{
    public color_enum myCol;
    SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = color_array[(int)myCol];
    }

    // Update is called once per frame
    void Update()
    {
    }
}
