using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider slider;
    private static float _volume;

    private void Awake()
    {
        _volume = PlayerPrefs.GetFloat("volume", 0f);
        
        slider.value = _volume;
        audioMixer.SetFloat("Volume", _volume == -20 ? -80 : _volume);
    }

    public void SetVolume (float val)
    {
        _volume = val;
        audioMixer.SetFloat("Volume", _volume == -20 ? -80 : _volume);
        PlayerPrefs.SetFloat("volume", _volume);
    }

    public static float GetSourceVolume() {
        return (_volume + 20)/20;
    }
}
