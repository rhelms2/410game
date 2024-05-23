using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class Player_Inventory : GLOBAL_color
{
    protected static List<GameObject> inventory = new List<GameObject>();

    [SerializeField]
    private bool testing = false;
    [SerializeField]
    private GameObject ColorSwitcher;
    [SerializeField]
    private GameObject red_crystal;
    [SerializeField]
    private GameObject yellow_crystal;
    [SerializeField]
    private GameObject blue_crystal;
    [SerializeField]
    private GameObject gun;

    // This boolean is switched on by item pickups which triggers UpdateUI
    static protected bool inventory_changed = false;

    void Awake() {

        // If testing the game, activate all inventory slots
        if (testing) {
            inventory.Add(ColorSwitcher);
            inventory.Add(red_crystal);
            inventory.Add(yellow_crystal);
            inventory.Add(blue_crystal);
            int i = 0;
            foreach (bool item in inventory_activation) {
                inventory_activation[i] = true;
                i++;
            }
            UpdateUI();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inventory_changed) {
            UpdateUI();
            inventory_changed = false;
        }
    }

    protected void UpdateUI() {
        foreach (GameObject obj in inventory) {

            // Debug.Log("Updating UI... current game object: " + obj);

            if (obj == ColorSwitcher) {
                // sets the color switcher to active

                ColorSwitcher.SetActive(true);
            }
            else if (obj == red_crystal) {
                
                // sets red spire active and allows control of red by the player

                ColorSwitcher.transform.GetChild(0).gameObject.GetComponent<Color_swap>().enabled = true;
                ColorSwitcher.transform.gameObject.GetComponent<ColorSwitcherRotation>().enabled = true;
                inventory_activation[0] = true;
                ColorSwitcher.transform.GetChild(1).GetChild(0).gameObject.GetComponent<block_collision_switcher>().enabled = true;
            }
            else if (obj == yellow_crystal) {

                // sets yellow spire active and allows control of yellow by the player
                
                ColorSwitcher.transform.GetChild(0).gameObject.GetComponent<Color_swap>().enabled = true;
                ColorSwitcher.transform.gameObject.GetComponent<ColorSwitcherRotation>().enabled = true;
                inventory_activation[1] = true;
                ColorSwitcher.transform.GetChild(2).GetChild(0).gameObject.GetComponent<block_collision_switcher>().enabled = true;
            }
            else if (obj == blue_crystal) {

                // sets blue spire active and allows control of blue by the player

                ColorSwitcher.transform.GetChild(0).gameObject.GetComponent<Color_swap>().enabled = true;
                ColorSwitcher.transform.gameObject.GetComponent<ColorSwitcherRotation>().enabled = true;
                inventory_activation[2] = true;
                ColorSwitcher.transform.GetChild(3).GetChild(0).gameObject.GetComponent<block_collision_switcher>().enabled = true;
            }
        }

    }

}
