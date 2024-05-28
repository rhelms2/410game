using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glow : GLOBAL_color
{
    public GameObject diamond;

    // Start is called before the first frame update
    void Start()
    {
        diamond.GetComponent<Light>().intensity = 0f; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            Debug.Log("hit!");
            diamond.GetComponent<Light>().intensity = 50f;
        }
    }
}
