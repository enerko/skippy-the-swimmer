using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private RectTransform rectTransform;
    private Vector3 initialPosition;

    List<float> offsets = new List<float>();
    
    public Slider slider;
    public Image Border;
    public Image Circle;
    public Image HurtSkippy;

    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = PlayerHealth.s_MaxHealth;
        rectTransform = GetComponent<RectTransform>();
        initialPosition = rectTransform.position;
        
        offsets.Add(-20);
        offsets.Add(0);
        offsets.Add(20);
        offsets.Add(0);
    }

    void Update()
    {
        slider.value = PlayerHealth.s_Health;

        
        float healthPercent = (float)PlayerHealth.s_Health / PlayerHealth.s_MaxHealth;

        if (healthPercent <= 0)
        {
            // empty
            Border.color = new Color32(241, 80, 80, 255);
            Circle.color = new Color32(241, 80, 80, 150);
        } else if (healthPercent <= 0.3) {
            // low
            Border.color = new Color32(255, 255, 255, 255);
            Circle.color = new Color32(255, 255, 255, 255);
            HurtSkippy.enabled = true;
        } else 
        {
            // full
            HurtSkippy.enabled = false;
        }
    }

    public IEnumerator Shake() {
        foreach (var offset in offsets)
        {
            yield return new WaitForSeconds(0.08f);
            rectTransform.position = new Vector3(1, 0, 0) * offset + initialPosition;
            yield return null;
        }

        yield return new WaitForSeconds(1.5f);
    }
}
