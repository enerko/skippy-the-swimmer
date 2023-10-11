using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Image CircleFill;
    public Image GaugeFill;
    public Image HurtSkippy;

    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = PlayerHealth.s_MaxHealth;
    }

    void Update()
    {
        slider.value = PlayerHealth.s_Health;

        
        float healthPercent = (float)PlayerHealth.s_Health / PlayerHealth.s_MaxHealth;

        // colour the box depending on health percentage
        if (healthPercent <= 0.3)
        {
            // low
            CircleFill.color = new Color32(255, 76, 61, 255);
            GaugeFill.color = new Color32(255, 76, 61, 255);
            HurtSkippy.enabled = true;
        } else 
        {
            // full
            CircleFill.color = new Color32(255, 255, 225, 255); 
            GaugeFill.color = new Color32(255, 255, 225, 255); 
            HurtSkippy.enabled = false;
        }
        
    }
}
