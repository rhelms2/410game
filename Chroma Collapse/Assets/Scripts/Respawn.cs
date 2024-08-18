using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour
{
    private string scene_name;
    private int index;

    void Awake() {
        scene_name = SceneManager.GetActiveScene().name;
    
        // Get the parent Transform
        Transform parentTransform = transform.parent;

        if (parentTransform != null)
        {
            // Iterate through the parent's children
            for (int i = 0; i < parentTransform.childCount; i++)
            {
                if (parentTransform.GetChild(i) == transform)
                {
                    index = i; 
                    break;
                }
            }
        }
        else
        {
            Debug.LogWarning("This GameObject has no parent.");
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            GameManager.instance.SetRespawn(scene_name, index);
        }
    }
}
