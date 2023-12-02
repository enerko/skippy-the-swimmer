using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMain : MonoBehaviour
{
    public static Transform s_CameraOverride;  // set this to force camera transform (in override mode iff this is non-null)
    public static bool s_OverrideTransitioning = false;  // if transitioning between free mode and override
    public const float OverrideTime = 0.75f;
    public static AudioSource s_SfxSource;

    public Transform goalTransform;
    public Vector3 offset = new Vector3(0, 2, -7);

    // important: pitch is always between 0-360 so this is kinda weird, but look at the calculations below
    private const int MaxPitch = 60;  // cap how much you can look downwards
    private const int MinPitch = -15;  // cap how much you can look upwards
    private const float AutoRotateIdleTime = 1.5f;  // how many seconds since last camera input to start auto rotating
    private Vector3 _focus;
    private Vector3 _velocity;
    private Vector2 _input;
    private float _cameraIdleTimer = AutoRotateIdleTime;  // keep track of how long the camera control has been idle for

    public GameObject player;
    public Rigidbody playerRB;
    public bool isPlayerCamera = true;  // is this the player camera or some random other camera? like in the menu for example
    public static bool s_CutScenePlayed;

    // hide objectives and checklist prompt
    // disbale input except escape
    public static bool s_CutSceneActive;

    // Start is called before the first frame update
    void Start()
    {
        s_SfxSource = GetComponent<AudioSource>();

        if (isPlayerCamera) {
            // Hide cursor
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _focus = player.transform.position;

            if (s_CutScenePlayed)
            {
                OnCutSceneEnd();
            }
        }
    }

    public void PerformCameraRotate(InputValue inputValue)
    {
        _input = inputValue.Get<Vector2>() * PlayerPrefs.GetFloat("Sensitivity", 5) * 0.5f;

        if (PlayerPrefs.GetFloat("Invert Camera", 0) == 1) {
            _input *= -1;
        }

        _cameraIdleTimer = 0;
    }

    // separate one for the right joystick
    public void PerformCameraRotateJoystick(InputValue inputValue)
    {
        _input = inputValue.Get<Vector2>() * PlayerPrefs.GetFloat("Sensitivity", 5) * 100 * Time.deltaTime;

        if (PlayerPrefs.GetFloat("Invert Camera", 0) == 1) {
            _input *= -1;
        }

        _cameraIdleTimer = 0;
    }

    // Given pitch angle between 0 and 360, adjust it to make it -180 to 180
    private float AdjustPitch(float pitch) {
        if (pitch <= 180) {
            return pitch;
        } else {
            return pitch - 360;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Globals.s_GameIsPaused || !isPlayerCamera) return;

        if (s_CutSceneActive) return;

        // If camera angle is forced...
        // if (s_CameraOverride) {
        //     goalTransform.position = s_CameraOverride.position;
        //     goalTransform.rotation = s_CameraOverride.rotation;

        //     transform.position = Vector3.SmoothDamp(transform.position, goalTransform.position, ref _velocity, OverrideTime);
        //     transform.rotation = Quaternion.Lerp(transform.rotation, goalTransform.rotation, 6f * Time.deltaTime);

        //     return;
        // }

        _focus = Vector3.SmoothDamp(_focus, player.transform.position, ref _velocity, 0.25f);
        goalTransform.position = _focus;

        // time how long camera input has been idle for
        if (_input == Vector2.zero) {
            _cameraIdleTimer += Time.deltaTime;
        }

        // rotate
        if ((_cameraIdleTimer >= AutoRotateIdleTime) && (PlayerPrefs.GetFloat("Autorotate", 1) == 1) &&
                (Player.s_HorizInput != Vector3.zero)) {  // auto-rotate the camera, but only if player is moving and not controlling camera
            Vector3 playerAngles = playerRB.gameObject.transform.rotation.eulerAngles;

            // behave differently if player is moving away from or towards camera
            bool towards = Player.s_HorizInput.z < 0;
            float yAngle = towards ? playerAngles.y + 180 : playerAngles.y;
            // float yAngle = towards ? goalTransform.rotation.eulerAngles.y : playerAngles.y;

            Vector3 newAngles = new Vector3(15 + playerAngles.x, yAngle, 0);
            Quaternion targetRotation = Quaternion.Euler(newAngles);
            goalTransform.rotation = Quaternion.Slerp(goalTransform.rotation, targetRotation, Time.deltaTime);
        } else {  // player control
            // _input is already adjusted for sensitivity
            goalTransform.Rotate(new Vector3(0, _input.x, 0), Space.World);
            goalTransform.Rotate(new Vector3(-_input.y, 0, 0));
        }

        // cap camera's pitch
        Vector3 currRotation = goalTransform.rotation.eulerAngles;

        // when reading pitch, it's 0 - 360
        float currPitch = AdjustPitch(currRotation.x);

        // You can set pitch to anything, it will autonormalize to 0 - 360 internally
        currPitch = Mathf.Clamp(currPitch, MinPitch, MaxPitch);
        goalTransform.rotation = Quaternion.Euler(currPitch, currRotation.y, 0);

        goalTransform.Translate(offset);

        // raycast from focus to the camera to see if anything is blocking it
        Vector3 direction = goalTransform.position - _focus;
        Ray ray = new Ray(_focus, direction.normalized);
        RaycastHit hit;
        LayerMask ignore = ~(LayerMask.GetMask("Player") | LayerMask.GetMask("Ignore Raycast") | LayerMask.GetMask("Interactable") |
            LayerMask.GetMask("Glass"));

        if (Physics.Raycast(ray, out hit, offset.magnitude, ignore, QueryTriggerInteraction.Ignore)) {
            goalTransform.position = hit.point + hit.normal * 0.1f;  // small buffer to prevent clipping thru nearby walls
        }

        // smoothly move camera to goal; this may look a bit weird if moving very fast with mouse, but pretend it's not an issue with a controller
        // mostly makes transitioning between cutting smooth
        transform.position = Vector3.Lerp(transform.position, goalTransform.position, 40f * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, goalTransform.rotation, 40f * Time.deltaTime);
    }
    
    public static Vector3 CustomWorldToScreenPoint(Vector3 position)
    {
        RectTransform gameUI = GameObject.Find("Game UI").GetComponent<RectTransform>();
        Vector3 point = Camera.main.WorldToScreenPoint(position);
        Vector3 customPoint = new Vector3(point.x / Camera.main.pixelWidth * gameUI.rect.width, point.y / Camera.main.pixelHeight * gameUI.rect.height,0);

        return customPoint;
    }

    public static void PlaySFX(AudioClip clip) {
        s_SfxSource.PlayOneShot(clip);
    }

    public void OnCutSceneStart()
    {
        s_CutSceneActive = true;
    }

    public void OnCutSceneEnd()
    {
        s_CutSceneActive = false;
        if (gameObject.GetComponent<Animator>())
        {
            gameObject.GetComponent<Animator>().enabled = false;
        }
        Globals.s_Restarted = true;

    }

}
