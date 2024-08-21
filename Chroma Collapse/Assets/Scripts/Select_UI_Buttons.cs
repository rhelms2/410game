using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Select_UI_Buttons : MonoBehaviour
{
    public GameObject[] buttons;
    int button_index;
    PlayerControls controls_instance;

    // Start is called before the first frame update
    void Start()
    {
        button_index = 0;
        buttons[0].GetComponent<Button>().Select();
        controls_instance = GameManager.controls;
    }

    // Update is called once per frame
    void Update()
    {
        // buttons[button_index].GetComponent<Button>().Select();

        if (controls_instance.Gameplay.Activate.WasPressedThisFrame()) {
            buttons[button_index].GetComponent<Button>().onClick.Invoke();
        }
        
        // This works for buttons that are arranged such that the button at the first index is on top, 2nd below that, etc.

        float direction = controls_instance.Gameplay.Move.ReadValue<Vector2>().y;
        if (direction < 0) {
            button_index++;
            if (button_index > buttons.Length - 1) {
                button_index = buttons.Length - 1;
            }
            buttons[button_index].GetComponent<Button>().Select();
        }
        else if (direction > 0) {
            button_index--;
            if (button_index < 0) {
                button_index = 0;
            }
            buttons[button_index].GetComponent<Button>().Select();
        }
    }
}
