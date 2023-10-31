using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Plays sound when you hit it
public class Smackable : Interactable
{
    [SerializeField] private AudioClip _activateSound;

    public override void Activate() {
        if (_activateSound) {
            float volume = PlayerPrefs.GetFloat("SFX Volume", 1);
            AudioSource.PlayClipAtPoint(_activateSound, transform.position, volume);
        }
    }
}
