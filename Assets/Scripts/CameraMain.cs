using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMain : MonoBehaviour
{
    private readonly Vector3 Offset = new Vector3(0, 2, -7);
    private float _sensitivity = 15f;
    private Vector3 focus;
    private Vector3 velocity;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;  // so you can still move camera even if mouse is at edge of screen
 	    Cursor.visible = false;
        focus = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.GameIsPaused) return;

        focus = Vector3.SmoothDamp(focus, player.transform.position, ref velocity, 0.1f);
        transform.position = focus;

        // moving mouse horizontally
        gameObject.transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * _sensitivity, 0), Space.World);

        gameObject.transform.Translate(Offset);
    }
}
