using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            LevelManager.prevLevel = SceneManager.GetActiveScene().name;
            Globals.LoadScene("Win");
        }
    }
}
