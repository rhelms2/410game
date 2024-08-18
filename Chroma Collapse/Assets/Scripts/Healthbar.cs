using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour 
{
    [SerializeField]
    Image[] healthImg;
    int last_index;

    void Start() {
        Player_Singleton.player_instance.SetHealthbarObject(this.gameObject);
        foreach(Image i in healthImg) i.enabled = false;
        last_index = Player_Singleton.MAX_HEALTH;
        UpdateImg(last_index);
    }

    public void UpdateImg(int health) {
        healthImg[last_index].enabled = false;
        healthImg[health].enabled = true;
        last_index = health;
    }
}
