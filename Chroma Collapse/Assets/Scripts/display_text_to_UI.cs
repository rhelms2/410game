using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class display_text_to_UI : MonoBehaviour
{
    public string text;
    public bool destroy_on_exit = false;
    public TextMeshProUGUI display_target;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the other collider belongs to the player object
        if (other.CompareTag("Player"))
        {
            // Set the display target text to desired message
            display_target.text = text;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the other collider belongs to the player object
        if (other.CompareTag("Player")) {
            // Erase message on player exit
            display_target.text = "";
            
            if (destroy_on_exit) {
                Destroy(gameObject);
            }
        }
    }
}
