using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public enum ControlDeviceType
    {
        Keyboard,
        Gamepad,
    }
     public static ControlDeviceType currentControlDevice = ControlDeviceType.Keyboard;
     private PlayerInput _controls;
        
    private Player player;
    private CameraMain cameraMain;
    private PlayerControls playerControls;
    private AchievementsManager achievements;

    private void Awake()
    {
        playerControls = new PlayerControls();

         _controls = FindObjectOfType<PlayerInput>();
        _controls.onControlsChanged += OnControlsChanged;
        OnControlsChanged(_controls);

        player = GameObject.FindFirstObjectByType<Player>();
        cameraMain = GameObject.FindFirstObjectByType<CameraMain>();
        achievements = FindObjectOfType<AchievementsManager>();
    }

    private void OnControlsChanged(PlayerInput obj)
    {
         foreach (var device in obj.devices)
        {
            var name = device.description.ToString().ToLower();
            if (!name.Contains("keyboard") && !name.Contains("mouse")) {
                currentControlDevice = ControlDeviceType.Gamepad;
                return;
            }
        }
        currentControlDevice = ControlDeviceType.Keyboard;
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

    private void OnViewChecklist(InputValue inputValue) 
    {
        achievements?.DisplayAchievements(inputValue);
    }

}
