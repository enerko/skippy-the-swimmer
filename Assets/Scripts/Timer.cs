using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Time the player
public class Timer : MonoBehaviour
{
    public static float s_TimePlayed = 0;  // time played in seconds
    public static bool s_Enabled = true;

    // Update is called once per frame
    void Update()
    {
        if (s_Enabled) {
            s_TimePlayed += Time.deltaTime;
        }
    }

    // Return formatted time
    public static string GetTime() {
        int minutes = (int)(s_TimePlayed / 60);  // get whole minutes, cap it at 99
        int seconds = (int)s_TimePlayed % 60;
        float milli = (s_TimePlayed - Mathf.Floor(s_TimePlayed)) * 100;  // get the decimal part, first two digits are left of decimal point

        return string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milli);
    }

    // Reset the timer and play it
    public static void ResetTimer() {
        s_Enabled = true;
        s_TimePlayed = 0;
    }
}
