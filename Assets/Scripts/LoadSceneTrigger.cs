using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneTrigger : MonoBehaviour
{
    public string sceneName;

    // The trigger is set so it only registers player collision, you can assume only the player triggers it
    public void OnTriggerEnter(Collider _) {
        Globals.LoadScene(sceneName);
    }
}
