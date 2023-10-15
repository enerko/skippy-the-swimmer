using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public Button resume;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown("joystick button 7"))
        {
            if (Globals.GameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    public void Resume ()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Globals.GameIsPaused = false;

        // reset resume button pressed state
        EventSystem.current.SetSelectedGameObject(null);  
    }

    void Pause ()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        resume.Select();
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        Globals.GameIsPaused = true;
    }
}
