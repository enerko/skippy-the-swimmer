using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockable : Interactable
{
    [SerializeField] private Vector3 _force;
    [SerializeField] private AudioClip _activateSound;
    [SerializeField] private AudioClip _impactSound;
    [SerializeField] private GameObject _impactTarget;

    private Rigidbody _rb;
    private AudioSource _audioSource;

    private void Start() {
        _rb = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }

    public override void Activate() {
        // play activated sound if there is one
        Activated = true;
        if (_activateSound) {
            _audioSource.clip = _activateSound;
            _audioSource.Play();
        }

        // knock the object down with the provided force
        _rb.isKinematic = false;
        _rb.AddRelativeForce(_force, ForceMode.Impulse);
    }

    // Handle impact noise
    void OnTriggerEnter(Collider other)
    {
        if (FinishedAction) return;  // do nothing if already finished
        if (other.gameObject != _impactTarget) return;
        FinishedAction = true;

        if (_impactSound) {
            _audioSource.clip = _impactSound;
            _audioSource.Play();
        }
    }
}
