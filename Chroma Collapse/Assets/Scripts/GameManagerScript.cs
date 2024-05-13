using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : Player_Inventory
{
    private static GameManagerScript instance;
    public GameObject player;
    public Transform respawn_point;

    //from https://www.youtube.com/watch?v=pKFtyaAPzYo

    public GameObject gameOverUI;
    // Start is called before the first frame update
    void Start()
    {
        gameOverUI.SetActive(false);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        gameOverUI.SetActive(true);
    }

    public void Restart()
    {
        if (inventory.Count > 0) {
            player.transform.position = respawn_point.position;
        }
        else {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
        }
    }
}
