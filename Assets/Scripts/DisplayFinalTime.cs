using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// For use on the win screen, display the final time
public class DisplayFinalTime : MonoBehaviour
{
    public string formatString;  // A string containing {0}, which will be replaced with the final time
    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        Timer.s_Enabled = false;
        text.text = string.Format(formatString, Timer.GetTime());
    }
}
