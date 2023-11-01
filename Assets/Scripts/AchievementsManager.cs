using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class AchievementsManager : MonoBehaviour
{
    public PopUpManager popupManager;
    private int objectsBrokenCount = 0;


    // All the possible achievements
    private Achievement breakOne = new Achievement("breakOne", "Break an object");
    private Achievement breakJar = new Achievement("breakJar", "Find a cookie");
    private Achievement unlockPowerup = new Achievement("unlockPowerup", "Unlock a powerup");
    private Achievement playInstrument = new Achievement("playInstrument", "Play an instrument");
    private Achievement painter = new Achievement("painter", "Become a painter");
    private Achievement pearlOne = new Achievement("pearlOne", "Collect a pearl");
    private Achievement pearlAll = new Achievement("pearlAll", "Collect all the pearls");

    private List<Achievement> achievements;

    public GameObject achievementList;
    private TextMeshProUGUI _text;
    private Image _background;
    private int objectCollectedCount;

    private int totalPearls;


    // Start is called before the first frame update
    void Start()
    {
        // Subscribe to the ObjectBrokenEvent
        foreach (var breakable in FindObjectsOfType<Breakable>())
        {
            breakable.ObjectBrokenEvent += HandleObjectBroken;
        }

        foreach(var collectible in FindObjectsOfType<Collectible>())
        {
            collectible.ObjectCollectedEvent += HandleObjectCollected;
            totalPearls += 1;
        }
        
        achievements = new List<Achievement>() { pearlOne, pearlAll, breakOne, breakJar }; //, unlockPowerup, playInstrument, painter };
        _text = achievementList.GetComponent<TextMeshProUGUI>();
        _background = gameObject.GetComponentInChildren<Image>();
        UpdateText();

        HideList();

    }

    public void ShowList()
    {
        achievementList.SetActive(true);
        _background.enabled = true;
        Player.s_AchievementsOpen = true;
    }

    public void HideList()
    {
        achievementList.SetActive(false);
        _background.enabled = false;
        Player.s_AchievementsOpen = false;
    }

    private void UpdateText()
    {
        string text = "";
        int completed = 0;
        int total = 0;
        foreach (var achievement in achievements)
        {
            text += achievement.achievementLocked ? "[ ]" : "[x]";
            text += " " + achievement.achievementDescription + "<br><br>";
            completed += achievement.achievementLocked ? 0 : 1;
            total += 1;

        }
        string title = $"Achievements ({completed} / {total}  complete): <br><br> ";
        _text.text = title + text;
    }

    private void HandleObjectBroken(GameObject brokenObject)
    {
        // Handle the event (e.g., update achievement progress or perform some action)

        // Update the counter
        objectsBrokenCount++;

        if (objectsBrokenCount == 1)
        {
            UnlockAchievement(breakOne);
        }

        if (brokenObject.name == "Cookie_Jar")
        {
            UnlockAchievement(breakJar);
        }
    }

    private void HandleObjectCollected(GameObject collectedObject)
    {
        objectCollectedCount++;

        if(objectCollectedCount == 1)
        {
            UnlockAchievement(pearlOne);
        }

        if (objectCollectedCount == totalPearls)
        {
            UnlockAchievement(pearlAll);
        }
    }

    private void UnlockAchievement(Achievement achievement)
    {
        popupManager.ShowPopup(achievement.achievementDescription);
        achievement.achievementLocked = false;
        UpdateText();
    }

    public void DisplayAchievements(InputValue inputValue)
    {
        if (inputValue.isPressed)
        {
            ShowList();
        }
        else
        {
            HideList();
        }
    }
}
