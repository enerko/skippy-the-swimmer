using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public bool active;  // if prompt should be shown

    private GameObject _promptTemplate;
    private GameObject _promptContainer;
    private GameObject _promptGui;
    private bool _disabled;  // if the Disable method was called

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
        _promptGui.SetActive(active);
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
                text = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Images/prompt_Q.png");
                break;
            case PromptKey.E:
                text = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Images/prompt_E.png");
                break;
            case PromptKey.SPACE:
                text = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Images/prompt_SPACE.png");
                break;
            case PromptKey.TAB:
                text = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Images/prompt_TAB.png");
                break;
            case PromptKey.WASD:
                text = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Images/prompt_WASD.png");
                break;
            case PromptKey.MOUSE:
                text = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Images/prompt_MOUSE.png");
                break;
            case PromptKey.LEFT:
                text = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Images/prompt_LEFT.png");
                break;
            case PromptKey.EAST:
                text = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Images/prompt_EAST.png");
                break;
            case PromptKey.WEST:
                text = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Images/prompt_WEST.png");
                break;
            case PromptKey.SOUTH:
                text = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Images/prompt_SOUTH.png");
                break;
            default:
                return null;
        }

        return Sprite.Create(text, new Rect(0.0f, 0.0f, text.width, text.height), new Vector2(0.5f, 0.5f));
    }

    void OnTriggerStay(Collider other) {
        active = true;
    }

    void OnTriggerExit(Collider other) {
        active = false;
    }

    // Completely disable this prompt
    public void Disable() {
        active = false;
        _disabled = true;
        Destroy(_promptGui);
    }
}
