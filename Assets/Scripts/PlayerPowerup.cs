using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class PlayerPowerup : MonoBehaviour
{
    public static bool DoubleJumpEnabled { get; set; } = false;
    
    private static AudioSource _powerupBaseSource;
    private static AudioSource _doubleJumpSource;
    private static AudioSource _mainMusicSource;

    private static PowerUpUI _doubleJumpUI;

    public static float PowerUpDuration = 10f;
    private static float _doubleJumpTimeLeft = 0;

    private static SkinnedMeshRenderer _skippyBody;
    private static Material _normalMaterial;
    private static Material _powerupMaterial;

    private void Start() {
        // Assign the audio sources
        _mainMusicSource = GameObject.Find("/Main Music").GetComponent<AudioSource>();
        _powerupBaseSource = GameObject.Find("/Main Music/Powerup Base").GetComponent<AudioSource>();
        _doubleJumpSource = GameObject.Find("/Main Music/Double Jump").GetComponent<AudioSource>();

        // Assign the UI
        _doubleJumpUI = GameObject.Find("DoubleJumpUI")?.GetComponent<PowerUpUI>();

        // Assign renderer
        _skippyBody = GameObject.Find("/SkippyController/Skippy/Skippy_grp_LP/Body_grp/Body_geo").GetComponent<SkinnedMeshRenderer>();
        _normalMaterial = Resources.Load<Material>("Skippy");
        _powerupMaterial = Resources.Load<Material>("Glow");

        _skippyBody.material = _normalMaterial;

    }
    
    private void Update()
    {
        if (_doubleJumpTimeLeft > 0)
        {
            _doubleJumpTimeLeft -= Time.deltaTime;
        }
        else if (DoubleJumpEnabled && _doubleJumpTimeLeft <= 0)
        {
            DeactivateDoubleJump();
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

        _skippyBody.material = _powerupMaterial;
    }
    
    private static void DeactivateDoubleJump()
    {
        float musicVolume = PlayerPrefs.GetFloat("Music Volume", 1);

        _doubleJumpSource.volume = 0;
        DoubleJumpEnabled = false;
        _powerupBaseSource.volume = 0;
        _mainMusicSource.volume = musicVolume;

        _skippyBody.material = _normalMaterial;
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
