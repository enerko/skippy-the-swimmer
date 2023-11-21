using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject settingsMenu;
    public GameObject mainPauseMenu;
    public Button resume;

    // Update is called once per frame
    void Update()
    {
        if (Globals.s_CanPause && (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown("joystick button 7")))
        {
            if (Globals.s_GameIsPaused)
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

        settingsMenu.SetActive(false);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        Globals.s_GameIsPaused = false;

        // reset resume button pressed state
        EventSystem.current.SetSelectedGameObject(null);  
    }

    void Pause ()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        resume.Select();
        pauseMenuUI.SetActive(true);
        mainPauseMenu.SetActive(true);
        Time.timeScale = 0f;
        Globals.s_GameIsPaused = true;
    }
}
