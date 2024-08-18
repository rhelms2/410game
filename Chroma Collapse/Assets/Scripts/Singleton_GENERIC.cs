using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This script is for making sure only one instance of the game object is present.
// To be used with HUD items and game logic that persists between scenes.

public class Singleton : MonoBehaviour
{
    public static GameObject instance = null;

    void Awake() {
        if (instance != null) {
            Destroy(this);
            return;
        }
        instance = this.gameObject;
    }
}