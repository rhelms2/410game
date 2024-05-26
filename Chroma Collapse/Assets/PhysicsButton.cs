using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhysicsButton : MonoBehaviour
{
    public GameObject button;
    public GameObject ballPrefab;
    public GameObject ballSpawner;
    private GameObject ball;
    private Vector3 delta_position = new Vector3(0f, 0.05f, 0f);
    private Vector3 lastPosition;
    private Vector3 ballSpeedVector;
    private float ballSpeed;
    private float time_passed = 0;
    private bool pressed = false;
    private bool active = false;

    // Start is called before the first frame update
    void Start()
    {
        lastPosition = ballSpawner.transform.position;
    }

    void Update()
    {
        
        if (pressed == true)
        {
            Debug.Log(time_passed);
            time_passed += Time.deltaTime;
            if (time_passed >= 0.5f)
            {
                pressed = false;
                time_passed = 0;
                button.transform.localPosition += delta_position;
            }

        }
        
    }

    IEnumerator ballCheck()
    {
        while (active)
        {
            Debug.Log("Coroutine started");
            lastPosition = ball.transform.position;
            yield return new WaitForEndOfFrame();
            Debug.Log("After WaitForEndOfFrame");

            Debug.Log("Current Position: " + ball.transform.position);
            Debug.Log("Last Position: " + lastPosition);

            ballSpeedVector = (ball.transform.position - lastPosition) / Time.deltaTime;
            ballSpeed = ballSpeedVector.magnitude;
            Debug.Log("Ball Speed: " + ballSpeed);

            if (Mathf.Approximately(ballSpeed, 0f) && ball.transform.localPosition.y < -18.5f)
            {
                Debug.Log("Destroying Ball");
                Destroy(ball, 0f);
                active = false;
                lastPosition = ballSpawner.transform.position;
            }

            yield return null; // Ensure the coroutine runs continuously
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Bullet")
        {
            Debug.Log("Collided");
            if (pressed == false){
                pressed = true;
                button.transform.localPosition -= delta_position;
                Debug.Log("changed position of button");
                if (!active)
                {
                    ball = Instantiate(ballPrefab, ballSpawner.transform.position, ballSpawner.transform.rotation);
                    ball.transform.parent = ballSpawner.transform;
                    lastPosition = ball.transform.position;
                    active = true;
                    StartCoroutine(ballCheck());
                }
                
            }
            
        }
    }

}
