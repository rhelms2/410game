using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Item_Pickup : MonoBehaviour
{
    [SerializeField] int item_index;
    [SerializeField] AudioSource pickup_noise;

    void Awake() {
        if (Player_Inventory.instance.inventory_activation[item_index]) { 
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {

            Player_Inventory.instance.ActivateHUDItem(item_index);
            pickup_noise.Play();

            // Debug.Log("Adding item to player inventory. Item: " + inventory.Last());

            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
