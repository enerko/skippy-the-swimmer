using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConversationPrompt : MonoBehaviour
{
    public Conversation conversation;
    public GameObject promptGui;  // in the future, might change this to a panel or something
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // disable it when the player talks to an npc
        if (Player.s_ConversationActive)
            promptGui.SetActive(false);
        else if (Player.s_CurrentConversation == conversation) {  // if this prompt has control over the gui
            promptGui.SetActive(true);

            // move it with the camera
            promptGui.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(transform.position);
        } else if (Player.s_CurrentConversation is null) {  // disable it when player is out of any prompt's range
            promptGui.SetActive(false);
        }
    }

    // When player enters range (collider should be set to ignore everything else, so you can assume other is the player)
    void OnTriggerEnter(Collider other) {
        Player.s_CurrentConversation = conversation;
    }

    void OnTriggerExit(Collider other) {
        // don't disable other conversations, if they exist
        if (Player.s_CurrentConversation == conversation) {
            Player.s_CurrentConversation = null;
        }
    }
}
