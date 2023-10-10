using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerup : MonoBehaviour
{
    private static AudioSource s_PowerupBaseSource;
    private static AudioSource s_DoubleJumpSource;
    private static AudioSource s_SpeedSource;
    private static AudioSource s_MainMusicSource;

    void Start() {
        // do it this way so we dont have to drag and drop all the time i guess
        s_MainMusicSource = GameObject.Find("/Main Music").GetComponent<AudioSource>();
        s_PowerupBaseSource = GameObject.Find("/Main Music/Powerup Base").GetComponent<AudioSource>();
        s_DoubleJumpSource = GameObject.Find("/Main Music/Double Jump").GetComponent<AudioSource>();
        s_SpeedSource = GameObject.Find("/Main Music/Speed").GetComponent<AudioSource>();
    }

    public static IEnumerator EnableDoubleJump()
    {
        s_PowerupBaseSource.volume = 1;
        s_DoubleJumpSource.volume = 1;
        s_MainMusicSource.volume = 0;
        Player.s_DoubleJumpEnabled = true;

        yield return new WaitForSeconds(10f);

        s_DoubleJumpSource.volume = 0;
        Player.s_DoubleJumpEnabled = false;

        // if speed is not active
        if (!Player.s_SpeedBoostEnabled) {
            s_PowerupBaseSource.volume = 0;
            s_MainMusicSource.volume = 1;
        }
    }

    public static IEnumerator EnableSpeedBoost()
    {
        s_PowerupBaseSource.volume = 1;
        s_SpeedSource.volume = 1;
        s_MainMusicSource.volume = 0;
        Player.s_SpeedBoostEnabled = true;

        yield return new WaitForSeconds(10f);

        s_SpeedSource.volume = 0;
        Player.s_SpeedBoostEnabled = false;

        // if double jump is not active
        if (!Player.s_DoubleJumpEnabled) {
            s_PowerupBaseSource.volume = 0;
            s_MainMusicSource.volume = 1;
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
