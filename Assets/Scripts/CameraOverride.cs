using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOverride : MonoBehaviour
{
    public Transform goal;

    void OnTriggerStay(Collider other) {
        if (CameraMain.s_CameraOverride || CameraMain.s_OverrideTransitioning) return;  // do nothing if already overridden or currently transitioning

        StopAllCoroutines();  // prevent conflict with restoring
        StartCoroutine(OverrideCamera());
    }

    void OnTriggerExit(Collider other) {
        StartCoroutine(RestoreCamera());
    }

    private IEnumerator OverrideCamera() {
        CameraMain.s_CameraOverride = goal;
        CameraMain.s_OverrideTransitioning = true;

        yield return new WaitForSeconds(CameraMain.OverrideTime);

        CameraMain.s_OverrideTransitioning = false;
    }

    private IEnumerator RestoreCamera() {
        yield return new WaitUntil(() => !CameraMain.s_OverrideTransitioning);

        CameraMain.s_CameraOverride = null;
        CameraMain.s_OverrideTransitioning = true;

        yield return new WaitForSeconds(CameraMain.OverrideTime);

        CameraMain.s_OverrideTransitioning = false;
    }
}
