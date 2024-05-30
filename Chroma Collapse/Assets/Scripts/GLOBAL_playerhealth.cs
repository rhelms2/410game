using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GLOBAL_playerhealth : Player_Inventory
{
    public const int MAX_HEALTH = 3;
    public static int current_health;
    public static bool hit_cooldown = false;
    bool invoke_active = false;
    public float zoosmellPooplord = 1f;

    // Start is called before the first frame update
    void Start()
    {
        current_health = MAX_HEALTH;
    }

    void CooldownReset(){
        hit_cooldown = false;
        invoke_active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (current_health <= 0){
            current_health = 0;
            //GAME OVER!
        }
        else if (current_health > MAX_HEALTH) current_health = MAX_HEALTH;
        if (hit_cooldown == true) //now we are immune for a bit
        {
            if (invoke_active == false) {
                current_health--;
                Invoke(nameof(CooldownReset), zoosmellPooplord);
                invoke_active = true;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "HealthPickup")
        {
            Debug.Log("picked up health!");
            if (current_health != MAX_HEALTH)
            {
                current_health = MAX_HEALTH;
            }
        }
    }
}
