using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
// using UnityEditor.SearchService;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.UI;

public class Scene_Transition : MonoBehaviour
{
    [SerializeField] 
    private int spawn_index;

    [SerializeField]
    private string scene_name;
    
    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            Debug.Log("Scene Transition method triggered");
            GameManager.instance.Load_New_Scene(scene_name, spawn_index); 
        }
    }
    
}
