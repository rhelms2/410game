using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This script is for making sure only one instance of the player is present
// when moving between scenes

public class Player : GLOBAL_color
{
    public static Player player_instance;
    RawImage display;

    // Start is called before the first frame update

/*
    void Awake()
    {
        if (player_instance != null) {
            Destroy(this.gameObject);
            return;
        } 
    
        // If this is the first instance of the player, then we set this as the static
        // instance. Otherwise we delete it from the scene
        
        player_instance = this;        
        GameObject.DontDestroyOnLoad(this.gameObject);
    }

*/


    void Start()
    {
        if (player_instance != null) {
            Destroy(this.transform.GetChild(0).gameObject);    // Destroy the player as well... not happening for some reason
            Destroy(this.gameObject);
            return;
        } 
    
        // If this is the first instance of the player, then we set this as the static
        // instance. Otherwise we delete it from the scene
        
        player_instance = this;        
        GameObject.DontDestroyOnLoad(this.gameObject);
    }


    public void Fade() {
        Debug.Log("IN FADE");
        RawImage display = GameObject.FindWithTag("Fade Target").GetComponent<RawImage>();

        float currentTime = 0;

        while (display.color != Color.black) {
            display.color = Color.Lerp(Color.white, Color.black, currentTime += (Time.deltaTime * 10f / 1));
            Debug.Log("SCREEN SHOULD BE TURNING BLACK NOW");
        }
        new WaitForSeconds(2);
        Debug.Log("WAITING");
        currentTime = 0;
        while (display.color != Color.white) {
            display.color = Color.Lerp(Color.black, Color.white, currentTime += (Time.deltaTime * 0.5f / 1));
            Debug.Log("SCREEN SHOULD BE TURNING WHITE NOW");
        }
    }




}
