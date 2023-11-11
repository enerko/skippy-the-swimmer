using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Attach this to any audio source so it uses the volume established by user settings
public class DynamicVolumeChanger : MonoBehaviour
{
    public enum AudioType {
        MUSIC, SFX
    }

    public AudioType audioType;

    private AudioSource _source;

    void Start() {
        _source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        _source.volume = PlayerPrefs.GetFloat(audioType == AudioType.SFX ? "SFX Volume" : "Music Volume", 1);
    }
}
