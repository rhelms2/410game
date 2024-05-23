using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Item_Pickup : Player_Inventory
{

    [SerializeField]
    private GameObject player;

    [SerializeField]
    GameObject item;

    [SerializeField]
    AudioSource pickup_noise;

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            inventory.Add(item);
            inventory_changed = true;
            pickup_noise.Play();

            // Debug.Log("Adding item to player inventory. Item: " + inventory.Last());

            gameObject.SetActive(false);
        }
    }
}
