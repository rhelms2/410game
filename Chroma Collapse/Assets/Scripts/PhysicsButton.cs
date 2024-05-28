using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhysicsButton : MonoBehaviour
{
    public GameObject button;
    public GameObject ballPrefab;
    public GameObject ballSpawner;
    public GameObject platform;
    public GameObject pedestal;
    public GameObject keyCard;
    private GameObject ball;
    private Vector3 deltaButtonPosition = new Vector3(0f, 0.05f, 0f);
    private Vector3 deltaPedestalPosition = new Vector3(0f, 2.5f, 0f);
    private Vector3 targetPedestalPosition;
    private Vector3 targetKeyCardPosition;
    private Vector3 targetPlatformPosition;
    public float platformSpeed = 2f;
    private float time_passed = 0;
    private bool pressed = false;
    private bool active = false;
    private bool platformMoving = false;
    private bool pedestalMoving = false;
    private bool keyCardMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        targetPlatformPosition = new Vector3(platform.transform.position.x, platform.transform.position.y, platform.transform.position.z - 2.7f);
        targetPedestalPosition = pedestal.transform.position + deltaPedestalPosition;
        targetKeyCardPosition = keyCard.transform.position + deltaPedestalPosition;
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
                if (z < 12.5 && z > 7.5)
                {
                    Debug.Log("win!");
                    platformMoving = true;
                }
                active = false;
            }
        }

        if (platformMoving)
        {
            platform.transform.position = Vector3.MoveTowards(platform.transform.position, targetPlatformPosition, platformSpeed * Time.deltaTime);
            if (platform.transform.position == targetPlatformPosition)
            {
                platformMoving = false;
                pedestalMoving = true;
                keyCardMoving = true;
            }
        }

        if (pedestalMoving)
        {
            pedestal.transform.position = Vector3.MoveTowards(pedestal.transform.position, targetPedestalPosition, platformSpeed * Time.deltaTime);
            if (pedestal.transform.position == targetPedestalPosition)
            {
                pedestalMoving = false;
            }
        }

        if (keyCardMoving)
        {
            keyCard.transform.position = Vector3.MoveTowards(keyCard.transform.position, targetKeyCardPosition, platformSpeed * Time.deltaTime);
            if (keyCard.transform.position == targetKeyCardPosition)
            {
                keyCardMoving = false;
            }
        }




    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Bullet")
        {
            if (pressed == false)
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
