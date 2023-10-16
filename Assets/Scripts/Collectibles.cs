using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Collectibles : MonoBehaviour
{
    public Sprite collectibleIcon;

    private int _numCollected;

    private TextMeshProUGUI textUI;


    void Start()
    {
        gameObject.GetComponentInChildren<Image>().sprite = collectibleIcon;
        textUI = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        _numCollected = 0;
        UpdateText();
    }

    void UpdateText() {
        textUI.text = "x" + _numCollected;
    }

    public void CollectNew() {
        _numCollected += 1;
        UpdateText();
    }
}
