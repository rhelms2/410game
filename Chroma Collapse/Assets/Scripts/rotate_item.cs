using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate_item : MonoBehaviour
{
    public Vector3 rotateAmount = new Vector3(0, 120, 0);
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotateAmount * Time.deltaTime);
    }
}
