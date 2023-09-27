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
    private const float DamagePerSecond = 50;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        float healthChange = (Player.s_InWater ? HealingPerSecond : -DamagePerSecond) * Time.deltaTime;
        s_Health = Math.Clamp(s_Health + healthChange, 0, s_MaxHealth);

        // On death...
        if (s_Health <= 0) {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
        }
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
