using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class GLOBAL_playerhealth : GLOBAL_color
{
    public const int MAX_HEALTH = 3;
    public float flash_speed = 15;
    public static int current_health;
    public static bool hit_cooldown = false;
    bool invoke_active = false;
    public float zoosmellPooplord = 1f;
    public AudioSource hurt;

    public RawImage display;

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
                hurt.Play();
                StartCoroutine(Screen_flash());
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

    IEnumerator Screen_flash() {

        float currentTime = 0;

        while (display.color != Color.red) {
            display.color = Color.Lerp(Color.white, Color.red, currentTime += (Time.deltaTime * flash_speed / 1));
            // Debug.Log("In part 1 of screen_flash, screen should be turning red");
            yield return null;
        }
        currentTime = 0;
        while (display.color != Color.white) {
            display.color = Color.Lerp(Color.red, Color.white, currentTime += (Time.deltaTime * flash_speed / 1));
            // Debug.Log("In part 2 of screen_flash, screen should be turning back to normal");
            yield return null;
        }

        // Debug.Log("Outside whiles");
        // yield return new WaitForSeconds(0.1f);
    }
}
