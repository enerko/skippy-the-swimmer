using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPowerup : MonoBehaviour
{
    public static bool DoubleJumpEnabled { get; set; } = false;
    
    private static AudioSource _powerupBaseSource;
    private static AudioSource _doubleJumpSource;
    private static AudioSource _mainMusicSource;

    private static PowerUpUI _doubleJumpUI;

    public static float PowerUpDuration = 10f;
    private static float _doubleJumpTimeLeft = 0f;

    private void Start() {
        // Assign the audio sources
        _mainMusicSource = GameObject.Find("/Main Music").GetComponent<AudioSource>();
        _powerupBaseSource = GameObject.Find("/Main Music/Powerup Base").GetComponent<AudioSource>();
        _doubleJumpSource = GameObject.Find("/Main Music/Double Jump").GetComponent<AudioSource>();

        // Assign the UI
        _doubleJumpUI = GameObject.Find("DoubleJumpUI")?.GetComponent<PowerUpUI>();
    }
    
    private void Update()
    {
        if (DoubleJumpEnabled)
        {
            _doubleJumpTimeLeft -= Time.deltaTime;
            if (_doubleJumpTimeLeft <= 0)
            {
                DeactivateDoubleJump();
            }
        }
    }

    public static void EnableDoubleJump()
    {
        float sfxVolume = PlayerPrefs.GetFloat("SFX Volume", 1);

        _powerupBaseSource.volume = sfxVolume;
        _doubleJumpSource.volume = sfxVolume;
        _mainMusicSource.volume = 0;
        DoubleJumpEnabled = true;

        _doubleJumpUI?.Show();
        _doubleJumpTimeLeft = PowerUpDuration;
    }
    
    private static void DeactivateDoubleJump()
    {
        float musicVolume = PlayerPrefs.GetFloat("Music Volume", 1);

        _doubleJumpSource.volume = 0;
        DoubleJumpEnabled = false;
        _powerupBaseSource.volume = 0;
        _mainMusicSource.volume = musicVolume;
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
