using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : GLOBAL_playerhealth
{
    private static GameManagerScript instance;
    private GameObject player;
    private Transform respawn_point;

    //from https://www.youtube.com/watch?v=pKFtyaAPzYo

    public GameObject gameOverUI;
    
    // Start is called before the first frame update
    void Start()
    {
        respawn_point = GameObject.FindWithTag("Respawn Point").transform;
        player = GameObject.FindWithTag("Player").transform.parent.gameObject;
        gameOverUI.SetActive(false);   
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindWithTag("Player").transform.parent.gameObject;

    }

    public void GameOver()
    {
        // gameOverUI.SetActive(true);
        Restart();
    }

    public void Restart()
    {
        // Debug.Log("Current Player Object: " + player);
        if (player.transform == null) {
            // Debug.Log("Player is null ");
        }
        else {
            // Debug.Log("Player transform is not null ");           
        }
        // player.transform.position = respawn_point.position;      
        // Debug.Log("Reloading Scene and setting player to spawn position");
        // int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        // SceneManager.LoadScene(currentSceneIndex);
        current_health = MAX_HEALTH;
        // Debug.Log("Old Player Position: " + player.transform.position);
        // Debug.Log("Respawn Position: " + respawn_point.position);
        player.transform.position = respawn_point.position;
        // Debug.Log("New Player Position: " + player.transform.position);
        player.transform.rotation = respawn_point.rotation;
            
        // Debug.Log("current_health: " + current_health + " MAX HEALTH: " + MAX_HEALTH);
    }
}
