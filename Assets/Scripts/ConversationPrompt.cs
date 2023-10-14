using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationPrompt : MonoBehaviour
{
    public Conversation conversation;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
