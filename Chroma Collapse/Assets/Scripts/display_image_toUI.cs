using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class display_image_to_ui : MonoBehaviour
{
    public Sprite image;
    public Image target_display;
    private bool in_range = false;
    private bool image_active = false;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the other collider belongs to the player object
        if (other.CompareTag("Player"))
        {
            in_range = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the other collider belongs to the player object
        if (other.CompareTag("Player"))
        {
            in_range = false;
            image_active = false;
            target_display.color = new Color(1, 1, 1, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (in_range) {
            if (!image_active && Input.GetKeyDown("f")) {
                image_active = true;
                target_display.sprite = image;
                target_display.color = new Color(1, 1, 1, 1);
            }
            else if (image_active && Input.GetKeyDown("f")) {
                image_active = false;
                target_display.color = new Color(1, 1, 1, 0);
            }
        }
    }
}
