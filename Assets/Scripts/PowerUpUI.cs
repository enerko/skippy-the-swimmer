using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpUI : MonoBehaviour
{
    public Slider slider;
    public Image doubleJumpLogo;
    private GameObject _prompt;
    private float _time = 0;

    private void Start()
    {
        slider.maxValue = PlayerPowerup.PowerUpDuration;
        doubleJumpLogo.enabled = false;
        _prompt = gameObject.transform.Find("Prompt").gameObject;
    }

    void Update()
    {
        // Check if a conversation is active
        if (Player.s_ConversationActive)
        {
            return; // Skip updating the timer when a conversation is active
        }

        if (_time > 0)
        {
            _time -= Time.deltaTime;
            slider.value = _time;
        }
        else if (doubleJumpLogo.enabled)
        {
            _prompt.SetActive(false);
            doubleJumpLogo.enabled = false;
        }
    }

    public void Show()
    {
        _time = PlayerPowerup.PowerUpDuration;
        
        if (!doubleJumpLogo.enabled)
        {
            _prompt.SetActive(true);
            doubleJumpLogo.enabled = true;
        }
    }
}
