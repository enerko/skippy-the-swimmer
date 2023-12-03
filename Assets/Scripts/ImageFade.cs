using UnityEngine;
using UnityEngine.UI;
using System.Collections;
 
public class ImageFade : MonoBehaviour
{
    private Image img;
    public int fadeTime;

    public void ShowCreditsImage(bool show)
    {
        img = GetComponent<Image>();
        StartCoroutine(FadeImage(show));
    }

    IEnumerator FadeImage(bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            for (float i = fadeTime; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                img.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
        // fade from transparent to opaque
        else
        {
            for (float i = 0; i <= fadeTime; i += Time.deltaTime)
            {
                // set color with i as alpha
                img.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
    }
}

