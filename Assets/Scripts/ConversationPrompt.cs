using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ConversationPrompt : MonoBehaviour
{
    public Conversation conversation;
    public bool force = false;  // force the player into a convo when they get in range

    private GameObject _promptGui;  // in the future, might change this to a panel or something

    void Start() {
        _promptGui = GameObject.Find("/Game UI/Prompt");
        conversation.SetPrompt(this);
    }

    // Update is called once per frame
    void Update()
    {
        // disable it when the player talks to an npc
        if (Player.s_ConversationActive)
            _promptGui.SetActive(false);
        else if (Player.s_CurrentConversation == conversation && !force) {  // if this prompt has control over the gui
            _promptGui.SetActive(true);

            // move it with the camera
            _promptGui.GetComponent<RectTransform>().anchoredPosition = Camera.main.WorldToScreenPoint(transform.position);
        } else if (Player.s_CurrentConversation is null) {  // disable it when player is out of any prompt's range
            _promptGui.SetActive(false);
        }
    }

    // When player enters range (collider should be set to ignore everything else, so you can assume other is the player)
    void OnTriggerStay(Collider other) {
        Player.s_CurrentConversation = conversation;

        // force without prompting
        if (force && Player.s_Grounded) {
            Player.s_ConversationActive = true;
            conversation.Advance();
            force = false;
        }
    }

    void OnTriggerExit(Collider other) {
        // don't disable other conversations, if they exist
        if (Player.s_CurrentConversation == conversation) {
            Player.s_CurrentConversation = null;
        }
    }

    public void SwitchConversation(Conversation other) {
        conversation = other;
    }
}
