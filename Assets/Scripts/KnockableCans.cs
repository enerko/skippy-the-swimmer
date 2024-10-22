using System;
using UnityEngine;

public class KnockableCans : Interactable
{
    private Transform playerTransform;
    [SerializeField] private AudioClip _knockOverSound;
    [SerializeField] private bool _spawnsLiquid;
    [SerializeField] private GameObject _energySpawn;
    [SerializeField] private Vector3 _knockOverForce = new Vector3(0, 0, -1); // Adjust as needed

    private Rigidbody _rigidbody;
    public Prompt interactPrompt;

    private bool _isKnockedOver = false;

    void Start() {
        _rigidbody = GetComponent<Rigidbody>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void Activate() {
        if (!Activated && !_isKnockedOver) {
            Activated = true;
            
            Vector3 directionToPlayer = (transform.position - playerTransform.position).normalized;
            
            // Fisable kinematic to apply physics force
            _rigidbody.isKinematic = false;
            _rigidbody.AddForce(directionToPlayer * _knockOverForce.magnitude, ForceMode.Impulse);

            // Optionally, play knock over sound
            if (_knockOverSound) {
                CameraMain.PlaySFX(_knockOverSound);
            }

            // Handle the spawning of liquid after a delay
            if (_spawnsLiquid) {
                Invoke("SpawnLiquid", 1f); // Adjust delay as needed
            }

            interactPrompt?.Disable();
            _isKnockedOver = true;
        }
    }

    // private void MakeRigidbodyKinematicAgain() {
    //     if (_rigidbody != null) {
    //         _rigidbody.isKinematic = true;
    //     }
    // }

    private void SpawnLiquid() {
        if (_energySpawn != null) {
            // Determine a point in front of the can
            Vector3 forwardPosition = transform.position + transform.right * 1.0f + Vector3.up * 0.5f; // 1.0f can be adjusted based on how far in front you want
            
            int layerMask = ~LayerMask.GetMask("Interactable") & ~LayerMask.GetMask("Player");
            // Raycast downwards from this forward position to find the ground
            RaycastHit hit;
            if (Physics.Raycast(forwardPosition, Vector3.down, out hit, 5, layerMask, QueryTriggerInteraction.Ignore)) {
                Instantiate(_energySpawn, hit.point, Quaternion.identity);
            }
        } else {
            Debug.LogError("EnergySpawn is not set or found in KnockableCans");
        }
    }
}
