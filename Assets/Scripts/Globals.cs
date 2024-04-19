using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Some global stuff managing game state and static vars i guess
public class Globals
{
    public static bool s_GameIsPaused = false;
    public static bool s_CanPause = true;  // sometimes dont allow pausing, e.g. fade out at end of tutorial
    public static bool s_Restarted = false;
    public static bool s_Speedrun = false;

    // Load the given scene and reset the appropriate static vars
    public static void LoadScene(string sceneName, bool hideCursor, bool resetTimer, bool resetCutscene) {
        // Hide cursor
        Cursor.lockState = hideCursor ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !hideCursor;

        if (resetTimer)
            Timer.ResetTimer();

        // Reset the appropriate static vars
        PlayerHealth.s_Health = PlayerHealth.s_MaxHealth;
        PlayerPowerup.DoubleJumpEnabled = false;
        Player.s_ConversationActive = false;
        Player.s_CurrentConversation = null;
        Player.s_Invul = false;
        Player.s_IsAttacking = false;
        Player.s_CanMove = true;
        Player.s_InWater = false;
        Player.s_HorizInput = Vector3.zero;
        
        CameraMain.s_CameraOverride = null;
        CameraMain.s_OverrideTransitioning = false;
        CameraMain.s_CutScenePlayed = s_Restarted;  // is overwritten

        if (s_Speedrun)
        {
            CameraMain.s_CutScenePlayed = true;
            s_Restarted = true;
        }

        // start new game; reset cutscene flags
        if (resetCutscene) {
            CameraMain.s_CutScenePlayed = false;
            CameraMain.s_CutSceneActive = false;
            s_Restarted = false;
        }



        Time.timeScale = 1f;
        s_GameIsPaused = false;
        s_CanPause = true;

        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
