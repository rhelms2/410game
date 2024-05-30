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
        player = GameObject.FindWithTag("Player");

    }

    public void GameOver()
    {
        // gameOverUI.SetActive(true);
        Restart();
    }

    public void Restart()
    {
        Debug.Log("Current Player Object: " + player);
        if (inventory.Count > 0) {
            if (player.transform == null) {
                Debug.Log("Player is null ");
                player = GameObject.FindWithTag("Player").transform.parent.gameObject;  // Double check we have the right object... annoying
                player.transform.position = respawn_point.position;
                //player = GameObject.FindWithTag("Player").transform.parent.gameObject;  // Double check we have the right object... annoying
            }
            else {
                Debug.Log("Player transform is not null ");
                player = GameObject.FindWithTag("Player").transform.parent.gameObject;  // Double check we have the right object... annoying
                player.transform.position = respawn_point.position;           
            }
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
            current_health = MAX_HEALTH;
        }
        else {
            if (player.transform.position == null) {
                Debug.Log("Player is null ");
                player = GameObject.FindWithTag("Player").transform.parent.gameObject;  // Double check we have the right object... annoying
                player.transform.position = respawn_point.position;
                // player = GameObject.FindWithTag("Player").transform.parent.gameObject;  // Double check we have the right object... annoying
            }
            else {
                player = GameObject.FindWithTag("Player").transform.parent.gameObject;  // Double check we have the right object... annoying
                player.transform.position = respawn_point.position;
                Debug.Log("Player transform is not null ");
                //player = GameObject.FindWithTag("Player").transform.parent.gameObject;  // Double check we have the right object... annoying
                player.transform.position = respawn_point.position;
            }
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
            current_health = MAX_HEALTH;
        }
    }
}
