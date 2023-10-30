using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Events;

public class Collectibles : MonoBehaviour
{
    public Sprite collectibleIcon;
    public int numCollected;
    public UnityEvent allCollected;

    private Image _img;
    private TextMeshProUGUI _textUI;
    private GameObject _magpie;
    private const int GoalNum = 5;
    private Collectible[] pearls;  // manage all the pearls spawning

    void Start()
    {
        _img = gameObject.GetComponentInChildren<Image>();
        _img.sprite = collectibleIcon;

        _textUI = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        numCollected = 0;
        UpdateText();
        
        _img.enabled = false;
        _textUI.enabled = false;

        _magpie = GameObject.Find("/Magpie");

        pearls = FindObjectsOfType<Collectible>();
        SetPearlsActive(false);  // initially inactive
    }

    void UpdateText() {
        _textUI.text = "x" + numCollected;
    }

    public void CollectNew() {
        if (numCollected == 0) {  
            _img.enabled = true;
            _textUI.enabled = true;
        }

        numCollected += 1;

        if (numCollected == GoalNum) {
            allCollected.Invoke();
        }

        UpdateText();
    }

    public void SetPearlsActive(bool active) {
        foreach (Collectible collectible in pearls)
        {
            collectible.gameObject.SetActive(active);
        }
    }
}
