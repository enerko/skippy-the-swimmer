using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Player : MonoBehaviour
{
    public static bool s_Grounded = false;
    public static bool s_InWater = false;
    public static bool s_Invul = false;
    public static bool s_IsAttacking = false;
   
    private const float Speed = 8;
    private const float Jump = 6;
    private const float Fall = 2.0f;
    private const float StepAudioDelay = 0.3f;
    private Rigidbody _rb;
    private AudioSource _audioSource;
    private float _timer = 0;
    private float _stepAudioTime = 0;
    private const float TailAttackRadius = 1.5f;
    private const float TailAttackDelay = 0.3f;
    private float _tailAttackTime = 0;

    public AudioClip[] drySteps;
    public AudioClip[] wetSteps;
    public GameObject plrMesh;
    public Rig lookAtRig;
    
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
        s_Grounded = Physics.Raycast(transform.position, -Vector3.up, out hit, 1, ~LayerMask.GetMask("Player")) ||
        Physics.Raycast(transform.position + transform.forward * 0.5f, -Vector3.up, out hit, 1, ~LayerMask.GetMask("Player")) ||
        Physics.Raycast(transform.position - transform.forward * 0.5f, -Vector3.up, out hit, 1, ~LayerMask.GetMask("Player"));

        float slopeAngle = s_Grounded ? Vector3.Angle(Vector3.up, hit.normal) : 0;

        Vector3 lookDirection;
        Vector3 goalUpDirection = slopeAngle >= 45 ? Vector3.up : hit.normal;  // can't climb steep things

        // Rotate in such a way that Skippy is aligned to the ground (do not rotate further when spinning)
        if (horizVelo.magnitude > 0.01f) {
            Vector3 goalDirection = s_Grounded ? Vector3.ProjectOnPlane(horizVelo, hit.normal).normalized : horizVelo;
            lookDirection = Vector3.Slerp(transform.forward, goalDirection, 0.1f).normalized;

            transform.LookAt(transform.position + lookDirection, Vector3.Slerp(transform.up, goalUpDirection, 0.1f));
        } else {
            horizVelo = Vector3.zero;
            transform.LookAt(transform.position + transform.forward, Vector3.Slerp(transform.up, goalUpDirection, 0.25f));
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

        // fall down faster
        if (currVelo.y < 0) {
            currVelo += Vector3.up * Physics.gravity.y * (Fall - 1) * Time.deltaTime;
        }

        // Tail attack
        if (Input.GetKeyDown("q") && _timer - _tailAttackTime >= TailAttackDelay && !s_IsAttacking) {
            TailAttack();
        }

        // Rotate the mesh if in tail attack
        if (s_IsAttacking) {
            lookAtRig.weight = 0;  // stop head from looking at the aim sphere
            float timeSince = _timer - _tailAttackTime;
            float rotation = 360 * (Time.deltaTime / TailAttackDelay);
            plrMesh.transform.Rotate(new Vector3(0, rotation, 0));

            // If the timer runs out, set flag back to false
            if (timeSince >= TailAttackDelay) {
                lookAtRig.weight = 1;
                s_IsAttacking = false;
            }
        }

        // Update velocity
        _rb.velocity = horizVelo * Speed + new Vector3(0, currVelo.y, 0);

        // Update timer
        _timer += Time.deltaTime;
    }

    // perform tail attack
    private void TailAttack() {
        _tailAttackTime = _timer;
        s_IsAttacking = true;
        Collider[] collided = Physics.OverlapSphere(transform.position, TailAttackRadius, LayerMask.GetMask("Breakable"));

        foreach (Collider other in collided) {
            Debug.Log(other.gameObject);

            if(other.GetComponent<KnockObject>() != null)
            {
                KnockObject ko = other.GetComponent<KnockObject>();
                StartCoroutine(ko.Knock());
            }
            else
            {
                Destroy(other.gameObject);

                IHasLiquid var = other.gameObject.GetComponent<IHasLiquid>();
                if (var != null)
                {
                    var.SpawnLiquid();
                }
            
            }

        }
    }
}