using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

public class LightButton : MonoBehaviour
{
    public GameObject button;
    public MovePedestal movePedestal;
    public LightLogic lightLogic;
    public string overlay_text;

    private bool pressed = false;
    private Vector3 deltaButtonPosition = new Vector3(0f, 0.05f, 0f);
    private TextMeshProUGUI text_target;
    private bool in_range = false;
    private bool fPressed = false;


    // Start is called before the first frame update
    void Start()
    {
        //targetPlatformPosition = new Vector3(platform.transform.position.x, platform.transform.position.y, platform.transform.position.z - 2.7f);
        //targetPedestalPosition = pedestal.transform.position + deltaPedestalPosition;
        //targetItemPosition = item.transform.position + deltaPedestalPosition;

        text_target = GameObject.FindWithTag("Display Port").transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();

    }

    void Update()
    {
        if (lightLogic.finished)
        {
            if (lightLogic.win)
            {
                movePedestal.platformMoving = true;
            }
            button.transform.localPosition += deltaButtonPosition;
            lightLogic.finished = false;
            pressed = false;
            lightLogic.win = true;
        }

        if (in_range && !fPressed)
        {
            text_target.text = overlay_text;
        }

        if (Input.GetKeyDown("f") && !pressed && !lightLogic.active)
        {
            pressed = true;
            lightLogic.active = true;
            lightLogic.actualLight.intensity = 1000;
            button.transform.localPosition -= deltaButtonPosition;
            if (!fPressed)
            {
                fPressed = true;
                text_target.text = "";
            }
        }

    }


    void OnTriggerEnter(Collider other)
    {
        // Check if the other collider belongs to the player object
        if (other.CompareTag("Player"))
        {
            in_range = true;

        }
    }

    void OnTriggerExit(Collider other)
    {
        // Check if the other collider belongs to the player object
        if (other.CompareTag("Player"))
        {
            in_range = false;
        }
    }

}
