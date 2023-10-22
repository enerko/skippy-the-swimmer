using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerup : MonoBehaviour
{
    public static bool s_DoubleJumpEnabled = false;
    public static bool s_SpeedBoostEnabled = false;
    
    private static AudioSource s_PowerupBaseSource;
    private static AudioSource s_DoubleJumpSource;
    private static AudioSource s_SpeedSource;
    private static AudioSource s_MainMusicSource;

    private static PowerUpUI doubleJumpUI;
    private static PowerUpUI speedBoostUI;

    public static float PowerUpDuration = 10f;
    private static float doubleJumpTimeLeft = 0f;
    private static float speedBoostTimeLeft = 0f;

    void Start() {
        // do it this way so we dont have to drag and drop all the time i guess
        s_MainMusicSource = GameObject.Find("/Main Music").GetComponent<AudioSource>();
        s_PowerupBaseSource = GameObject.Find("/Main Music/Powerup Base").GetComponent<AudioSource>();
        s_DoubleJumpSource = GameObject.Find("/Main Music/Double Jump").GetComponent<AudioSource>();
        s_SpeedSource = GameObject.Find("/Main Music/Speed").GetComponent<AudioSource>();

        doubleJumpUI = GameObject.Find("DoubleJumpUI").GetComponent<PowerUpUI>();
        speedBoostUI = GameObject.Find("SpeedBoostUI").GetComponent<PowerUpUI>();
    }
    
    void Update()
    {
        if (s_DoubleJumpEnabled)
        {
            doubleJumpTimeLeft -= Time.deltaTime;
            if (doubleJumpTimeLeft <= 0)
            {
                DeactivateDoubleJump();
            }
        }

        if (s_SpeedBoostEnabled)
        {
            speedBoostTimeLeft -= Time.deltaTime;
            if (speedBoostTimeLeft <= 0)
            {
                DeactivateSpeedBoost();
            }
        }
    }

    public static void EnableDoubleJump()
    {
        s_PowerupBaseSource.volume = 1;
        s_DoubleJumpSource.volume = 1;
        s_MainMusicSource.volume = 0;
        s_DoubleJumpEnabled = true;

        doubleJumpUI.Show();

        doubleJumpTimeLeft = PowerUpDuration;
    }

    public static void EnableSpeedBoost()
    {
        s_PowerupBaseSource.volume = 1;
        s_SpeedSource.volume = 1;
        s_MainMusicSource.volume = 0;
        s_SpeedBoostEnabled = true;

        speedBoostUI.Show();

        speedBoostTimeLeft = PowerUpDuration;
    }
    
    private void DeactivateDoubleJump()
    {
        s_DoubleJumpSource.volume = 0;
        s_DoubleJumpEnabled = false;
        if (!s_SpeedBoostEnabled)
        {
            s_PowerupBaseSource.volume = 0;
            s_MainMusicSource.volume = 1;
        }
    }

    private void DeactivateSpeedBoost()
    {
        s_SpeedSource.volume = 0;
        s_SpeedBoostEnabled = false;
        if (!s_DoubleJumpEnabled)
        {
            s_PowerupBaseSource.volume = 0;
            s_MainMusicSource.volume = 1;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Powerup powerup = other.GetComponent<Powerup>();
        if (powerup != null)
        {
            powerup.Activate();
        }
    }
}
