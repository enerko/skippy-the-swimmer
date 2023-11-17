using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerTypeHandler : MonoBehaviour
{
    public enum ControllerType
    {
        Keyboard,
        Gamepad,
    }
    public static ControllerType currentController = ControllerType.Keyboard;
    private PlayerInput _controls;


    public enum PromptKey {
        Q,
        E,
        SPACE,
        TAB,
        WASD,
        MOUSE,
        LEFT,
        EAST,
        WEST,
        //WEST_DARK,
        SOUTH
    }

    // Use this prompt's promptKey attribute to return the corresponding icon to use
    // Lord help us
    public static Sprite GetIcon(PromptKey promptKey) {
        Texture2D text;

        switch(promptKey) {
            case PromptKey.Q:
                text = Resources.Load<Texture2D>("prompt_Q");
                break;
            case PromptKey.E:
                text = Resources.Load<Texture2D>("prompt_E");
                break;
            case PromptKey.SPACE:
                text = Resources.Load<Texture2D>("prompt_SPACE");
                break;
            case PromptKey.TAB:
                text = Resources.Load<Texture2D>("prompt_TAB");
                break;
            case PromptKey.WASD:
                text = Resources.Load<Texture2D>("prompt_WASD");
                break;
            case PromptKey.MOUSE:
                text = Resources.Load<Texture2D>("prompt_MOUSE");
                break;
            case PromptKey.LEFT:
                text = Resources.Load<Texture2D>("prompt_LEFT");
                break;
            case PromptKey.EAST:
                text = Resources.Load<Texture2D>("prompt_EAST");
                break;
            case PromptKey.WEST:
                text = Resources.Load<Texture2D>("prompt_WEST");
                break;
            // case PromptKey.WEST_DARK:
            //     text = Resources.Load<Texture2D>("prompt_darkoutline_WEST");
            //     break;
            case PromptKey.SOUTH:
                text = Resources.Load<Texture2D>("prompt_SOUTH");
                break;
            default:
                return null;
        }

        return Sprite.Create(text, new Rect(0.0f, 0.0f, text.width, text.height), new Vector2(0.5f, 0.5f));
    }

    private void Awake()
    {
        _controls = FindObjectOfType<PlayerInput>();
        _controls.onControlsChanged += OnControlsChanged;
        OnControlsChanged(_controls);
    }

    private void OnControlsChanged(PlayerInput obj)
    {
        foreach (var device in obj.devices)
        {
            var name = device.description.ToString().ToLower();
            if (!name.Contains("keyboard") && !name.Contains("mouse")) {
                currentController = ControllerType.Gamepad;
                return;
            }
        }
        currentController = ControllerType.Keyboard;
    }

}
