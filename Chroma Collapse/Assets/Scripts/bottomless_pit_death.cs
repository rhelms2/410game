using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class insta_death : GameManagerScript
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the other collider belongs to the player object
        if (other.CompareTag("Player"))
        {
            // Do something when player enters the collider
            Restart();
        }
    }
}
