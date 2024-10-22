using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private RectTransform rectTransform;
    public Transform player; // Reference to the player's transform

    public Slider slider;
    public Image Border;
    public Image Circle;
    public Image HurtSkippy;
    private CanvasScaler canvasScaler;

    private Vector3 healthBarOffset = new Vector3(20, 30, 0); // Offset of the health bar above the player

    private float _interpolationValue = 0f;

    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = PlayerHealth.s_MaxHealth;
        rectTransform = GetComponent<RectTransform>();
        canvasScaler = GetComponentInParent<CanvasScaler>();

        // Set initial position relative to player
        if (player != null)
        {
            rectTransform.position = player.position + healthBarOffset;
        }
        _interpolationValue = 1f;
    }

    void Update()
    {
        // only make it visible when below max health or when power up is enabled
        // setting active to false would disable the Update function, so gotta do a hacky way to hide it
        if ((slider.value < PlayerHealth.s_MaxHealth || PlayerPowerup.DoubleJumpEnabled) && !CameraMain.s_CutSceneActive) {
            rectTransform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        } else {
            rectTransform.localScale = Vector3.zero;
        }

        if (PlayerPowerup.DoubleJumpEnabled) {
            // hide health bar when showing power up bar
            slider.value = 0;
            // don't animate bar filling up after power up
            _interpolationValue = 1f; 
        } else {
            // increase bar gradually
            if (slider.value != PlayerHealth.s_Health) {
                _interpolationValue += 0.75f * Time.deltaTime;
            }
            if (_interpolationValue > 1f) {
                _interpolationValue = 1f;
            }
            slider.value = Mathf.Lerp(slider.value, PlayerHealth.s_Health, _interpolationValue);
        }

        if (player != null)
        {
            // Get the reference resolution from the CanvasScaler
            Vector2 referenceResolution = canvasScaler.referenceResolution;
            float referenceWidth = referenceResolution.x;
            float referenceHeight = referenceResolution.y;

            // Calculate the scaling factors based on the current screen size and the reference resolution
            float widthScale = Screen.width / referenceWidth;
            float heightScale = Screen.height / referenceHeight;

            // Get the player's position on the screen
            Vector3 playerScreenPosition = Camera.main.WorldToScreenPoint(player.position);

            // Scale the health bar offset based on the screen size relative to the reference resolution
            Vector3 scaledHealthBarOffset = new Vector3(
                healthBarOffset.x * widthScale,
                healthBarOffset.y * heightScale,
                healthBarOffset.z
            );

            // Calculate the desired position for the health bar
            Vector3 healthBarScreenPosition = new Vector3(
                playerScreenPosition.x + scaledHealthBarOffset.x, // Move it to the right of the player
                playerScreenPosition.y + scaledHealthBarOffset.y, // Move it above the player
                playerScreenPosition.z
            );

            // Apply the calculated position to the health bar
            rectTransform.position = healthBarScreenPosition;
        }

        float healthPercent = (float)PlayerHealth.s_Health / PlayerHealth.s_MaxHealth;

        if (healthPercent <= 0)
        {
            // empty
            Border.color = new Color32(241, 80, 80, 255);
            Circle.color = new Color32(241, 80, 80, 150);
        } 
        else if (healthPercent <= 0.3) 
        {
            // low
            Border.color = new Color32(255, 255, 255, 255);
            Circle.color = new Color32(255, 255, 255, 255);
            HurtSkippy.enabled = true;
        } 
        else 
        {
            // full
            Border.color = new Color32(255, 255, 255, 255);
            Circle.color = new Color32(255, 255, 255, 255);
            HurtSkippy.enabled = false;
        }
    }

    public IEnumerator TailWhipUnavailable() {
                
        // flash 
        for (int i = 0; i < 5; i++) {
            Border.color = (i % 2 == 0) ? new Color32(241, 80, 80, 255) : new Color32(255, 255, 255, 255);
            Circle.color = (i % 2 == 0) ? new Color32(241, 80, 80, 150): new Color32(255, 255, 255, 255);

            yield return new WaitForSeconds(0.08f);
        }

        // don't flash for another 1.5 seconds
        yield return new WaitForSeconds(1.5f);
    }

    public void EnterWater() {
        Border.color = new Color32(255, 255, 255, 255);
        Circle.color = new Color32(255, 255, 255, 255);
        _interpolationValue = 0f;
    }
}
