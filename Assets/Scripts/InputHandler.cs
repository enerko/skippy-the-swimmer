using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private Player player;
    private CameraMain cameraMain;
    private PlayerControls playerControls;

    private void Awake()
    {
        playerControls = new PlayerControls();
        player = GameObject.FindFirstObjectByType<Player>();
        cameraMain = GameObject.FindFirstObjectByType<CameraMain>();
    }

    private void OnJump()
    {
        player?.PerformJump();
    }

    private void OnMove(InputValue inputValue)
    {
        player?.PerformMove(inputValue);
    }

    private void OnAttack(InputValue inputValue)
    {
        player?.PerformAttack(inputValue);
    }

    private void OnCameraRotate(InputValue inputValue)
    {
        cameraMain?.PerformCameraRotate(inputValue);
    }

    private void OnTalk()
    {
        player.PerformTalk();
    }

}
