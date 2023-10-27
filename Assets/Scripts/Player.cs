using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static bool s_Grounded = false;
    public static bool s_InWater = false;
    public static bool s_Invul = false;
    public static bool s_IsAttacking = false;
    public static bool s_CanMove = true;  // freeze movement when, for example, fading out at end of tutorial
    public static bool s_ConversationActive = false;  // if currently in a convo
    public static Conversation s_CurrentConversation;  // if CAN begin a convo
    public static bool s_AchievementsOpen = false;

    [SerializeField]
    private float _baseSpeed = 6;
    private const float Jump = 13f;  // initial velocity
    private bool hasUsedDoubleJump = false;
    private const float FallAdjustment = 3f;
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
    public AudioClip dryJump;
    public AudioClip wetJump;
    public AudioClip whooshJump;
    public AudioClip landing;
    public GameObject plrMesh;
    public Rig lookAtRig;
    public GameObject aim;

    private HealthBar _healthBar;
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _healthBar = FindObjectOfType<HealthBar>();
    }

    public void PerformJump()
    {
        if (s_ConversationActive || !s_CanMove) return;  // dont jump during a convo

        if (s_Grounded)
        {
            _rb.velocity = new Vector3(_rb.velocity.x, Jump, _rb.velocity.z);
            hasUsedDoubleJump = false;

            AudioClip jumpNoise = s_InWater ? wetJump : dryJump;
            AudioSource.PlayClipAtPoint(jumpNoise, transform.position);
        }
        else if (PlayerPowerup.s_DoubleJumpEnabled && !hasUsedDoubleJump)
        {
            _rb.velocity = new Vector3(_rb.velocity.x, Jump, _rb.velocity.z);
            hasUsedDoubleJump = true;
            AudioSource.PlayClipAtPoint(whooshJump, transform.position);
        }
    }

    public void PerformMove(InputValue inputValue)
    {
        if (s_ConversationActive || !s_CanMove) return;

        Vector3 inputVector3 = inputValue.Get<Vector3>();
        _horizInput = new Vector3(inputVector3.x, 0, inputVector3.z);
    }

    public void PerformAttack(InputValue inputValue)
    {
        if (s_ConversationActive || !s_CanMove) return; 

        if (PlayerHealth.s_Health <= 0) 
        {    
             StartCoroutine(_healthBar.Shake());
             return;
        }

        Vector2 attackValue = inputValue.Get<Vector2>();
        if (attackValue.y > 0 && !s_IsAttacking)
        {
            StartCoroutine(TailAttack());
        }
    }

    public void PerformTalk()
    {
        if (!s_CurrentConversation || !s_Grounded) return;  // no conversation to begin, or in air
        s_ConversationActive = s_CurrentConversation.index < s_CurrentConversation.dialogueChain.Length;
        s_CurrentConversation.Advance();
    }

    void Update() {
        // play footsteps
        if (s_Grounded && _timer - _stepAudioTime >= StepAudioDelay && _horizInput.magnitude > 0.01) {
            AudioClip[] clips = s_InWater ? wetSteps : drySteps;
            _stepAudioTime = _timer;
            AudioSource.PlayClipAtPoint(clips[UnityEngine.Random.Range(0, clips.Length)], transform.position, Settings.GetSourceVolume());
        }

        // move the aim object to move the neck, more noticeable when turning
        Vector3 aimGoal = new Vector3(transform.InverseTransformDirection(_rb.velocity).x * 3, 0.7f + _rb.velocity.y / 2, 4.15f);

        // if moving only horizontally, Skippy should still look forward
        if (Mathf.Abs(_horizInput.z) <= 0.1) {
            aimGoal.x = 0;
        }

        aim.transform.localPosition = Vector3.Lerp(aim.transform.localPosition, aimGoal, 10 * Time.deltaTime);

        _timer += Time.deltaTime;
    }

    // Physics stuff
    void FixedUpdate()
    {
        if (Globals.s_GameIsPaused) return;

        if (s_ConversationActive || !s_CanMove) {
            _horizInput = Vector3.zero;  // don't continue walking if you walk into npc and talk at the same time
        }
        
        Vector3 currVelo = _rb.velocity;

        // apply camera rotation
        float cameraRotation = Camera.main.transform.rotation.eulerAngles.y;
        Vector3 horizVelo = Quaternion.AngleAxis(cameraRotation, Vector3.up) * _horizInput;

        // raycast to find the normal
        RaycastHit hit;
        bool raycast = Physics.Raycast(transform.position, -Vector3.up, out hit, 1, ~LayerMask.GetMask("Player"), QueryTriggerInteraction.Ignore) ||
        Physics.Raycast(transform.position + transform.forward * 0.5f, -Vector3.up, out hit, 1, ~LayerMask.GetMask("Player"), QueryTriggerInteraction.Ignore) ||
        Physics.Raycast(transform.position - transform.forward * 0.5f, -Vector3.up, out hit, 1, ~LayerMask.GetMask("Player"), QueryTriggerInteraction.Ignore);

        float slopeAngle = s_Grounded ? Vector3.Angle(Vector3.up, hit.normal) : 0;

        Vector3 lookDirection;
        Vector3 goalUpDirection = slopeAngle >= 35 ? Vector3.up : hit.normal;  // can't climb steep things

        // Rotate in such a way that Skippy is aligned to the ground (do not rotate further when spinning)
        if (horizVelo.magnitude > 0.01f) {
            _rb.constraints = RigidbodyConstraints.FreezeRotation;

            Vector3 goalDirection = s_Grounded ? Vector3.ProjectOnPlane(horizVelo, hit.normal).normalized : horizVelo;
            lookDirection = Vector3.Slerp(transform.forward, goalDirection, 0.5f).normalized;
    
            transform.LookAt(transform.position + lookDirection, Vector3.Slerp(transform.up, goalUpDirection, 0.25f));
        } else {
            horizVelo = Vector3.zero;
            _rb.constraints |= RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
            transform.LookAt(transform.position + transform.forward, Vector3.Slerp(transform.up, goalUpDirection, 0.25f));
        }

        // fall down faster
        currVelo += Vector3.up * Physics.gravity.y * FallAdjustment * Time.deltaTime;

        // Update velocity
        float speed = PlayerPowerup.s_SpeedBoostEnabled ? _baseSpeed * 2 : (PlayerHealth.s_Health <= 0 ? _baseSpeed / 2 : _baseSpeed);
        _rb.velocity = horizVelo * speed + new Vector3(0, currVelo.y, 0);
    }

    // perform tail attack (and spin animation)
    private IEnumerator TailAttack() {
        s_IsAttacking = true;
        AudioSource.PlayClipAtPoint(tailSwipeSound, transform.position, Settings.GetSourceVolume());
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

    // Detect groundedness
    void OnTriggerEnter(Collider other) {
        if (other.isTrigger) return;  // other must not be another trigger, must be collideable
        s_Grounded = true;

        if (_rb.velocity.y < -0.1f)
            AudioSource.PlayClipAtPoint(landing, transform.position);
    }

    void OnTriggerStay(Collider other) {
        if (other.isTrigger) return;  // other must not be another trigger, must be collideable
        s_Grounded = true;
    }

    void OnTriggerExit(Collider other) {
        if (other.isTrigger) return;  // other must not be another trigger, must be collideable
        s_Grounded = false;
    }
}