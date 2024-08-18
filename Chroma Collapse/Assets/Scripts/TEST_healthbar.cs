using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_healthbar : MonoBehaviour
{
    [SerializeField] int health_modifier;

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            Debug.Log("Modifying player health");
            Player_Singleton.player_instance.ChangeCurrentHealth(health_modifier); 
        }
    }
}
