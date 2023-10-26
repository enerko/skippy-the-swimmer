using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PopUpManager : MonoBehaviour
{
    public Text popupText;
    public float popupDuration = 3.0f;

    private void Start()
    {
        // Ensure the pop-up is initially hidden
        HidePopup();
    }

    public void ShowPopup(string achievementName)
    {
        popupText.text = "New achievement unlocked! " + achievementName;
        gameObject.SetActive(true);
        StartCoroutine(ShowAndHidePopup());
    }

    private IEnumerator ShowAndHidePopup()
    {
        // Wait for a specified duration
        yield return new WaitForSeconds(popupDuration);

        // Hide the pop-up
        HidePopup();
    }

    private void HidePopup()
    {
        gameObject.SetActive(false);
    }
}