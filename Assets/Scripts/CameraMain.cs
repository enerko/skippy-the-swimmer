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

    public Transform goalTransform;
    public Vector3 offset = new Vector3(0, 2, -7);

    private const int MaxPitch = 60;  // max vertical rotation bounds
    private Vector3 _focus;
    private Vector3 _velocity;
    private Vector2 _input;

    public GameObject player;
    private bool cameraAnimated = true;

    // Start is called before the first frame update
    void Start()
    {
        // Hide cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _focus = player.transform.position;
    }

    public void PerformCameraRotate(InputValue inputValue)
    {
        _input = inputValue.Get<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Globals.s_GameIsPaused) return;

        if (cameraAnimated) return;

        // If camera angle is forced...
        if (s_CameraOverride) {
            goalTransform.position = s_CameraOverride.position;
            goalTransform.rotation = s_CameraOverride.rotation;

            transform.position = Vector3.SmoothDamp(transform.position, goalTransform.position, ref _velocity, OverrideTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, goalTransform.rotation, 6f * Time.deltaTime);

            return;
        }

        _focus = Vector3.SmoothDamp(_focus, player.transform.position, ref _velocity, 0.25f);
        goalTransform.position = _focus;

        // rotate
        float sensitivity = PlayerPrefs.GetFloat("Sensitivity", 4);
        goalTransform.Rotate(new Vector3(0, _input.x * sensitivity, 0), Space.World);
        goalTransform.Rotate(new Vector3(-_input.y * sensitivity, 0, 0));

        // cap camera's pitch
        Vector3 currRotation = goalTransform.rotation.eulerAngles;

        // pitch ranges from 0 - 360 even though in the editor it goes into negatives, so can't use Mathf.Clamp here
        float currPitch = currRotation.x;
       
        if (MaxPitch < currPitch && currPitch <= 90) {
            currPitch = MaxPitch;
        } else if (270 <= currPitch && currPitch < 360 - MaxPitch) {
            currPitch = -MaxPitch;
        }   

        goalTransform.rotation = Quaternion.Euler(currPitch, currRotation.y, 0);

        goalTransform.Translate(offset);

        // raycast from focus to the camera to see if anything is blocking it
        Vector3 direction = goalTransform.position - _focus;
        Ray ray = new Ray(_focus, direction.normalized);
        RaycastHit hit;
        LayerMask ignore = ~(LayerMask.GetMask("Player") | LayerMask.GetMask("Ignore Raycast") | LayerMask.GetMask("Interactable") |
            LayerMask.GetMask("Glass"));

        if (Physics.Raycast(ray, out hit, offset.magnitude, ignore, QueryTriggerInteraction.Ignore)) {
            goalTransform.position = hit.point;
        }

        // If transitioning out of override...
        if (s_OverrideTransitioning) {
            transform.position = Vector3.Lerp(transform.position, goalTransform.position, 10f * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, goalTransform.rotation, 20f * Time.deltaTime);
        } else {
            transform.position = goalTransform.position;
            transform.rotation = goalTransform.rotation;   
        }
    }
    public static Vector3 CustomWorldToScreenPoint(Vector3 position)
    {
        RectTransform gameUI = GameObject.Find("Game UI").GetComponent<RectTransform>();
        Vector3 point = Camera.main.WorldToScreenPoint(position);
        Vector3 customPoint = new Vector3(point.x / Camera.main.pixelWidth * gameUI.rect.width, point.y / Camera.main.pixelHeight * gameUI.rect.height,0);

        return customPoint;
    }

    public void StartFollowingPlayer()
    {
        cameraAnimated = false;
        gameObject.GetComponent<Animator>().enabled = false;
    }
}
