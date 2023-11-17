using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.Intrinsics;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Prompt : MonoBehaviour
{
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
        SOUTH
    }

    public string promptText;  // what to do?
    public PromptKey promptKeyController;  // which key to prompt?
    public PromptKey promptKeyKeyboard;
    public bool hidden = false;  // use this to override whether the prompt should be shown at all

    private GameObject _promptTemplate;
    private GameObject _promptContainer;
    private GameObject _promptGui;
    private bool _disabled;  // if the Disable method was called, completely ending this prompt
    protected bool _active;  // if prompt should be shown when not hidden

    protected void Start() {
        _promptTemplate = GameObject.Find("/Game UI/Prompt Template");
        _promptContainer = GameObject.Find("/Game UI/Prompt Container");
        _promptGui = CreatePrompt();
        _promptGui.transform.SetParent(_promptContainer.transform);
    }

    // Update is called once per frame
    protected void Update()
    {
        if (_disabled) {
            return;
        }

        // move it with the camera
        _promptGui.GetComponent<RectTransform>().anchoredPosition = CameraMain.CustomWorldToScreenPoint(transform.position);
        _promptGui.SetActive(hidden ? false : _active);
    }

    // Create the prompt gui for this prompt
    GameObject CreatePrompt() {
        GameObject clone = Instantiate(_promptTemplate);
        TextMeshProUGUI text = clone.transform.Find("Text").GetComponent<TextMeshProUGUI>();
        text.text = promptText;

        Image controllerIcon = clone.transform.Find("Controller Icon").GetComponent<Image>();
        controllerIcon.sprite = GetIcon(promptKeyController);

        Image keyboardIcon = clone.transform.Find("Keyboard Icon").GetComponent<Image>();
        keyboardIcon.sprite = GetIcon(promptKeyKeyboard);

        return clone;
    }

    // Use this prompt's promptKey attribute to return the corresponding icon to use
    // Lord help us
    Sprite GetIcon(PromptKey promptKey) {
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
            case PromptKey.SOUTH:
                text = Resources.Load<Texture2D>("prompt_SOUTH");
                break;
            default:
                return null;
        }

        return Sprite.Create(text, new Rect(0.0f, 0.0f, text.width, text.height), new Vector2(0.5f, 0.5f));
    }

    void OnTriggerStay(Collider other) {
        _active = true;
    }

    void OnTriggerExit(Collider other) {
        _active = false;
    }

    // Completely disable this prompt
    public void Disable() {
        _active = false;
        _disabled = true;
        Destroy(_promptGui);
    }

    public void SetHidden(bool v) {
        hidden = v;
    }
}
