using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This script is for making sure only one instance of the game object is present.
// To be used with HUD items and game logic that persists between scenes.

public class HUD_and_Display_Singleton : MonoBehaviour
{
    public static HUD_and_Display_Singleton instance;

    void Awake() {
        if (instance != null) {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        GameObject.DontDestroyOnLoad(this.gameObject);
    }
}