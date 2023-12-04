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
    private Collectible[] pearls;  // manage all the pearls spawning
    private bool _firstFrame = true;

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
    }

    // on the first frame only, set the pearls to inactive
    // we can't set them to inactive in Start or else AchievementsManager may not find them
    void Update() {
        if (!_firstFrame) return;
        SetPearlsActive(false);  // initially inactive
        _firstFrame = false;
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

        if (numCollected == pearls.Length) {
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
