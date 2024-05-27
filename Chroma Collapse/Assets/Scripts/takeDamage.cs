using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class takeDamage : GLOBAL_color
{
    public GameObject follow;
    public const int MAX_HEALTH = 1;
    public static int current_health;
    public static bool hit_cooldown = false;
    bool invoke_active = false;
    public float zoosmellPooplord = 1f;
    public color_enum damageCol;
    // Start is called before the first frame update
    void Start()
    {
        current_health = MAX_HEALTH;
    }

    void CooldownReset(){
        hit_cooldown = false;
        invoke_active = false;
    }

    void OnTriggerEnter(Collider other) {
        if (color == (int)damageCol){
            if (other.tag == "Bullet") {
                if (hit_cooldown == false) hit_cooldown = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = follow.transform.position;
        transform.rotation = follow.transform.rotation;
        if (current_health <= 0){
            current_health = 0;
            //Destroy!
            follow.transform.position = new Vector3(0, -1000, 0);
        }
        if (hit_cooldown == true) //now we are immune for a bit
        {
            if (invoke_active == false) {
                current_health--;
                Invoke(nameof(CooldownReset), zoosmellPooplord);
                invoke_active = true;
            }
        }
    }
}
