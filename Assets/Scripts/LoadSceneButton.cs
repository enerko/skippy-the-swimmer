using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneButton : MonoBehaviour
{
    public string sceneName;
    public void LoadScene() {
        Globals.LoadScene(sceneName);
    }
}
