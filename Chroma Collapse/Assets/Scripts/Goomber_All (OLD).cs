using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomber_All : GLOBAL_playerhealth
{
    private GameObject player;
    //https://www.youtube.com/watch?v=BGe5HDsyhkY
    float x, y, z, counter, angle, activeX, activeZ;
    Vector3 origVect;
    public int defense_color;
    public int knockback_force = 25;
    public int damage_color;
    public float moveSpeed = 2f;
    //https://www.youtube.com/watch?v=4Wh22ynlLyk
    Vector3 dir;
    private Rigidbody rb;
    Coroutine turning = null;
    public color_enum myCol;
    bool isOn = false;
    //NOTE: this is super cheap, but if you have the object floating
    //slightly above the ground the script below and the rigidbody
    //will make it jitter like it's walking around

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
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
        if (player.transform == null) {
            //Debug.Log("PLAYER IS NULL!!!!!!");
        }
        else {
            //Debug.Log("PLAYER IS NOT NULL!!!!!!");
        }
        if (Vector3.Distance(player.transform.position, transform.position) < 2f){
            if (color == defense_color) {
            }
            else {
                if (hit_cooldown == false) hit_cooldown = true;

                // Knock the player away
                Vector3 direction = (transform.position - player.transform.position).normalized;
                direction.y = 0.5f;
                player.transform.parent.GetComponent<Rigidbody>().AddForce(direction * knockback_force);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindWithTag("Player");
        //if ((int)myCol == color) isOn = false;
        //else 
        isOn = true;
        if (isOn) checkCollision();
        counter += Time.deltaTime;
        if ((Vector3.Distance(player.transform.position, transform.position) < 10f) && isOn){
            //In radius, follow the player
            if (turning == null){
                Vector3 looking = player.transform.position;
                looking.y = transform.position.y;
                StartCoroutine(RoutineTurn(transform, looking, 1f));
            }
            dir = player.transform.position - transform.position;
            dir.Normalize();
            activeX += dir.x * Time.deltaTime * moveSpeed;
            activeZ += dir.z * Time.deltaTime * moveSpeed;
            x = activeX;
            z = activeZ;
            //Tip: if you want it to follow you and then circle in the place
            //you left its radius, use origVect or remove the following check
        } else { 
            if ((activeX != origVect.x) || (activeZ != origVect.z)){
                if (turning == null) {
                    StartCoroutine(RoutineTurn(transform, origVect, 1f));
                }
                dir = origVect - transform.position;
                dir.Normalize();
                activeX += dir.x * Time.deltaTime * moveSpeed;
                activeZ += dir.z * Time.deltaTime * moveSpeed;
                x = activeX;
                z = activeZ;
                //Snap to origin
                if (Mathf.Abs(activeX - origVect.x) <= 1) activeX = origVect.x;
                if (Mathf.Abs(activeZ - origVect.z) <= 1) activeZ = origVect.z;
            } else { //SPIN!!!
                x = activeX + Mathf.Cos((counter*4)+1) * 2;
                z = activeZ + Mathf.Sin(counter*4) * 2;
                //perhaps it looks a little silly
                if (turning == null){
                    StartCoroutine(RoutineTurn(transform, new Vector3(
                        origVect.x + Random.Range(-50, 50), 0, origVect.z + 
                        Random.Range(-50, 50)), 1f));
                }
            }
        }
        transform.position = new Vector3(x, y, z);
    }
}
