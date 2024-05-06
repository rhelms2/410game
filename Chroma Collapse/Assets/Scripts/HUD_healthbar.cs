using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_healthbar : GLOBAL_playerhealth
{
    public Image[] healthImg; // 3 - full, 0 - empty
    int cur_health;
    // Start is called before the first frame update
    void Start()
    {
        cur_health = current_health;
        foreach(Image i in healthImg) i.enabled = false;
        healthImg[cur_health].enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (cur_health != current_health){
            cur_health = current_health;
            foreach(Image i in healthImg) i.enabled = false;
            healthImg[cur_health].enabled = true;
        }
    }
}
