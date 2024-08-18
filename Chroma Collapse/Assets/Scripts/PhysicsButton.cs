using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

public class PhysicsButton : MonoBehaviour
{
    public GameObject button;
    public GameObject ballPrefab;
    public GameObject ballSpawner;
    private GameObject ball;
    private float time_passed = 0;
    private bool pressed = false;
    private bool active = false;
    public MovePedestal movePedestal;
    private Vector3 deltaButtonPosition = new Vector3(0f, 0.05f, 0f);

    public string overlay_text;
    private TextMeshProUGUI text_target;
    private bool in_range = false;
    private bool fPressed = false;
    private bool displaying = true;

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
        if (pressed == true)
        {
            time_passed += Time.deltaTime;
            if (time_passed >= 1f)
            {
                pressed = false;
                time_passed = 0;
                button.transform.localPosition += deltaButtonPosition;
            }

        }

        if (active)
        {
            if (ball.transform.localPosition.y < -18.5f)
            {
                float z = ball.transform.localPosition.z;
                if (z < 13 && z > 7)
                {
                    movePedestal.platformMoving = true;
                }
                active = false;
            }
        }

        if (in_range && !fPressed)
        {
            text_target.text = overlay_text;
            displaying = true;
        }
        if (GameManager.controls.Gameplay.Activate.WasPressedThisFrame() && !pressed)
        {
            makeBall();
            pressed = true;
            if (!fPressed)
            {
                fPressed = true;
                text_target.text = "";
            }
        }
        //if (!in_range && displaying)
        //{
        //    text_target.text = "";
        //}
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
        }
    }

    private void makeBall()
    {
        pressed = true;
        button.transform.localPosition -= deltaButtonPosition;
        if (!active)
        {
            ball = Instantiate(ballPrefab, ballSpawner.transform.position, ballSpawner.transform.rotation);
            ball.transform.parent = ballSpawner.transform;
            active = true;
        }
    }
}
