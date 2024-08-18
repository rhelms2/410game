using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Item_Pickup : Player_Inventory
{
    [SerializeField] GameObject item;

    [SerializeField] AudioSource pickup_noise;

    void Awake() {
        if (gameObject != null) { 
            foreach (string tag in inventory) {
                if (tag == gameObject.tag) {
                    gameObject.SetActive(false);
                    Destroy(gameObject);
                }
            }
        }
        else {
            return;
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {

            inventory.Add(item.tag);
            inventory_changed = true;
            pickup_noise.Play();

            // Debug.Log("Adding item to player inventory. Item: " + inventory.Last());

            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
