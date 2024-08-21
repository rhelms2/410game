using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

public class HealthModifier : MonoBehaviour
{
    Player_Singleton player;
    [SerializeField] int modify_amt;

    void Awake() {
        player = Player_Singleton.player_instance;
    }

    void ApplyHealthModifier() {
        player.ChangeCurrentHealth(modify_amt);
    }
}