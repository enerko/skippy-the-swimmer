using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicVolume : MonoBehaviour
{
    private AudioSource _musicSource;  // for main music

    private void Start() {
        _musicSource = GetComponent<AudioSource>();

        // set music volume at the start
        _musicSource.volume = PlayerPrefs.GetFloat("Music Volume", 1);
    }

    // for the music slider to change volume at runtime
    public void UpdateMusicVolume() {
        _musicSource.volume = PlayerPrefs.GetFloat("Music Volume", 1);
    }
}
