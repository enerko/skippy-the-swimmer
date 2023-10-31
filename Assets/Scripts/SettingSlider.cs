using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements.Experimental;

public class SettingSlider : MonoBehaviour
{
    public string settingName;
    public float defaultValue;

    private Slider _slider;

    // Start is called before the first frame update
    void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.value = PlayerPrefs.GetFloat(settingName, defaultValue);
    }

    public void OnValueChanged() {
        PlayerPrefs.SetFloat(settingName, _slider.value);
    }
}
