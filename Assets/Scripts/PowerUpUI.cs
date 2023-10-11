using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpUI : MonoBehaviour
{
    private Slider _slider;
    private Image _icon;
    
    private float _time = 0;

    private void Start()
    {
        _time = 0;
        _icon = transform.GetChild(1).GetComponent<Image>();
        _slider = transform.GetComponent<Slider>();
    }

    void Update()
    {
        if (_time > 0)
        {
            _time -= Time.deltaTime;
            float normalizedTime = Mathf.Clamp01(_time / PlayerPowerup.PowerUpDuration);

            _slider.value = normalizedTime;
        } else
        {
            _icon.enabled = false;
        }
    }

    public void Show()
    {
        _time = PlayerPowerup.PowerUpDuration;
        _icon.enabled = true;
    }
}