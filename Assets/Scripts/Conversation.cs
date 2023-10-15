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
    public int index = 0;  // index into dialogueChain

    private float _typeDelay = 0.2f;
    private string _currentDialogue = string.Empty;
    private IEnumerator _typing;

    // Advance the convo and return if there is more
    public void Advance() {
        if (PauseMenu.GameIsPaused) return;  // dont advance dialogue when paused

        // player must press advance in order to end a conversation
        if (index == dialogueChain.Length) {
            index = 0;
            Player.s_ConversationActive = false;  // oh god this definitely violates some sort of architecture rules :skull:
            Debug.Log("DONE");
            return;
        }

        Dialogue dialogue = dialogueChain[index];

        // If starting new dialogue...
        if (_currentDialogue == string.Empty) {
            // Typewrite the dialogue
            _typing = Typewrite(dialogue);
            StartCoroutine(_typing);
        } else {
            // Player is skipping dialogue or type writer effect has ended
            StopCoroutine(_typing);
            _typing = null;

            _currentDialogue = dialogue.dialogue;
            Debug.Log(dialogue.speaker.name + ": " + _currentDialogue);

            index++;
            _currentDialogue = string.Empty;
        }
    }

    // Typewriter coroutine
    private IEnumerator Typewrite(Dialogue dialogue) {
        // Typewrite the dialogue
        foreach (char c in dialogue.dialogue.ToCharArray()) {
            if (PauseMenu.GameIsPaused) {
                yield return new WaitUntil(() => !PauseMenu.GameIsPaused);
            }

            _currentDialogue += c;
            Debug.Log(dialogue.speaker.name + ": " + _currentDialogue);
            yield return new WaitForSeconds(_typeDelay);
        }

        // Typewriter effect has been completed, reset for the next advancement
        // (i.e. there is nothing to skip, next advancement is when player presses button)
        index++;
        _currentDialogue = string.Empty;
    }
}
