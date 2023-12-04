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

    public void DisplayPauseMenu()
    {
        if (Globals.s_GameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
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

        // reset resume button pressed state and force deselect the last selected thing
        GameObject lastSelected = EventSystem.current.currentSelectedGameObject;
        if (lastSelected != null) {
            ButtonSelection lastButtonSelection = lastSelected.GetComponent<ButtonSelection>();
            lastButtonSelection?.ForceDeselect();
        }
        EventSystem.current.SetSelectedGameObject(null);  
    }

    void Pause ()
    {
        // enable or hide cursor based on if controller is plugged in
        bool isGamepad = ControllerTypeHandler.currentController == ControllerTypeHandler.ControllerType.Gamepad;
        Cursor.lockState = isGamepad ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !isGamepad;

        pauseMenuUI.SetActive(true);
        mainPauseMenu.SetActive(true);
        resume.Select();
        resume.GetComponent<ButtonSelection>().OnSelect(null);  // WHY TF DOES THIS NOT GET CALLED SOMETIMES?????

        Time.timeScale = 0f;
        Globals.s_GameIsPaused = true;
    }
}
