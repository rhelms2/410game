using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    [SerializeField] protected int damage_amt;
    [SerializeField] protected int knockback_amt;
    
    void Awake() {
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {

            Debug.Log("Player entering Enemy collider!");

            // Apply knockback force and damage to player

            Vector3 direction = -transform.forward;
            direction.y = 1;

            Player_Singleton.player_instance.ApplyForce(direction * knockback_amt);
            Player_Singleton.player_instance.ChangeCurrentHealth(-damage_amt);
        }
    }
}