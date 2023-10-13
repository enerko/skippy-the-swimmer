using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMain : MonoBehaviour
{
    private readonly Vector3 Offset = new Vector3(0, 2, -7);
    private float _sensitivity = 15f;
    private const int MaxPitch = 60;  // max vertical rotation bounds
    private Vector3 _focus;
    private Vector3 _velocity;

    public GameObject player;
    private PlayerControls cameraControls;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;  // so you can still move camera even if mouse is at edge of screen
 	    Cursor.visible = false;
        _focus = player.transform.position;
        cameraControls = new PlayerControls();
    }

    private void OnRotateCamera(InputValue inputValue)
    {
        Vector2 inputX = inputValue.Get<Vector2>();
        transform.Rotate(new Vector3(0, inputX.x * _sensitivity, 0), Space.World);
        transform.Rotate(new Vector3(-inputX.y * _sensitivity, 0, 0));
    }
    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.GameIsPaused) return;

        _focus = Vector3.SmoothDamp(_focus, player.transform.position, ref _velocity,  0.25f);
        transform.position = _focus;

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

        transform.Translate(Offset);

        // raycast from focus to the camera to see if anything is blocking it
        Vector3 direction = transform.position - _focus;
        Ray ray = new Ray(_focus, direction.normalized);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, Offset.magnitude, ~LayerMask.GetMask("Player"), QueryTriggerInteraction.Ignore)) {
            transform.position = hit.point;
        }
    }
}
