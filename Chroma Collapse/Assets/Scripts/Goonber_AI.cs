using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Goonber_AI : BaseEnemy
{

    [SerializeField] SpriteRenderer sprite;
    [SerializeField] int special_color;
    [SerializeField] int health;
    [SerializeField] LayerMask groundLayer, playerLayer;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] float sightRange;

    // Sets how far the enemy can stray from the patrol point when in patrol state
    [SerializeField] float patrol_distance;
    Vector3 patrolpoint;
    Vector3 walkpoint;

    enum state_enum {
        idle,
        patrol,
        follow,
        returning
    };

    int state = (int) state_enum.patrol;

    bool walking;
    bool output_damage;

    void Awake() {
        sprite.color = GLOBAL_color.color_array[special_color];

        agent = GetComponent<NavMeshAgent>();

        // Set the patrol station to the enemy's spawnpoint
        patrolpoint = transform.position;
    }

    void Update() {

        if (walking) {
            // Play walking animation sequence
        }
        else {
            // Stop walking animation sequence
        }

        if (GLOBAL_color.color == special_color) {
            output_damage = true;
        }
        else {
            output_damage = false;
        }

        CheckPlayerDistance();

        switch(state) {

            case (int) state_enum.patrol:
                walking = true;
                // Set a patrol point, set state to idle once in range for a set amount of time, then change state to patrol and repeat
                break;

            case (int) state_enum.follow:
                walking = true;
                agent.SetDestination(Player_Singleton.player_instance.GetPlayerPosition());
                break;

            case (int) state_enum.returning:
                walking = true;
                agent.SetDestination(patrolpoint);

                if (Vector3.Distance(transform.position, patrolpoint) < 1f) {
                    state = (int) state_enum.patrol;
                }

                break;

            case (int) state_enum.idle:
                walking = false;
                break;
        }
    }

    void CheckPlayerDistance() {
        if (Physics.CheckSphere(transform.position, sightRange, playerLayer)) {
            Debug.Log("Player is in sight range of Goonber!");
            state = (int) state_enum.follow;
        }
        else if (state == (int) state_enum.follow) {
            Debug.Log("Player is out of range of Goonber");
            state = (int) state_enum.returning;
        }
        else {
            state = (int) state_enum.patrol;
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {

            Debug.Log("Player entering Enemy collider!");

            if (output_damage) {
                // Apply knockback force and damage to player

                Vector3 direction = -transform.forward;
                direction.y = 1;

                Player_Singleton.player_instance.ApplyForce(direction * knockback_amt);
                Player_Singleton.player_instance.ChangeCurrentHealth(-damage_amt);
            }
        }
    }
}