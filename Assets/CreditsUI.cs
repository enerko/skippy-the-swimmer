using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsUI : MonoBehaviour
{
    private RectTransform rc;
    bool imageShown = false;
    public GameObject title;

    public void StartCredits()
    {
        rc = GetComponent<RectTransform>();
        rc.localPosition = new Vector3(195.300003f, -321, 0);
        StartCoroutine(WaitForImage());
        title.SetActive(true); 
    }

    private IEnumerator WaitForImage()
    {
        for (float i = 0; i <= 3; i += Time.deltaTime)
        {
            yield return null;
        }
        imageShown = true;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(rc.localPosition);
        Debug.Log(imageShown);
        if (rc.localPosition.y > 1100 || !imageShown) return;

        rc.localPosition = new Vector3(rc.localPosition.x, rc.localPosition.y + 0.5f, rc.localPosition.z);
    }
}
