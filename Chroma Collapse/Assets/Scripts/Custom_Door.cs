using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Custom_Door : MonoBehaviour
{
    [SerializeField] private GameObject Door;
    public int item_index;

    // Start is called before the first frame update
    void Start()
    {
        Door.transform.gameObject.GetComponent<door_vertical>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player_Inventory.instance.inventory_activation[item_index]) {
            Door.transform.gameObject.GetComponent<door_vertical>().enabled = true;
            Destroy(this);
        }
    }
}
