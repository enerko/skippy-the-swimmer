using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private RectTransform rectTransform;
    public Transform player; // Reference to the player's transform
    private List<float> offsets = new List<float>();

    public Slider slider;
    public Image Border;
    public Image Circle;
    public Image HurtSkippy;
    private bool isShaking = false;
    private CanvasScaler canvasScaler;

    private Vector3 healthBarOffset = new Vector3(20, 30, 0); // Offset of the health bar above the player

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
        
        offsets.Add(-5);
        offsets.Add(0);
        offsets.Add(5);
        offsets.Add(0);
    }

    void Update()
    {
        slider.value = PlayerHealth.s_Health;

        if (player != null && !isShaking)
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
            HurtSkippy.enabled = false;
        }
    }

    public IEnumerator Shake() {
        isShaking = true;
        
        // Initial position in screen space
        Vector2 shakeStartPosition = rectTransform.anchoredPosition;

        for (int i = 0; i < offsets.Count; i++) {
            yield return new WaitForSeconds(0.08f);
            // Apply the offsets to the x position in screen space
            rectTransform.anchoredPosition = new Vector2(shakeStartPosition.x + offsets[i], shakeStartPosition.y);
        }

        // Return to the initial position in screen space
        rectTransform.anchoredPosition = shakeStartPosition;
        yield return new WaitForSeconds(1.5f);
        isShaking = false;
    }
}
