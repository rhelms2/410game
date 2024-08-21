using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

/*
    This script is for making sure only one instance of the player is present
    when moving between scenes. It also contains methods used to set the player's state, position,
    and health
*/

public class Player_Singleton : MonoBehaviour 
{
    public static Player_Singleton player_instance;

    /* 
    Defining an enum for states. I'm only using this to disallow inputs when scene
    transtions are occuring for now, but it may be useful going forward? Note: this
    is accessed in the Player_Movement script
    */

    public enum state {normal, freeze, dead};
    private static int player_state;


    // Defining player health values. Hard coded for now...
    public static int MAX_HEALTH = 3;
    private static int CURR_HEALTH = MAX_HEALTH;


    // A method will be called from here when the player health is changed to update the image for the UI.
    // The healthbar class sets this instance of itself on start using SetHealthbarObject
    private static Healthbar healthbar = null;

    void Awake()
    {
        if (player_instance != null) {
            Debug.Log("Destroying player singleton instance");
            Destroy(this.gameObject);
            return;
        } 
    
        // If this is the first instance of the player, then we set this as the static
        // instance. Otherwise we delete it from the scene

        Debug.Log("Creating player singleton instance"); 
        player_instance = this;        
        GameObject.DontDestroyOnLoad(this.gameObject);
        SetPlayerState((int) state.normal);
    }

    // Getters and Setters for private static player variables
    public void SetPlayerState(int state) {
        player_state = state;
    }
    public int GetPlayerState() {
        return player_state;
    }

    public void SetPlayerPosition(UnityEngine.Vector3 new_pos) {
        Player_Movement.overlaps = 0;
        player_instance.transform.position = new_pos;
    }
    public void SetPlayerRotation(UnityEngine.Quaternion new_rot) {
        // Must set the child, orientation, here
        player_instance.transform.GetChild(0).rotation = UnityEngine.Quaternion.Euler(0, new_rot.y, 0);
    }
    public UnityEngine.Vector3 GetPlayerPosition() {
        return player_instance.transform.position;
    }

// ------------------------------------------------

// Health logic
    public void SetHealthbarObject(GameObject obj) {
        if (healthbar != null) {
            Debug.Log("Healthbar already set, aborting");
        }
        else {
            healthbar = obj.GetComponent<Healthbar>();
        }
    }
    public void ChangeCurrentHealth(int amount) {
        CURR_HEALTH += amount;
        if (CURR_HEALTH < 0) {
            CURR_HEALTH = 0;
        }
        else if (CURR_HEALTH > MAX_HEALTH) {
            CURR_HEALTH = MAX_HEALTH;
        }

        healthbar.UpdateImg(CURR_HEALTH);

        if (CURR_HEALTH == 0) {
            GameManager.instance.GameOver();
        }
    }

    public void ApplyForce(UnityEngine.Vector3 direction) {
        transform.GetComponent<Rigidbody>().AddForce(direction);
    }
}
