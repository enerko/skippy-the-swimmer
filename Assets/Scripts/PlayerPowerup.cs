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

    void Start() {
        // do it this way so we dont have to drag and drop all the time i guess
        s_MainMusicSource = GameObject.Find("/Main Music").GetComponent<AudioSource>();
        s_PowerupBaseSource = GameObject.Find("/Main Music/Powerup Base").GetComponent<AudioSource>();
        s_DoubleJumpSource = GameObject.Find("/Main Music/Double Jump").GetComponent<AudioSource>();
        s_SpeedSource = GameObject.Find("/Main Music/Speed").GetComponent<AudioSource>();

        doubleJumpUI = GameObject.Find("DoubleJumpUI").GetComponent<PowerUpUI>();
        speedBoostUI = GameObject.Find("SpeedBoostUI").GetComponent<PowerUpUI>();
    }

    public static IEnumerator EnableDoubleJump()
    {
        if (!s_DoubleJumpEnabled) {  // temporarily disable replenishing
            s_PowerupBaseSource.volume = 1;
            s_DoubleJumpSource.volume = 1;
            s_MainMusicSource.volume = 0;
            s_DoubleJumpEnabled = true;

            doubleJumpUI.Show();

            yield return new WaitForSeconds(PowerUpDuration);
            // TODO: Because you can replenish this when it's still active, can't use a coroutine
            // must use an actual timer and keep track of time left

            s_DoubleJumpSource.volume = 0;
            s_DoubleJumpEnabled = false;

            // if speed is not active
            if (!s_SpeedBoostEnabled) {
                s_PowerupBaseSource.volume = 0;
                s_MainMusicSource.volume = 1;
            }
        }
    }

    public static IEnumerator EnableSpeedBoost()
    {
        if (!s_SpeedBoostEnabled) {
            s_PowerupBaseSource.volume = 1;
            s_SpeedSource.volume = 1;
            s_MainMusicSource.volume = 0;
            s_SpeedBoostEnabled = true;

            speedBoostUI.Show();

            yield return new WaitForSeconds(PowerUpDuration);

            s_SpeedSource.volume = 0;
            s_SpeedBoostEnabled = false;

            // if double jump is not active
            if (!s_DoubleJumpEnabled) {
                s_PowerupBaseSource.volume = 0;
                s_MainMusicSource.volume = 1;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Powerup powerup = other.GetComponent<Powerup>();
        if (powerup != null)
        {
            powerup.Activate();
        }
    }
}
