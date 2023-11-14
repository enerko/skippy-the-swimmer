using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Plays sound when you hit it
public class Smackable : Interactable
{
    [SerializeField] private AudioClip _activateSound;

    public override void Activate() {
        if (_activateSound) {
            CameraMain.PlaySFX(_activateSound);
        }
    }
}
