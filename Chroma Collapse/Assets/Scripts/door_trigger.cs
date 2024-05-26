using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class door_trigger : MonoBehaviour
{
    protected bool trigger_hit;
    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            trigger_hit = true;
        }
    }

    public bool get_state() {
        return trigger_hit;
    }

    public void set_state(bool state) {
        trigger_hit = false;
    }
}
