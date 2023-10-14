using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conversation : MonoBehaviour
{
    [Serializable]
    public struct Dialogue {
        public string dialogue;
        public GameObject speaker;
    }

    public Dialogue[] dialogueChain;

    private int _index = 0;  // index into dialogueChain

    // Advance the convo and return if there is more
    public bool Advance() {
        Dialogue dialogue = dialogueChain[_index];
        Debug.Log(dialogue.speaker.name + ": " + dialogue.dialogue);
        _index++;

        // you've reached the end
        if (_index == dialogueChain.Length) {
            _index = 0;
            
            return false;
            // IF WE USE A FLAG SYSTEM, WE CAN SET ANY REQUIRED FLAGS HERE TO CHANGE THE PROMPTER's CONVERSATION
        }

        return true;
    }
}
