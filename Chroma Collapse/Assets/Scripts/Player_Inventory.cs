using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Inventory : MonoBehaviour
{
    public static Player_Inventory instance;

    // The order the items are set in the editor MUST match the corresponding index of the inventory_activation list
    public GameObject[] inventory_list;

    // This list is accessed in other scripts to check what items the player has so as to expand/limit functionality
    public bool[] inventory_activation;

    void Awake() {
        if (instance != null) {
            Destroy(this.gameObject);
            return;
        }

        instance = this;

        for (int i = 0; i < inventory_list.Length; i++) {

            if (inventory_activation[i]) {
                ActivateHUDItem(i);
            }
        }
        
    }

    public void ActivateHUDItem(int item_index) {
        if (item_index >= 0 && item_index < inventory_list.Length) {
            inventory_list[item_index].SetActive(true);
            inventory_activation[item_index] = true;
        }
        else {
            Debug.Log("Item index is invalid");
        }
    }

}
