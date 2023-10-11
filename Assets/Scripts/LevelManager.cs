using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    private Button _btn;
    public bool loadPreviousLevel;
    public bool loadNextLevel;
    public static string prevLevel;
    private List<string> levels = new List<string>
    {
        "Main","Tutorial", "Level 1"
    };

    // Start is called before the first frame update
    void Start()
    {
        _btn = GetComponent<Button>();
        _btn.onClick.AddListener(LoadLevel);
    }

    // Update is called once per frame
    void LoadLevel()
    {
        PlayerHealth.s_Health = PlayerHealth.s_MaxHealth;
        if (loadPreviousLevel)
        {
            SceneManager.LoadScene(prevLevel);
        }
        else if (loadNextLevel)
        {
            string nextLevel = levels[levels.IndexOf(prevLevel) + 1];
            SceneManager.LoadScene(nextLevel);
        }
        

        
        
    }
}
