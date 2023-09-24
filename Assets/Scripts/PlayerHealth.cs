using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public static int s_MaxHealth = 100;
    public static int s_Health = s_MaxHealth;

    private const float HealthUpdateRate = 0.1f;  // health updates every this many seconds
    private const int HealingPerUpdate = 50;
    private const int DamagePerUpdate = 5;
    private float _timer = 0;
    private float _lastHealthUpdate = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (_timer - _lastHealthUpdate >= HealthUpdateRate) {
            _lastHealthUpdate = _timer;

            s_Health = Math.Clamp(Player.s_InWater ? s_Health + HealingPerUpdate : s_Health - DamagePerUpdate, 0, s_MaxHealth);
        }

        // On death...
        if (s_Health <= 0) {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
        }

        _timer += Time.deltaTime;
    }

    // When Skippy enters a puddle of water
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag != "Puddle") return;
        Player.s_InWater = true;
    }

    // When Skippy exits a puddle of water
    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag != "Puddle") return;
        Player.s_InWater = false;
    }
}
