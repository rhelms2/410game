using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Goonber_AI : BaseEnemy
{

    [SerializeField] SpriteRenderer sprite;
    [SerializeField] int special_color, health, idle_frames_limit;
    [SerializeField] LayerMask groundLayer, playerLayer;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] float sightRange, patrol_distance, patrol_speed, follow_speed;
    
    // Sets how far the enemy can stray from the patrol point when in patrol state

    Vector3 patrolpoint;
    Vector3 walkpoint;

    Animator animator;

    enum state_enum {
        idle,
        patrol,
        follow,
    };

    // These are all variables needed for the internal logic here

    int state = (int) state_enum.patrol;
    int counter = 0;
    int counter_end;
    bool walking;
    bool walkPointSet;
    bool output_damage;

    void Awake() {
        sprite.color = GLOBAL_color.color_array[special_color];

        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // Set the patrol station to the enemy's spawnpoint
        patrolpoint = transform.position;
        Debug.Log("Goonber patrolpoint: " + patrolpoint);
    }

    void Update() {

        if (walking) {
            // Play walking animation sequence
            animator.SetBool("Walk", true);
        }
        else {
            // Stop walking animation sequence
            animator.SetBool("Walk", false);
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
                agent.isStopped = false;
                agent.speed = patrol_speed;

                CheckPatrolPointDist();

                if (!walkPointSet) SetRandPatrolPoint();
                if (walkPointSet) agent.SetDestination(walkpoint);

                Vector3 distanceToWalkPoint = transform.position - walkpoint;
                float distance = distanceToWalkPoint.magnitude;

                Debug.Log("Goonber's distance to next walkpoint: " + distance);

                if (distance < 1.5f) {
                    walkPointSet = false;
                    ChangeState((int) state_enum.idle);
                    SetIdleTimer();
                }
                break;

            case (int) state_enum.follow:

                walking = true;
                agent.isStopped = false;
                agent.speed = follow_speed;
                agent.SetDestination(Player_Singleton.player_instance.GetPlayerPosition());
                break;

            case (int) state_enum.idle:

                agent.isStopped = true;
                agent.ResetPath();
                walking = false;

                counter++;
                if (counter >= counter_end) {
                    ChangeState((int) state_enum.patrol);
                }
                break;
        }
    }

    void CheckPlayerDistance() {
        if (Physics.CheckSphere(transform.position, sightRange, playerLayer)) {
            Debug.Log("Player is in sight range of Goonber!");
            ChangeState((int) state_enum.follow);
        }
        else if (state == (int) state_enum.follow) {
            Debug.Log("Player is out of range of Goonber");
            ChangeState((int) state_enum.idle);
            SetIdleTimer();
        }
    }

    void CheckPatrolPointDist() {
        // If goonber has strayed too far, it will return to the initial patrol post

        if (Vector3.Distance(transform.position, patrolpoint) > patrol_distance) {
            Debug.Log("Goonber out of patrol range. Returning to original post");
            Debug.Log("Goonber patrolpoint: " + patrolpoint);
            walkpoint = patrolpoint;
            walkPointSet = true;
        }
    }

    void SetRandPatrolPoint() {
        // Calculate random point in range
        float randomZ = UnityEngine.Random.Range(-patrol_distance, patrol_distance);
        float randomX = UnityEngine.Random.Range(-patrol_distance, patrol_distance);

        walkpoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkpoint, -transform.up, 2f, groundLayer)) walkPointSet = true;
    }

    void ChangeState(int newstate) {
        // This method is made mostly for debugging purposes...
        Debug.Log("Changing Goonber state from: " + Enum.GetName(typeof(state_enum), state) + " to: " + Enum.GetName(typeof(state_enum), newstate));

        state = newstate;
    }

    void SetIdleTimer() {
        counter = 0;
        counter_end = UnityEngine.Random.Range(0, idle_frames_limit);
        Debug.Log("Goonber idle timer set to end in " + counter_end + " frames");
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {

            Debug.Log("Player entering Enemy collider!");

            if (output_damage) {
                // Apply knockback force and damage to player

                Vector3 direction = -transform.forward;
                direction.y = Math.Max(Math.Abs(direction.x), Math.Abs(direction.z));   // Set this to a different value in case it is 0

                Player_Singleton.player_instance.ApplyForce(direction * knockback_amt);
                Player_Singleton.player_instance.ChangeCurrentHealth(-damage_amt);
            }
        }

        if (other.tag == "Bullet") {
            Debug.Log("Bullet entering Enemy collider!");

            if (output_damage) {
                // Trigger enemy knockback or flash, subtract hp or destroy object
            }
        }
    }
}