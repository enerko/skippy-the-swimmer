using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Image fill;

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
            fill.color = new Color32(219, 30, 62, 255); // low
        } else 
        {
            fill.color = new Color32(102, 169, 225, 255); // full
        }
        
    }
}
