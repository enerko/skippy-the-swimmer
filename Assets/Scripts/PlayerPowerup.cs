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
    public AudioClip powerupEndingSound;
    public static AudioClip powerupReceivedSound;
    [SerializeField] private AudioClip _powerupReceivedSoundInstance;
    private static AudioSource _audioSource;
    
    private bool _endingSoundPlayed = false;

    private static PowerUpUI _doubleJumpUI;

    public static float PowerUpDuration = 10f;
    private static float _doubleJumpTimeLeft = 0;

    private static SkinnedMeshRenderer _skippyBody;
    private static Material _normalMaterial;
    private static Material _powerupMaterial;
    
    void Awake()
    {
        PlayerPowerup.powerupReceivedSound = _powerupReceivedSoundInstance;
    }
    
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

        _powerupBaseSource.enabled = false;
        _doubleJumpSource.enabled = false;
        _audioSource = GetComponent<AudioSource>();
    }
    
    private void Update()
    {
        if (_doubleJumpTimeLeft > 0)
        {
            _doubleJumpTimeLeft -= Time.deltaTime;

            // Check if the powerup time is about to end and play the sound once
            if (_doubleJumpTimeLeft <= 5f && !_endingSoundPlayed)
            {
                CameraMain.PlaySFX(powerupEndingSound);
                _endingSoundPlayed = true; // Set the flag to true to prevent replaying the sound
            }
        }
        else if (DoubleJumpEnabled && _doubleJumpTimeLeft <= 0)
        {
            DeactivateDoubleJump();
            _endingSoundPlayed = false; // Reset the flag for next time the powerup is activated
        }
    }

    public static void EnableDoubleJump()
    {
        _mainMusicSource.enabled = false;
        _powerupBaseSource.enabled = true;
        _doubleJumpSource.enabled = true;

        if (!_powerupBaseSource.isPlaying) {
            CameraMain.PlaySFX(powerupReceivedSound);
            _powerupBaseSource.Play();
        }
        if (!_doubleJumpSource.isPlaying)
            _doubleJumpSource.Play();
            
        DoubleJumpEnabled = true;

        _doubleJumpUI?.Show();
        _doubleJumpTimeLeft = PowerUpDuration;

        _skippyBody.material = _powerupMaterial;
    }
    
    private static void DeactivateDoubleJump()
    {
        DoubleJumpEnabled = false;

        _doubleJumpSource.enabled = false;
        _powerupBaseSource.enabled = false;
        _powerupBaseSource.Stop();
        _doubleJumpSource.Stop();

        _mainMusicSource.enabled = true;

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
