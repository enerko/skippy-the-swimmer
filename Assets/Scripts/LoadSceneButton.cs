using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneButton : MonoBehaviour
{
    public string sceneName;
    public bool hideCursor = true;  // should the next scene hide the cursor?
    public bool resetTimer = false;
    public bool resetCutscene = false;

    public void LoadScene() {
        Globals.LoadScene(sceneName, hideCursor, resetTimer, resetCutscene);
    }
}
