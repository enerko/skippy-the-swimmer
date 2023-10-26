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
    private Image _img;
    private TextMeshProUGUI _textUI;
    private GameObject _magpie;
    private int goalNum = 5;

    // event for when all collectibles are collected
    public UnityEvent onAllCollected;
    public UnityEvent onAllCollectedPremature;  // if all are collected before talking to magpie

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

        if (numCollected == goalNum) {
            ConversationPrompt magpiePrompt = _magpie.GetComponent<ConversationPrompt>();

            // If force is still true, you haven't talked to magpie yet
            if (magpiePrompt.force) {
                onAllCollectedPremature.Invoke();
            } else {
                onAllCollected.Invoke();
            }
        }

        UpdateText();
    }
}
