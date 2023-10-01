using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider slider;

    private void Awake()
    {
        float volume= PlayerPrefs.GetFloat("volume", 0f);
        slider.value = volume;
        audioMixer.SetFloat("Volume", volume);
    }

    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("Volume", volume);
        PlayerPrefs.SetFloat("volume", volume);
    }
}
