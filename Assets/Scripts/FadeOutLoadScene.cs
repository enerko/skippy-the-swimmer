using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOutLoadScene : MonoBehaviour
{
    public string sceneName;
    public Image fadeImage;

    // The trigger is set so it only registers player collision, you can assume only the player triggers it
    public void OnTriggerEnter(Collider _) {
        StartCoroutine(Fade());
    }

    private IEnumerator Fade() {
        Globals.s_CanPause = false;
        Player.s_CanMove = false;

        // wtf? https://stackoverflow.com/questions/42330509/crossfadealpha-not-working
        Color fixedColor = fadeImage.color;
        fixedColor.a = 1;
        fadeImage.color = fixedColor;
        fadeImage.CrossFadeAlpha(0f, 0f, true);
        fadeImage.CrossFadeAlpha(1, 3, false);

        yield return new WaitForSeconds(5);
        Globals.LoadScene(sceneName);
    }
}
