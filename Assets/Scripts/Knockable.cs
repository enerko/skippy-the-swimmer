using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockable : MonoBehaviour, IInteractable
{
    [SerializeField] private Vector3 _force;
    [SerializeField] private AudioClip _activateSound;
    [SerializeField] private AudioClip _impactSound;

    private Rigidbody _rb;
    private AudioSource _audioSource;
    private bool _activated = false;
    private bool _finishedAction = false;

    private void Start() {
        _rb = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }

    public void Activate() {
        // play activated sound if there is one
        _activated = true;
        if (_activateSound) {
            _audioSource.clip = _activateSound;
            _audioSource.Play();
        }

        // knock the object down with the provided force
        _rb.isKinematic = false;
        _rb.AddRelativeForce(_force, ForceMode.Impulse);
    }

    public bool GetActivated() {
        return _activated;
    }

    public bool GetFinishedAction() {
        return _finishedAction;
    }
}
