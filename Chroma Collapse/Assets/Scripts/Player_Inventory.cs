using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class Player_Inventory : GLOBAL_color
{
    protected static List<string> inventory = new List<string>();

    [SerializeField] private bool testing = false;
    private string ColorSwitcher_tag = "ColorSwitcher Pickup";
    private string red_crystal_tag = "Red Chip Pickup";
    private string yellow_crystal_tag = "Yellow Chip Pickup";
    private string blue_crystal_tag = "Blue Chip Pickup";
    private string gun_tag = "Gun Pickup";
    private string red_gun_chip = "Red Gun Chip";
    private string yellow_gun_chip = "Yellow Gun Chip";
    private string blue_gun_chip = "Blue Gun Chip";
    private string key_card = "Key Card";

    // This boolean is switched on by item pickups which triggers UpdateUI
    static protected bool inventory_changed = false;

    void Awake() {

        // If testing the game, activate all inventory slots
        if (testing) {
            inventory.Add(ColorSwitcher_tag);
            inventory.Add(red_crystal_tag);
            inventory.Add(yellow_crystal_tag);
            inventory.Add(blue_crystal_tag);
            /* 
            inventory.Add(gun_tag);
            inventory.Add(red_gun_chip);
            inventory.Add(yellow_gun_chip);
            inventory.Add(blue_gun_chip);
            inventory.Add(key_card);
            
            int i = 0;
            
            foreach (bool item in inventory_activation) {
                inventory_activation[i] = true;
                i++;
            }
            */

            for (int j = 0; j < 3; j++) {
                inventory_activation[j] = true;
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
            // Debug.Log("INVENTORY CHANGED");
            UpdateUI();
            inventory_changed = false;
        }
    }

    protected void UpdateUI() {

        GameObject ColorSwitcher = GameObject.FindWithTag("ColorSwitcher UI Object").transform.GetChild(1).gameObject;

        foreach (string tag in inventory) {

            Debug.Log("UpdateUI: Inventory tag: " + tag);
            // Debug.Log("ColorSwitcher tag: " + ColorSwitcher_tag);

            if (tag == ColorSwitcher_tag) {
                // sets the color switcher to active
                // Debug.Log("Color switcher location: " + ColorSwitcher);
                ColorSwitcher.SetActive(true);
            }
            else if (tag == red_crystal_tag) {
                
                // sets red spire active and allows control of red by the player

                ColorSwitcher.transform.GetChild(0).gameObject.GetComponent<Color_swap>().enabled = true;
                ColorSwitcher.transform.gameObject.GetComponent<ColorSwitcherRotation>().enabled = true;
                inventory_activation[0] = true;
                ColorSwitcher.transform.GetChild(1).GetChild(0).gameObject.GetComponent<block_collision_switcher>().enabled = true;
            }
            else if (tag == yellow_crystal_tag) {

                // sets yellow spire active and allows control of yellow by the player
                
                ColorSwitcher.transform.GetChild(0).gameObject.GetComponent<Color_swap>().enabled = true;
                ColorSwitcher.transform.gameObject.GetComponent<ColorSwitcherRotation>().enabled = true;
                inventory_activation[1] = true;
                ColorSwitcher.transform.GetChild(2).GetChild(0).gameObject.GetComponent<block_collision_switcher>().enabled = true;
            }
            else if (tag == blue_crystal_tag) {

                // sets blue spire active and allows control of blue by the player

                ColorSwitcher.transform.GetChild(0).gameObject.GetComponent<Color_swap>().enabled = true;
                ColorSwitcher.transform.gameObject.GetComponent<ColorSwitcherRotation>().enabled = true;
                inventory_activation[2] = true;
                ColorSwitcher.transform.GetChild(3).GetChild(0).gameObject.GetComponent<block_collision_switcher>().enabled = true;
            }
            else if (tag == gun_tag) {
                ColorSwitcher.transform.parent.GetChild(2).gameObject.SetActive(true);  // Gun location
            }
            else if (tag == red_gun_chip) {
                inventory_activation[3] = true;
            }
            else if (tag == yellow_gun_chip) {
                inventory_activation[4] = true;
            }
            else if (tag == blue_gun_chip) {
                inventory_activation[5] = true;
            }
            else if (tag == key_card) {
                inventory_activation[6] = true;
            }
        }

    }

}
