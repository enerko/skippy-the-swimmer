using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMain : MonoBehaviour
{
    public Vector3 offset = new Vector3(0, 2, -7);
    private float _sensitivity = 3f;
    private const int MaxPitch = 60;  // max vertical rotation bounds
    private Vector3 _focus;
    private Vector3 _velocity;
    private Vector2 _input;

    public GameObject player;

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

        _focus = Vector3.SmoothDamp(_focus, player.transform.position, ref _velocity,  0.25f);
        transform.position = _focus;

        // rotate
        transform.Rotate(new Vector3(0, _input.x * _sensitivity, 0), Space.World);
        transform.Rotate(new Vector3(-_input.y * _sensitivity, 0, 0));

        // cap camera's pitch
        Vector3 currRotation = transform.rotation.eulerAngles;

        // pitch ranges from 0 - 360 even though in the editor it goes into negatives, so can't use Mathf.Clamp here
        float currPitch = currRotation.x;
       
        if (MaxPitch < currPitch && currPitch <= 90) {
            currPitch = MaxPitch;
        } else if (270 <= currPitch && currPitch < 360 - MaxPitch) {
            currPitch = -MaxPitch;
        }   

        transform.rotation = Quaternion.Euler(currPitch, currRotation.y, 0);

        transform.Translate(offset);

        // raycast from focus to the camera to see if anything is blocking it
        Vector3 direction = transform.position - _focus;
        Ray ray = new Ray(_focus, direction.normalized);
        RaycastHit hit;
        LayerMask ignore = ~(LayerMask.GetMask("Player") | LayerMask.GetMask("Ignore Raycast") | LayerMask.GetMask("Interactable"));

        if (Physics.Raycast(ray, out hit, offset.magnitude, ignore, QueryTriggerInteraction.Ignore)) {
            transform.position = hit.point;
        }
    }
    public static Vector3 CustomWorldToScreenPoint(Vector3 position)
    {
        RectTransform gameUI = GameObject.Find("Game UI").GetComponent<RectTransform>();
        Vector3 point = Camera.main.WorldToScreenPoint(position);
        Vector3 customPoint = new Vector3(point.x / Camera.main.pixelWidth * gameUI.rect.width, point.y / Camera.main.pixelHeight * gameUI.rect.height,0);

        return customPoint;
    }
}
