using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static bool s_Grounded = false;
    public static bool s_InWater = false;
   
    private const float Speed = 8;
    private const float Jump = 7;
    private const float StepAudioDelay = 0.3f;
    private Rigidbody _rb;
    private AudioSource _audioSource;
    private float _timer = 0;
    private float _stepAudioTime = 0;

    public AudioClip[] drySteps;
    public AudioClip[] wetSteps;
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currVelo = _rb.velocity;
        Vector3 horizVelo = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        float cameraRotation = Camera.main.transform.rotation.eulerAngles.y;
        horizVelo = Quaternion.AngleAxis(cameraRotation, Vector3.up) * horizVelo;

        // raycast to see groundedness
        RaycastHit hit;
        s_Grounded = Physics.Raycast(transform.position, Vector3.down, out hit, 1, ~LayerMask.GetMask("Ignore Raycast"));

        // Rotate in such a way that Skippy is aligned to the ground
        if (horizVelo != Vector3.zero) {
            Vector3 lookDirection = Vector3.Slerp(transform.up, horizVelo, 0.05f).normalized;
            transform.LookAt(hit.normal == Vector3.zero ? transform.position - Vector3.up : transform.position - hit.normal, lookDirection);
        } else {
            transform.LookAt(transform.position - hit.normal, transform.up);
        }

        if (s_Grounded) {
            // Play random step sound (currently only dry)
            if (_timer - _stepAudioTime >= StepAudioDelay && horizVelo.magnitude > 0.01) {
                AudioClip[] clips = s_InWater ? wetSteps : drySteps;
                _stepAudioTime = _timer;
                _audioSource.clip = clips[UnityEngine.Random.Range(0, clips.Length)];
                _audioSource.Play();
            }
        }

        // Handle jumping
        if (Input.GetKeyDown("space") && s_Grounded) {
            currVelo.y = Jump;
        }

        // Update velocity
        _rb.velocity = horizVelo * Speed + new Vector3(0, currVelo.y, 0);

        // Update timer
        _timer += Time.deltaTime;
    }
}
