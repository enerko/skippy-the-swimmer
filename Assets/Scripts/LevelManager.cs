using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    private Button _btn;
    [SerializeField] private string levelName;
    // Start is called before the first frame update
    void Start()
    {
        _btn = GetComponent<Button>();
        _btn.onClick.AddListener(LoadLevel);
    }

    // Update is called once per frame
    void LoadLevel()
    {
        SceneManager.LoadScene(levelName);
    }
}
