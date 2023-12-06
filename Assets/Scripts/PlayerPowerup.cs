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
    
    private static bool _endingSoundPlayed = false;

    private static PowerUpUI _doubleJumpUI;

    public static float PowerUpDuration = 10f;
    private static float _doubleJumpTimeLeft = 0;

    private static SkinnedMeshRenderer _skippyBody;
    private static Material _normalMaterial;
    private static Material _powerupMaterial;
    private AudioSource _powerupEndingSource;
    
    void Awake()
    {
        powerupReceivedSound = _powerupReceivedSoundInstance;
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
        _audioSource = GetComponents<AudioSource>()[0];
        _powerupEndingSource = GetComponents<AudioSource>()[1]; 
        Debug.Log("AudioSource for Powerup Ending Sound Assigned: " + (_powerupEndingSource != null));
    }
    
    private void Update()
    {
        if (Player.s_ConversationActive)
        {
            if (_endingSoundPlayed && _powerupEndingSource.isPlaying)
            {
                _powerupEndingSource.Pause(); // Pause the powerup ending sound
            }
            return;
        }
        else
        {
            if (_endingSoundPlayed && !_powerupEndingSource.isPlaying && _doubleJumpTimeLeft <= 5f)
            {
                _powerupEndingSource.UnPause(); // Resume the powerup ending sound
            }
        }

        if (_doubleJumpTimeLeft > 0)
        {
            _doubleJumpTimeLeft -= Time.deltaTime;

            if (_doubleJumpTimeLeft <= 5f && !_endingSoundPlayed)
            {
                // Play the powerup ending sound directly through the AudioSource
                _powerupEndingSource.clip = powerupEndingSound;
                _powerupEndingSource.Play();
                _endingSoundPlayed = true;
            }
        }
        else if (DoubleJumpEnabled && _doubleJumpTimeLeft <= 0)
        {
            DeactivateDoubleJump();
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

        _endingSoundPlayed = false; // Reset the flag whenever the powerup is enabled or refreshed
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
