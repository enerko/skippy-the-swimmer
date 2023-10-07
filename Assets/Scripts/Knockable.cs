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

    private void Start() {
        _rb = GetComponent<Rigidbody>();
    }

    public override void Activate() {
        Activated = true;

        // play activated sound if there is one
        if (_activateSound) {
            AudioSource.PlayClipAtPoint(_activateSound, transform.position);
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
            AudioSource.PlayClipAtPoint(_impactSound, transform.position);
        }
    }
}
