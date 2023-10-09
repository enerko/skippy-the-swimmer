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
   
    [SerializeField]
    private float Speed = 7;
    private const float Jump = 7;
    private bool doubleJump = false;
    private bool hasUsedDoubleJump = false;
    private const float FallAdjustment = 1.0f;
    private const float StepAudioDelay = 0.3f;
    private const float TailAttackRadius = 1.5f;
    private const float TailAttackDelay = 0.3f;
    private float _timer = 0;
    private float _stepAudioTime = 0;
    private Rigidbody _rb;
    private Vector3 _horizInput;

    public AudioClip[] drySteps;
    public AudioClip[] wetSteps;
    public AudioClip tailSwipeSound;
    public GameObject plrMesh;
    public Rig lookAtRig;
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Input stuff
    void Update() {
        _horizInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        

        // Handle jumping
        if (Input.GetButtonDown("Jump"))
        {
            if (s_Grounded)
            {
                _rb.velocity = new Vector3(_rb.velocity.x, Jump, _rb.velocity.z);
                hasUsedDoubleJump = false;
                
            }
            else if(doubleJump && !hasUsedDoubleJump)
            {
                _rb.velocity = new Vector3(_rb.velocity.x, Jump * 1.2f, _rb.velocity.z);
                hasUsedDoubleJump = true;
            }
        }

        // Tail attack
        if (Input.GetAxis("Fire1") > 0 && !s_IsAttacking) {
            StartCoroutine(TailAttack());
        }

        // play footsteps
        if (s_Grounded && _timer - _stepAudioTime >= StepAudioDelay && _horizInput.magnitude > 0.01) {
            AudioClip[] clips = s_InWater ? wetSteps : drySteps;
            _stepAudioTime = _timer;
            AudioSource.PlayClipAtPoint(clips[UnityEngine.Random.Range(0, clips.Length)], transform.position, 0.8f);
        }

        _timer += Time.deltaTime;
    }

    // Physics stuff
    void FixedUpdate()
    {
        Vector3 currVelo = _rb.velocity;

        // apply camera rotation
        float cameraRotation = Camera.main.transform.rotation.eulerAngles.y;
        Vector3 horizVelo = Quaternion.AngleAxis(cameraRotation, Vector3.up) * _horizInput;

        // raycast to see groundedness
        RaycastHit hit;
        s_Grounded = Physics.Raycast(transform.position, -Vector3.up, out hit, 1, ~LayerMask.GetMask("Player"), QueryTriggerInteraction.Ignore) ||
        Physics.Raycast(transform.position + transform.forward * 0.5f, -Vector3.up, out hit, 1, ~LayerMask.GetMask("Player"), QueryTriggerInteraction.Ignore) ||
        Physics.Raycast(transform.position - transform.forward * 0.5f, -Vector3.up, out hit, 1, ~LayerMask.GetMask("Player"), QueryTriggerInteraction.Ignore);

        
        float slopeAngle = s_Grounded ? Vector3.Angle(Vector3.up, hit.normal) : 0;

        Vector3 lookDirection;
        Vector3 goalUpDirection = slopeAngle >= 35 ? Vector3.up : hit.normal;  // can't climb steep things

        // Rotate in such a way that Skippy is aligned to the ground (do not rotate further when spinning)
        if (horizVelo.magnitude > 0.01f) {
            Vector3 goalDirection = s_Grounded ? Vector3.ProjectOnPlane(horizVelo, hit.normal).normalized : horizVelo;
            lookDirection = Vector3.Slerp(transform.forward, goalDirection, 0.1f).normalized;
    
            transform.LookAt(transform.position + lookDirection, Vector3.Slerp(transform.up, goalUpDirection, 0.1f));
        } else {
            horizVelo = Vector3.zero;
            transform.LookAt(transform.position + transform.forward, Vector3.Slerp(transform.up, goalUpDirection, 0.25f));
        }

        // fall down faster
        currVelo += Vector3.up * Physics.gravity.y * FallAdjustment * Time.deltaTime;

        // Update velocity
        _rb.velocity = horizVelo * Speed + new Vector3(0, currVelo.y, 0);
    }

    // perform tail attack (and spin animation)
    private IEnumerator TailAttack() {
        s_IsAttacking = true;
        AudioSource.PlayClipAtPoint(tailSwipeSound, transform.position, 0.8f);
        Collider[] collided = Physics.OverlapSphere(transform.position, TailAttackRadius, LayerMask.GetMask("Interactable"));

        // process each object
        foreach (Collider other in collided) {
            Interactable interact = other.GetComponent<Interactable>();

            if ((bool)!interact?.Activated)
                interact.Activate();

            Debug.Log(other.gameObject);
        }

        // do the tail attack animation
        lookAtRig.weight = 0;  // stop head from looking at the aim sphere

        float degreesPerSecond = 360 / TailAttackDelay;
        float degreesSoFar = 0;

        // spin the mesh
        while (degreesSoFar < 360) {
            float rotation = degreesPerSecond * Time.deltaTime;
            plrMesh.transform.Rotate(new Vector3(0, rotation, 0));
            degreesSoFar += rotation;
            yield return null;
        }

        lookAtRig.weight = 1;
        s_IsAttacking = false;
        yield return null;  // coroutine should stop here
    }

    public void EnableDoubleJump()
    {
        doubleJump = true;
        Invoke("DisableDoubleJump", 10.0f);
    }

    public void DisableDoubleJump()
    {
        doubleJump = false;
    }

    public void EnableSpeedBoost()
    {
        Speed = 14;
        Invoke("DisableSpeedBoost", 10.0f);
    }

    public void DisableSpeedBoost()
    {
        Speed = 7;
    }
}