using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Collectibles : MonoBehaviour
{
    public Sprite collectibleIcon;

    private int _numCollected;
    private Image _img;
    private TextMeshProUGUI _textUI;


    void Start()
    {
        _img = gameObject.GetComponentInChildren<Image>();
        _img.sprite = collectibleIcon;

        _textUI = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        _numCollected = 0;
        UpdateText();
        
        _img.enabled = false;
        _textUI.enabled = false;

    }

    void UpdateText() {
        _textUI.text = "x" + _numCollected;
    }

    public void CollectNew() {
        if (_numCollected == 0) {  
            _img.enabled = true;
            _textUI.enabled = true;
        }
        _numCollected += 1;
        UpdateText();
    }
}
