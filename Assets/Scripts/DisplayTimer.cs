using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Make a text label display a timer
public class DisplayTimer : MonoBehaviour
{
    public TextMeshProUGUI text;

    // Update is called once per frame
    void Update()
    {
        text.text = Timer.GetTime();
        UpdateVisibility();
    }

    // Update visibility, this can be called externally to make this object active again
    public void UpdateVisibility() {
        gameObject.SetActive(PlayerPrefs.GetFloat("Timer", 1) == 1);
    }
}
