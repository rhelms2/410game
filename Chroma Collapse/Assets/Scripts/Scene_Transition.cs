using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
// using UnityEditor.SearchService;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.UI;

public class Scene_Transition : GLOBAL_color
{
    
    // [SerializeField] private int scene_index;

    [SerializeField]
    private string scene_name;
    
    [SerializeField]
    private Vector3 spawn_position;

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            // Debug.Log("Scene to load: " + SceneManager.GetSceneByName(scene_name));
            other.transform.parent.parent.GetComponent<Player>().FadeOut();
            SceneManager.LoadSceneAsync(scene_name, LoadSceneMode.Single);
            // SceneManager.MoveGameObjectToScene(other.transform.root.gameObject, scene);
            // SceneManager.LoadSceneAsync(scene.name, LoadSceneMode.Single);
            other.transform.parent.position = spawn_position;
            // Debug.Log("Player transferred to new scene");
            color = 0;
        }
    }
    
}
