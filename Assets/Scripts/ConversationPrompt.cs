using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ConversationPrompt : Prompt
{
    public Conversation conversation;
    public bool force = false;  // force the player into a convo when they get in range
    public UnityEvent finishedFirstConvo;  // fires when you finish this prompt's first conversation

    private bool _firstConvo = true;

    new void Start() {
        conversation.SetPrompt(this);
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        // disable it when the player talks to an npc
        if (Player.s_ConversationActive) {
            _active = false;
        } else if (!Player.s_CanMove) {
            _active = false;
        } else if (Player.s_CurrentConversation == conversation && !force) {  // if this prompt has control over the gui
            _active = true;
        } else if (Player.s_CurrentConversation is null) {  // disable it when player is out of any prompt's range
            _active = false;
        }

        base.Update();
    }

    // When player enters range (collider should be set to ignore everything else, so you can assume other is the player)
    void OnTriggerStay(Collider other) {
        Player.s_CurrentConversation = conversation;

        // force without prompting
        if (force && Player.s_Grounded) {
            Player.s_ConversationActive = true;
            conversation.Advance();
            conversation.SetPrompt(this);  // just in case
            force = false;
        }
    }

    void OnTriggerExit(Collider other) {
        // don't disable other conversations, if they exist
        // also don't disable while a convo is active, in case you fall out of range while in a convo which is unlikely
        if (Player.s_CurrentConversation == conversation && !Player.s_ConversationActive) {
            Player.s_CurrentConversation = null;
        }
    }

    public void SwitchConversation(Conversation other) {
        conversation = other;
    }
    
    public void SignalConversationFinished() {
        if (_firstConvo) {
            _firstConvo = false;
            finishedFirstConvo.Invoke();
        }
    }
}
