using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public static float s_MaxHealth = 100;
    public static float s_Health = s_MaxHealth;
    public AudioClip waterSplash;
    public AudioClip healthChargeSound;

    private float DamagePerSecond = 5;
    
    private float _timer = 0;
    private const float InvulTime = 0.75f;
    
    private HealthBar _healthBar;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _healthBar = FindObjectOfType<HealthBar>();
        _audioSource = GetComponent<AudioSource>();
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
        else if (Player.s_ConversationActive) //|| Player.s_AchievementsOpen)
        {
            return;
        }
        else
        {
            if (Player.s_InWater) {
                // completely heal so player never runs out of health during power up
                s_Health = s_MaxHealth;
            } else {
                float healthChange = (-DamagePerSecond) * Time.deltaTime;
                s_Health = Math.Clamp(s_Health + healthChange, 0, s_MaxHealth);
            }
        }
    }

    // When Skippy enters a puddle of water
    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag != "Water") return;

        if (!Player.s_InWater) {
            Player.s_InWater = true;
            _healthBar.EnterWater();

            if (other.gameObject.name == "WaterTank" || other.gameObject.name == "Spawn") return;

            CameraMain.PlaySFX(waterSplash);
            CameraMain.PlaySFX(healthChargeSound);
        }
    }

    // When Skippy exits a puddle of water
    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag != "Water") return;
        Player.s_InWater = false;
        Player.s_Invul = true;
    }

}
