using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    private Button _btn;
    
    // Start is called before the first frame update
    void Start()
    {
        _btn = GetComponent<Button>();
        _btn.onClick.AddListener(StartMain);
    }

    private void StartMain() {
        // reset health
        PlayerHealth.s_Health = PlayerHealth.s_MaxHealth;

        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }
}
