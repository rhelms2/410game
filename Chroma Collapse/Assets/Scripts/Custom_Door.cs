using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Custom_Door : Player_Inventory
{
    public GameObject Door;

    // Start is called before the first frame update
    void Start()
    {
        Door.transform.gameObject.GetComponent<door_vertical>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (inventory.Count > 0) {
            Door.transform.gameObject.GetComponent<door_vertical>().enabled = true;
            Destroy(this);
        }
    }
}
