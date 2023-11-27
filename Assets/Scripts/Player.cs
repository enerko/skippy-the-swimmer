using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

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
    public static bool s_IsDashing;

    [SerializeField]
    private float _baseSpeed = 6;
    private const float Jump = 14f;  // initial velocity
    private bool hasUsedDoubleJump = false;
    private const float FallAdjustment = 3f;
    private const float StepAudioDelay = 0.3f;
    private const float TailAttackRadius = 1.5f;
    private const float TailAttackDelay = 0.3f;  // how long tail attack lasts for
    private float _timer = 0;
    private float _idleTimer = 0;
    private float _stepAudioTime = 0;
    private Rigidbody _rb;
    private Vector3 _horizInput;
    [SerializeField]
    private Gradient _healthGradient;

    public AudioClip[] drySteps;
    public AudioClip[] wetSteps;
    public AudioClip tailSwipeSound;
    public AudioClip dryJump;
    public AudioClip wetJump;
    public AudioClip whooshJump;
    public AudioClip landing;
    public AudioClip boing;
    public GameObject plrMesh;
    public Rig lookAtRig;
    public GameObject aim;
    public Animator animator;
    public Renderer plrRenderer;
    public ParticleSystem particles;

    private HealthBar _healthBar;
    private Quaternion _relativeRotation;
    private Color _originalColor;
    private bool _triggerDetectingGround = false;  // if the attached trigger is in the ground
    private float _dashTime = 0.5f;
    private float _dashSpeed = 15;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _healthBar = FindObjectOfType<HealthBar>();

        if (plrRenderer != null)
        {
            _originalColor = plrRenderer.material.color;
        }

        // the director only exists in the bedroom level, not the tutorial
        PlayableDirector director = gameObject.GetComponent<PlayableDirector>();
        if (CameraMain.s_CutScenePlayed && (director != null))
        {
            director.enabled = false;
        }
    }

    public void PerformJump()
    {
        if (s_ConversationActive || !s_CanMove || CameraMain.s_CutSceneActive) return;  // dont jump during a convo

        animator.SetBool("IsSleeping", false);
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Lay down 0") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("Wake up") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("Sleep")) return;

        if (s_Grounded)
        {
            _rb.velocity = new Vector3(_rb.velocity.x, Jump, _rb.velocity.z);
            hasUsedDoubleJump = false;

            AudioClip jumpNoise = s_InWater ? wetJump : dryJump;
            CameraMain.PlaySFX(jumpNoise);

        }
        else if (PlayerPowerup.DoubleJumpEnabled && !hasUsedDoubleJump)
        {
            _rb.velocity = new Vector3(_rb.velocity.x, Jump, _rb.velocity.z);
            hasUsedDoubleJump = true;
            CameraMain.PlaySFX(whooshJump);
        }
    }

    public void PerformMove(InputValue inputValue)
    {
        if (s_ConversationActive || !s_CanMove || CameraMain.s_CutSceneActive) return;
        animator.SetBool("IsSleeping", false);
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Lay down 0") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("Wake up") ||
            animator.GetCurrentAnimatorStateInfo(0).IsName("Sleep")) return;

        // Always change frame of reference when input is changed
        _relativeRotation = Camera.main.transform.rotation;
        Vector3 inputVector3 = inputValue.Get<Vector3>();
        _horizInput = new Vector3(inputVector3.x, 0, inputVector3.z);
    }

    public void PerformDash()
    {
        if (s_ConversationActive || !s_CanMove || CameraMain.s_CutSceneActive 
            || s_IsDashing || PlayerHealth.s_Health == 0) return;

        StartCoroutine(DashCoroutine());

    }
    private IEnumerator DashCoroutine()
    {
        float startTime = Time.time; 
        while (Time.time < startTime + _dashTime)
        {
            s_IsDashing = true;
            yield return null; 
        }
        s_IsDashing = false;
    }

    public void PerformAttack(InputValue inputValue)
    {
        if (s_ConversationActive || !s_CanMove || CameraMain.s_CutSceneActive) return; 

        if (PlayerHealth.s_Health <= 0) 
        {    
             StartCoroutine(_healthBar.TailWhipUnavailable());
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
        if (!s_CurrentConversation || !s_Grounded || CameraMain.s_CutSceneActive) return;  // no conversation to begin, or in air
        s_ConversationActive = s_CurrentConversation.index < s_CurrentConversation.dialogueChain.Length;
        s_CurrentConversation.Advance();
    }

    void Update() {
        // set animator to play walk
        float inputMagnitude = new Vector2(_horizInput.x, _horizInput.z).magnitude;
        animator.SetBool("IsWalking", inputMagnitude > 0.1f);
        animator.SetBool("IsGrounded", s_Grounded);
        animator.SetFloat("VeloY", _rb.velocity.y);

       if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle 0") && !CameraMain.s_CutSceneActive && s_InWater)
        {
            _idleTimer += Time.deltaTime;
            //Debug.Log(_idleTimer.ToString());
        }
        else
        {
            _idleTimer = 0;
        }

        if (_idleTimer > 5)
        {
            animator.SetBool("IsSleeping", true);
        }

        // play footsteps
        if (s_Grounded && _timer - _stepAudioTime >= StepAudioDelay && _horizInput.magnitude > 0.01) {
            AudioClip[] clips = s_InWater ? wetSteps : drySteps;
            _stepAudioTime = _timer;

            CameraMain.PlaySFX(clips[UnityEngine.Random.Range(0, clips.Length)]);
        }

        // move the aim object to move the neck, more noticeable when turning
        Vector3 aimGoal = new Vector3(transform.InverseTransformDirection(_rb.velocity).x * 3, 0.7f + _rb.velocity.y / 2, 4.15f);
        
        // update player colour based on hp
        UpdatePlayerColor();
        
        // if moving only horizontally, Skippy should still look forward
        if (Mathf.Abs(_horizInput.z) <= 0.1) {
            aimGoal.x = 0;
        }

        aim.transform.localPosition = Vector3.Lerp(aim.transform.localPosition, aimGoal, 10 * Time.deltaTime);

        if((PlayerPowerup.DoubleJumpEnabled && _horizInput.magnitude != 0) || (PlayerPowerup.DoubleJumpEnabled && !s_Grounded) )
        {
            if (!particles.isPlaying)
            {
                particles.Play();
            }
            
        }
        else
        {
            particles.Stop();
        }

        _timer += Time.deltaTime;
        
    }

    // Physics stuff
    void FixedUpdate()
    {
        if (Globals.s_GameIsPaused) return;

        animator.SetBool("IsGrounded", s_Grounded);
        animator.SetFloat("VeloY", _rb.velocity.y);

        if (s_ConversationActive || !s_CanMove || CameraMain.s_CutSceneActive) {
            _horizInput = Vector3.zero;  // don't continue walking if you walk into npc and talk at the same time
        }
        
        Vector3 currVelo = _rb.velocity;

        // Prevent controls from suddenly changing when camera is moved to forced angle
        // If in override mode, the frame of reference auto updates when input is changed
        if (!CameraMain.s_CameraOverride) {
            // it's okay if transitioning to freeform camera
            _relativeRotation = Camera.main.transform.rotation;
        }

        // apply camera rotation
        float cameraRotation = _relativeRotation.eulerAngles.y;
        Vector3 horizVelo = Quaternion.AngleAxis(cameraRotation, Vector3.up) * _horizInput;
        horizVelo = horizVelo.normalized;

        // raycast to find the normal
        RaycastHit hit;
        bool raycast = Physics.Raycast(transform.position, -Vector3.up, out hit, 1, ~LayerMask.GetMask("Player"), QueryTriggerInteraction.Ignore) ||
        Physics.Raycast(transform.position + transform.forward * 0.5f, -Vector3.up, out hit, 1, ~LayerMask.GetMask("Player"), QueryTriggerInteraction.Ignore) ||
        Physics.Raycast(transform.position - transform.forward * 0.5f, -Vector3.up, out hit, 1, ~LayerMask.GetMask("Player"), QueryTriggerInteraction.Ignore);

        float slopeAngle = 0;

        if (raycast) {
            s_Grounded = true;
            slopeAngle = Vector3.Angle(Vector3.up, hit.normal);
        } else {
            s_Grounded = _triggerDetectingGround;  // the trigger is a failsafe
        }

        //Vector3 lookDirection;
        Vector3 goalUpDirection = slopeAngle >= 35 ? Vector3.up : hit.normal;  // can't climb steep things

        // Rotate in such a way that Skippy is aligned to the ground (do not rotate further when spinning)
        if (horizVelo.magnitude > 0.01f || s_IsDashing) {
            _rb.constraints = RigidbodyConstraints.FreezeRotation;

            Vector3 goalDirection = s_Grounded ? Vector3.ProjectOnPlane(horizVelo, hit.normal).normalized : horizVelo;  // look towards
            Quaternion goalRotation = Quaternion.LookRotation(goalDirection, goalUpDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, goalRotation, 0.2f);

            // lookDirection = Vector3.Slerp(transform.forward, goalDirection, 0.5f).normalized;
    
            // transform.LookAt(transform.position + lookDirection, Vector3.Slerp(transform.up, goalUpDirection, 0.25f));
        } else {
            horizVelo = Vector3.zero;
            _rb.constraints |= RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
            transform.LookAt(transform.position + transform.forward, Vector3.Slerp(transform.up, goalUpDirection, 0.25f));
        }

        // fall down faster
        currVelo += Vector3.up * Physics.gravity.y * FallAdjustment * Time.deltaTime;

        // Update velocity
        float speed = s_IsDashing ? _dashSpeed: _baseSpeed;
        speed *= horizVelo.magnitude;
        Vector3 newDirection = (Vector3.Scale(transform.forward, new Vector3(1, 0, 1)) * 0.25f + horizVelo * 0.75f).normalized;

         _rb.velocity = newDirection * speed + new Vector3(0, currVelo.y, 0);

    }

    // perform tail attack (and spin animation)
    private IEnumerator TailAttack() {
        s_IsAttacking = true;

        CameraMain.PlaySFX(tailSwipeSound);

        Collider[] collided = Physics.OverlapSphere(transform.position, TailAttackRadius);

        // process each object
        foreach (Collider other in collided) {
            Interactable interact = other.GetComponent<Interactable>();

            if ((interact != null) && !interact.Activated) {
                interact.Activate();
                Debug.Log(other.gameObject);
            }
        }

        // do the tail attack animation
        lookAtRig.weight = 0;  // stop head from looking at the aim sphere

        float degreesPerSecond = 360 / TailAttackDelay;
        float degreesSoFar = 0;

        // spin the mesh
        while (degreesSoFar < 360) {
            float rotation = degreesPerSecond * Time.deltaTime;

            // in low frame rate, rotation can actually cause it to overshoot
            // so use math to figure out how much to actually rotate by to not overshoot 360 degrees
            float trueRotation = Mathf.Min(degreesSoFar + rotation, 360) - degreesSoFar;
            plrMesh.transform.Rotate(new Vector3(0, trueRotation, 0));

            degreesSoFar += trueRotation;
            yield return null;
        }

        lookAtRig.weight = 1;
        s_IsAttacking = false;
        yield return null;  // coroutine should stop here
    }

    // Detect pillow bounce
    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.name != "PillowCollider") {
            return;
        }

        CameraMain.PlaySFX(boing);

        // if double jump is on, you should be able to jump once after bouncing
        if (PlayerPowerup.DoubleJumpEnabled) {
            hasUsedDoubleJump = false;
        }
    }

    // Detect groundedness with a collider IN CASE RAYCASTING DOESN'T DETECT IT
    void OnTriggerEnter(Collider other) {
        if (other.isTrigger) return;  // other must not be another trigger, must be collideable
        _triggerDetectingGround = true;

        if (_rb.velocity.y < -0.1f) {
            CameraMain.PlaySFX(landing);
        }
    }

    void OnTriggerStay(Collider other) {
        if (other.isTrigger) return;  // other must not be another trigger, must be collideable
        _triggerDetectingGround = true;
    }

    void OnTriggerExit(Collider other) {
        if (other.isTrigger) return;  // other must not be another trigger, must be collideable
        _triggerDetectingGround = false;
    }

    private void UpdatePlayerColor()
    {
        if (plrRenderer != null && !PlayerPowerup.DoubleJumpEnabled)
        {
            float healthFraction = PlayerHealth.s_Health / PlayerHealth.s_MaxHealth;

            // Calculate the grayscale version of the original color
            float grayScale = (_originalColor.r + _originalColor.g + _originalColor.b) / 3;
            Color grayColor = new Color(grayScale, grayScale, grayScale);

            // Blend the original color with the grayscale color based on health
            Color currentColor = Color.Lerp(grayColor, _originalColor, healthFraction);

            plrRenderer.material.color = currentColor;
        }
    }
    }
