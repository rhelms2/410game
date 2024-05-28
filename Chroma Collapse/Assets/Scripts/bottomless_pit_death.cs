using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class insta_death : MonoBehaviour
{
    [SerializeField] private Transform respawn_point;
    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log("Something is in contact with pit collider");

        // Check if the other collider belongs to the player object
        if (other.CompareTag("Player"))
        {
            // Debug.Log("Player is in contact with pit collider");
            // Do something when player enters the collider
            other.transform.parent.position = respawn_point.position;
        }
    }
}
