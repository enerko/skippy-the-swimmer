using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        
        if (other.gameObject.tag == "Player") {
            if (Globals.s_Speedrun)
            {
                Globals.LoadScene("Win", false, false, false);
            }
            else
            {
                Globals.LoadScene("Ending", true, false, false);
                Timer.s_Enabled = false;
            }
        }
    }
}
