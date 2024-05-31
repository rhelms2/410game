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

    public string optional_overlay_text;
    private TextMeshProUGUI text_target;
    private Image target_display;
    private bool in_range = false;
    private bool fPressed = false;

    //public GameObject platform;
    //public GameObject pedestal;
    //public GameObject item;
    //private Vector3 deltaPedestalPosition = new Vector3(0f, 2.5f, 0f);
    //private Vector3 targetPedestalPosition;
    //private Vector3 targetItemPosition;
    //private Vector3 targetPlatformPosition;
    //public float platformSpeed = 2f;
    //private bool platformMoving = false;
    //private bool pedestalMoving = false;
    //private bool keyCardMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        //targetPlatformPosition = new Vector3(platform.transform.position.x, platform.transform.position.y, platform.transform.position.z - 2.7f);
        //targetPedestalPosition = pedestal.transform.position + deltaPedestalPosition;
        //targetItemPosition = item.transform.position + deltaPedestalPosition;

        target_display = GameObject.FindWithTag("Display Port").transform.GetChild(1).gameObject.GetComponent<Image>();
        text_target = GameObject.FindWithTag("Display Port").transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (pressed == true)
        {
            time_passed += Time.deltaTime;
            if (time_passed >= 0.5f)
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
                Debug.Log("z" + z);
                if (z < 13 && z > 7)
                {
                    Debug.Log("win!");
                    movePedestal.platformMoving = true;
                }
                active = false;
            }
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        // Check if the other collider belongs to the player object
        if (other.CompareTag("Player"))
        {
            in_range = true;
            if (Input.GetKeyDown("f"))
            {
                active = true;
            }
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

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Bullet")
        {
            if (pressed == false)
            {
                pressed = true;
                //button.transform.localPosition -= deltaButtonPosition;
                //if (!active)
                //{
                //    ball = Instantiate(ballPrefab, ballSpawner.transform.position, ballSpawner.transform.rotation);
                //    ball.transform.parent = ballSpawner.transform;
                //    active = true;
                //}

            }

        }
    }

    private void makeBall()
    {
        button.transform.localPosition -= deltaButtonPosition;
        if (!active)
        {
            ball = Instantiate(ballPrefab, ballSpawner.transform.position, ballSpawner.transform.rotation);
            ball.transform.parent = ballSpawner.transform;
            active = true;
        }
    }

    //IEnumerator ballCheck()
    //{
    //    while (active)
    //    {
    //        //Debug.Log("Coroutine started");
    //        //lastPosition = ball.transform.position;
    //        //yield return new WaitForEndOfFrame();
    //        //Debug.Log("After WaitForEndOfFrame");

    //        //Debug.Log("Current Position: " + ball.transform.position);
    //        //Debug.Log("Last Position: " + lastPosition);

    //        //ballSpeedVector = (ball.transform.position - lastPosition);
    //        //ballSpeed = ballSpeedVector.magnitude / Time.deltaTime;
    //        //Debug.Log("Ball Speed: " + ballSpeed);



    //        yield return null; // Ensure the coroutine runs continuously
    //    }
    //}



}
