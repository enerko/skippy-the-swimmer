using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class HintManager : MonoBehaviour
{
    private Dictionary<string, bool> _checklist = new Dictionary<string, bool>();

    private Image _background;
    private TextMeshProUGUI _text;
    private bool _pearlsSet = false;

    public GameObject prompt;

    void Start()
    {
        _background = gameObject.GetComponentInChildren<Image>();
        _text = gameObject.GetComponentInChildren<TextMeshProUGUI>();

        _background.enabled = false;
        _text.enabled = false;

        foreach (var obj in Object.FindObjectsOfType<Collectible>())
        {
            _checklist[obj.GetComponent<Collectible>().description] = false;
            obj.ObjectCollectedEvent += UpdateChecklist;
        }

        UpdateText();
    }

    private void UpdateText()
    {
        string text = "Hints: <br><br>";
        foreach (var description in _checklist.Keys)
        {
            text += _checklist[description] ? "[x]" : "[ ]";
            text += " " + description + "<br><br>";
        }
        _text.text = text;
    }

    public void SetHints()
    {
        _pearlsSet = true;
        prompt.SetActive(true);
    }

    public void UpdateChecklist(GameObject obj)
    {
        Collectible pearl = obj.GetComponent<Collectible>();
        if (pearl)
        {
            _checklist[pearl.description] = true;
            UpdateText();
        }
        
    }

    public void DisplayChecklist(InputValue inputValue)
    {
        if (!_pearlsSet) return;
        if (inputValue.isPressed)
        {
            _background.enabled = true;
            _text.enabled = true;
        }
        else
        {
            _background.enabled = false;
            _text.enabled = false;
        }
    }
}
