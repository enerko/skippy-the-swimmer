using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Some global stuff managing game state and static vars i guess
public class Globals
{
    public static bool GameIsPaused = false;

    // Load the given scene and reset the appropriate static vars
    public static void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);

        // Hide cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Reset the appropriate static vars
        PlayerHealth.s_Health = PlayerHealth.s_MaxHealth;
        PlayerPowerup.s_DoubleJumpEnabled = false;
        PlayerPowerup.s_SpeedBoostEnabled = false;
        Player.s_ConversationActive = false;
        Player.s_CurrentConversation = null;
        Player.s_Invul = false;
        Player.s_IsAttacking = false;

        Time.timeScale = 1f;
        GameIsPaused = false;
    }
}