using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTarget : GLOBAL_color
{
    public GameObject diamond;
    [SerializeField] private int activation_color;
    public bool active = false;

    // Start is called before the first frame update
    void Start()
    {
        diamond.transform.GetChild(0).GetComponent<Renderer>().material.color = color_array[activation_color];
        diamond.transform.GetChild(1).GetComponent<Renderer>().material.color = color_array[activation_color];
        diamond.GetComponent<Light>().intensity = 0f; 
    }

    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log("Other collider has entered diamond collider");

        if (other.tag == "Bullet")
        {
            // Debug.Log("hit!");
            if (other.gameObject.GetComponent<Light>().color == color_array[activation_color]) {
                diamond.GetComponent<Light>().color = color_array[activation_color];
                diamond.GetComponent<Light>().intensity = 50f;
                active = true;
            }
        }
    }
}
