using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    public GameObject achievementsPrompt;
    public Objectives objectives;

    // Update is called once per frame
    void Update() 
    {
        if (CameraMain.s_CutSceneActive)
        {
            achievementsPrompt.SetActive(false);
            objectives.HideObjectives();
        }
        else
        {
            objectives.ShowObjectives();
        }
    }
}