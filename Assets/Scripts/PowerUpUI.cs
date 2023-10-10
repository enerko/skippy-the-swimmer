using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpUI : MonoBehaviour
{
    public Slider slider;
    public Image fill;

    static float time = 0;

    private void Start()
    {
        time = 0;
    }

    void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
            float normalizedTime = Mathf.Clamp01(time / Powerup.PowerUpDuration);

            slider.value = normalizedTime;
        }
    }

    public static void ShowUI()
    {
        time = Powerup.PowerUpDuration;
    }
}
