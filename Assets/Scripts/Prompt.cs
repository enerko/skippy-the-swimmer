using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.Intrinsics;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;

public class Prompt : MonoBehaviour
{
    public string promptText;  // what to do?
    public ControllerTypeHandler.PromptKey promptKeyController;  // which key to prompt?
    public ControllerTypeHandler.PromptKey promptKeyKeyboard;
    [NonSerialized]
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
        _promptGui.transform.SetParent(_promptContainer.transform, false);
        _promptContainer.transform.SetParent(_promptContainer.transform.parent, false);
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

        Image promptIcon = clone.transform.Find("Prompt Icon").GetComponent<Image>();
        if (ControllerTypeHandler.currentController == ControllerTypeHandler.ControllerType.Gamepad) {
            promptIcon.sprite = ControllerTypeHandler.GetIcon(promptKeyController);
        } else {
            promptIcon.sprite =  ControllerTypeHandler.GetIcon(promptKeyKeyboard);
        }
        
        return clone;
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
