using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Color_swap : MonoBehaviour
{
    public Material red;
    public Material green;
    public Material blue;
    public GameObject Object;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("1")) {
            Object.GetComponent<MeshRenderer> ().material = red;
        }
        else if (Input.GetKey("2")) {
            Object.GetComponent<MeshRenderer> ().material = green;
        }
        else if (Input.GetKey("3")) {
            Object.GetComponent<MeshRenderer> ().material = blue;
        }
    }
}
