using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Inventory : GLOBAL_color
{
    public static List<GameObject> inventory = new List<GameObject>();
    public GameObject ColorSwitcher;
    public GameObject red_crystal;
    public GameObject yellow_crystal;
    public GameObject blue_crystal;
    public GameObject gun;
    static public bool inventory_changed = false;

    // Start is called before the first frame update
    void Start()
    {
        ColorSwitcher.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (inventory_changed) {
            UpdateUI();
            inventory_changed = false;
        }
    }

    void UpdateUI() {
        foreach (GameObject obj in inventory) {

            Debug.Log("Updating UI... current game object: " + obj);

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
