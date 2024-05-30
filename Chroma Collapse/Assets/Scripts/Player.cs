using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is for making sure only one instance of the player is present
// when moving between scenes

public class Player : MonoBehaviour
{
    public static Player player_instance;

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


}
