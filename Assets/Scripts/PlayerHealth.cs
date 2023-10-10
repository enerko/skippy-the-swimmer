using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public static float s_MaxHealth = 100;
    public static float s_Health = s_MaxHealth;

    private const float HealingPerSecond = 100;
    private const float DamagePerSecond = 10;
    
    private float _timer = 0;
    private const float InvulTime = 0.75f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        // delay healthChange on state change (in/out of water)
        if (Player.s_Invul) {
            _timer += Time.deltaTime;
            if (_timer >= InvulTime) {
                Player.s_Invul = false;
                _timer = 0;
            }
        }
        else
        {
            float healthChange = (Player.s_InWater ? HealingPerSecond : -DamagePerSecond) * Time.deltaTime;
            s_Health = Math.Clamp(s_Health + healthChange, 0, s_MaxHealth);
        }
        

        // On death...
        if (s_Health <= 0) {
            s_Health = s_MaxHealth;
            CheckpointManager.Instance.RespawnAtLastCheckpoint();
            Time.timeScale = 1;
        }
    }

    // When Skippy enters a puddle of water
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag != "Water") return;
        Player.s_InWater = true;
        
    }

    // When Skippy exits a puddle of water
    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag != "Water") return;
        Player.s_InWater = false;
        Player.s_Invul = true;
    }
    
    // When Skippy just leaves water
}
