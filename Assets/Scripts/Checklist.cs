using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class Checklist : MonoBehaviour
{
    private Dictionary<string, bool> _checklist = new Dictionary<string, bool>();

    private Image _background;
    private TextMeshProUGUI _text;

    void Start()
    {
        _background = gameObject.GetComponentInChildren<Image>();
        _text = gameObject.GetComponentInChildren<TextMeshProUGUI>();

        _background.enabled = false;
        _text.enabled = false;

        foreach (var obj in Object.FindObjectsOfType<Collectible>())
        {
            _checklist[obj.GetComponent<Collectible>().description] = false;
        }

        UpdateText();
    }

    private void UpdateText() 
    {
        string text = "Checklist: <br><br>";
        foreach (var description in _checklist.Keys) {
            text += _checklist[description] ? "[x]" : "[ ]";
            text += " " + description + "<br><br>";
        }
        _text.text = text;
    }

    public void UpdateChecklist(string description) 
    {
        _checklist[description] = true;
        UpdateText();
    }

    public void DisplayChecklist(InputValue inputValue)
    {
        if (inputValue.isPressed)
        {
            _background.enabled = true;
            _text.enabled = true;
            Player.s_ChecklistOpen = true;
        } else {
            _background.enabled = false;
            _text.enabled = false;
            Player.s_ChecklistOpen = false;
        }
    }
}
