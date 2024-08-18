using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_bob : MonoBehaviour
{
    public Animator camanim;

    void Update() {
        if (Player_Movement.walking) {
            camanim.enabled = true;
            camanim.SetTrigger("Walk");
        }
        else {
            camanim.SetTrigger("Idle");
            camanim.enabled = false;
        }
    }
}