using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Item_Pickup : Player_Inventory
{
    public GameObject player;
    public GameObject item;
    public float pickup_distance;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((player.transform.position.x <= gameObject.transform.position.x + pickup_distance) && 
                (player.transform.position.x >= gameObject.transform.position.x - pickup_distance) &&
                (player.transform.position.z <= gameObject.transform.position.z + pickup_distance) && 
                (player.transform.position.z >= gameObject.transform.position.z - pickup_distance)) {

            inventory.Add(item);
            inventory_changed = true;

            Debug.Log("Adding item to player inventory. Item: " + inventory.Last());

            gameObject.SetActive(false);
        }
    }
}
