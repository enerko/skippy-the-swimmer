using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smackable : Interactable
{
    [SerializeField] private AudioClip _activateSound;

    public override void Activate() {
        if (_activateSound) {
            AudioSource.PlayClipAtPoint(_activateSound, transform.position, Settings.GetSourceVolume());
        }
    }
}
