using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kill_player : MonoBehaviour
{
    public GameObject player;
    public Transform respawn_point;
    private void OnTriggerEnter(Collider other)
    {
        // Check if the other collider belongs to the player object
        if (other.CompareTag("Player"))
        {
            // Do something when player enters the collider
            player.transform.position = respawn_point.position;
            // Example: Disable the collider after the player enters it
            // gameObject.SetActive(false);
        }
    }
}
