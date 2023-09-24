using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Material healthHigh;
    public Material healthMid;
    public Material healthLow;
    public GameObject[] healthBoxes;

    // Start is called before the first frame update
    void Start()
    {
    }

    void Update()
    {
        // see whether each box should be filled
        for (int i = 0; i < healthBoxes.Length; i++)
        {
            GameObject healthBox = healthBoxes[i];
            float healthPercent = (float)PlayerHealth.s_Health / PlayerHealth.s_MaxHealth;
            healthBox.SetActive(healthPercent > 0.1 * i);

            // colour the box depending on health percentage
            if (healthPercent <= 0.2)
            {
                healthBox.GetComponent<Renderer>().material = healthLow;
            } else if (healthPercent <= 0.5)
            {
                healthBox.GetComponent<Renderer>().material = healthMid;
            } else
            {
                healthBox.GetComponent<Renderer>().material = healthHigh;
            }
        }
    }
}
