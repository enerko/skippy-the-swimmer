using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class HintManager : MonoBehaviour
{
    private Dictionary<string, AchievementRowUI> _checklist = new Dictionary<string, AchievementRowUI>();

    private Image _background;
    private VerticalLayoutGroup _layoutGroup;
    private bool _pearlsSet = false;

    public GameObject achievementRowPrefab;
    public GameObject prompt;

    void Start()
    {
        _background = gameObject.GetComponentInChildren<Image>();
        _layoutGroup = gameObject.GetComponentInChildren<VerticalLayoutGroup>();

        _background.enabled = false;
        
        foreach (var obj in Object.FindObjectsOfType<Collectible>())
        {
            obj.ObjectCollectedEvent += UpdateChecklist;
            GameObject row = Instantiate(achievementRowPrefab, _layoutGroup.transform);
            row.transform.SetParent(_layoutGroup.transform);
            AchievementRowUI rowUI = row.GetComponent<AchievementRowUI>();
            rowUI.SetInitialValues(obj);
            _checklist[obj.GetComponent<Collectible>().description] = rowUI;
        }

        _layoutGroup.gameObject.SetActive(false);

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
            _checklist[pearl.description].Collected();
        }
        
    }

    public void DisplayChecklist(InputValue inputValue)
    {
        if (!_pearlsSet) return;
        if (inputValue.isPressed)
        {
            _background.enabled = true;
            _layoutGroup.gameObject.SetActive(true);
            LayoutRebuilder.ForceRebuildLayoutImmediate(_layoutGroup.gameObject.transform as RectTransform);
        }
        else
        {
            _background.enabled = false;
            _layoutGroup.gameObject.SetActive(false);
        }
    }
}
