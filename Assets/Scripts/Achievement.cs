using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievement: MonoBehaviour
{
    public string achievementName;
    public string achievementDescription;
    public bool achievementLocked;

    public Achievement(string name, string description)
    {
        achievementName = name;
        achievementDescription = description;
        achievementLocked = true;
    }

}
