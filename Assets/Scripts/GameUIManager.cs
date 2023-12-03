using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    public GameObject achievementsPrompt;
    public Objectives objectives;
    public DisplayTimer timer;
    public HealthBar healthBar;
    public GameObject promptTemplate;
    public GameObject promptContainer;

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

    public void HideTimer()
    {
        timer.HideTimer();
    }

    public void HideObjectives()
    {
        objectives.HideObjectives();
    }

    public void HideHealth()
    {
        healthBar.gameObject.SetActive(false);
    }

    public void HidePrompts()
    {
        promptContainer.gameObject.SetActive(false);
        promptTemplate.gameObject.SetActive(false);
    }

    public void HideAll()
    {
        HideTimer();
        HideObjectives();
        HideHealth();   
        HidePrompts();
    }
}
