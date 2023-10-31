using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpUI : MonoBehaviour
{
    private Image _filterImage; // Reference to the Image component on this GameObject
    private float _time = 0;

    private void Start()
    {
        // Get the Image component from this GameObject
        _filterImage = GetComponent<Image>();
        if (_filterImage != null)
        {
            // Start with the image disabled
            _filterImage.enabled = false;
        }
    }

    void Update()
    {
        if (_time > 0)
        {
            _time -= Time.deltaTime;
        }
        else if (_filterImage != null && _filterImage.enabled)
        {
            // When the power-up duration ends, disable the image
            _filterImage.enabled = false;
        }
    }

    public void Show()
    {
        _time = PlayerPowerup.PowerUpDuration;

        // Enable the image when the power-up is active
        if (_filterImage != null)
        {
            _filterImage.enabled = true;
        }
    }
}
