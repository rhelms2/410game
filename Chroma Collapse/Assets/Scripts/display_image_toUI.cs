using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class display_image_to_ui : MonoBehaviour
{
    public Sprite image;
    public string optional_overlay_text;
    private TextMeshProUGUI text_target;
    private Image target_display;
    public AudioSource sound_effect;
    private bool in_range = false;
    private bool image_active = false;
    public bool automatic = false;

    void Start() {
        target_display = GameObject.FindWithTag("Display Port").transform.GetChild(1).gameObject.GetComponent<Image>();
        text_target = GameObject.FindWithTag("Display Port").transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
        if (automatic) {
            target_display.sprite = image;
            target_display.sprite = image;
            text_target.text = optional_overlay_text;
            target_display.color = new Color(1, 1, 1, 1);
        }
    }

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
            if (image_active) {
                image_active = false;
                text_target.text = "";
                target_display.color = new Color(1, 1, 1, 0);
                sound_effect.Play();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (in_range) {
            if (!image_active && Input.GetKeyDown("f")) {
                image_active = true;
                target_display.sprite = image;
                text_target.text = optional_overlay_text;
                target_display.color = new Color(1, 1, 1, 1);
                sound_effect.Play();
            }
            else if (image_active && Input.GetKeyDown("f")) {
                image_active = false;
                text_target.text = "";
                target_display.color = new Color(1, 1, 1, 0);
                sound_effect.Play();
            }
        }
        
    }
}
