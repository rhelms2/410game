using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zorb_All : GLOBAL_playerhealth
{
    public GameObject player;
    public GameObject walls;
    //https://www.youtube.com/watch?v=BGe5HDsyhkY
    float x, y, z, counter, angle, activeX, activeZ;
    Vector3 origVect;
    public float moveSpeed = 10f;
    //https://www.youtube.com/watch?v=4Wh22ynlLyk
    Vector3 dir;
    private Rigidbody rb;
    Coroutine turning = null;
    public color_enum myCol;
    bool isOn = false;
    bool wallhit = true;
    //NOTE: this is super cheap, but if you have the object floating
    //slightly above the ground the script below and the rigidbody
    //will make it jitter like it's walking around

    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
        origVect = transform.position;
        activeX = origVect.x;
        activeZ = origVect.z;
        y = origVect.y;
        rb = this.GetComponent<Rigidbody>();
    }

    //I took this from project 2
    IEnumerator RoutineTurn(Transform obj, Vector3 pos, float duration) {
        Quaternion current = obj.rotation;
        Quaternion newrot = Quaternion.LookRotation(pos - obj.position, obj.TransformDirection(Vector3.up));
        float counter = 0;
        while (counter < duration){
            counter += Time.deltaTime;
            obj.rotation = Quaternion.Lerp(current, newrot, counter / duration);
            yield return null;
        }
    }

    void checkCollision(){
        if (Vector3.Distance(player.transform.position, transform.position) < 2f){
            if (hit_cooldown == false) hit_cooldown = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ((int)myCol == color) isOn = true;
        else isOn = false;
        if (isOn) checkCollision();
        counter += Time.deltaTime;
        if ((Vector3.Distance(player.transform.position, transform.position) < 10f) && isOn){
            //In radius, follow the player
            if (wallhit){
                if (turning == null){
                    Vector3 looking = player.transform.position;
                    looking.y = transform.position.y;
                    StartCoroutine(RoutineTurn(transform, looking, 1f));
                }
                wallhit = false;
                dir = player.transform.position - transform.position;
                dir.Normalize();
            }
            //Tip: if you want it to follow you and then circle in the place
            //you left its radius, use origVect or remove the following check
        } else if (wallhit) { 
            if ((activeX != origVect.x) || (activeZ != origVect.z)){
                if (turning == null) {
                    StartCoroutine(RoutineTurn(transform, origVect, 1f));
                }
                dir = origVect - transform.position;
                dir.Normalize();
                activeX += dir.x * Time.deltaTime * moveSpeed * 0.7f;
                activeZ += dir.z * Time.deltaTime * moveSpeed * 0.7f;
                x = activeX;
                z = activeZ;
                //Snap to origin
                if (Mathf.Abs(activeX - origVect.x) <= 1) activeX = origVect.x;
                if (Mathf.Abs(activeZ - origVect.z) <= 1) activeZ = origVect.z;
            } else { //SPIN!!!
                x = activeX + Mathf.Cos((counter*3)+7) * 1;
                z = activeZ + Mathf.Sin(counter*4-2) * 2;
                //perhaps it looks a little silly
                if (turning == null){
                    StartCoroutine(RoutineTurn(transform, new Vector3(
                        origVect.x + Random.Range(-50, 50), 0, origVect.z + 
                        Random.Range(-50, 50)), 1f));
                }
            }
        }
        else { // he is running
            activeX += dir.x * Time.deltaTime * moveSpeed;
            activeZ += dir.z * Time.deltaTime * moveSpeed;
            x = activeX;
            z = activeZ;
        }
        //transform.position = new Vector3(x, y, z);
        rb.MovePosition(new Vector3(x, y, z));
    }
}
