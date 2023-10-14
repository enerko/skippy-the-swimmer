using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationPrompt : MonoBehaviour
{
    private bool _enabled = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // When player enters range (collider should be set to ignore everything else)
    void OnTriggerEnter(Collider player) {
        _enabled = true;
        Debug.Log("Talk to me baby!");
    }

    void OnTriggerExit(Collider player) {
        _enabled = false;
    }
}
