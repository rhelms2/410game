using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class reset_level : GLOBAL_playerhealth
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the other collider belongs to the player object
        if (other.CompareTag("Player"))
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }
    }
}
