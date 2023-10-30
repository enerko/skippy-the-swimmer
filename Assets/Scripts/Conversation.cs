using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using TMPro;

public class Conversation : MonoBehaviour
{
    [Serializable]
    public struct Dialogue {
        public string dialogue;
        public GameObject speaker;
    }

    public Dialogue[] dialogueChain;
    public int index = 0;  // index into dialogueChain

    // If set, this convo's prompt will switch to nextConversation
    public Conversation nextConversation;

    private float _typeDelay = 0.1f;
    private string _currentDialogue = string.Empty;
    private IEnumerator _typing;
    private GameObject _dialogueBox;
    private TextMeshProUGUI _dialogueSection;
    private TextMeshProUGUI _speakerSection;
    private ConversationPrompt _conversationPrompt;  // what prompt triggered this conversation?

    void Start() {
        _dialogueBox = GameObject.Find("/Game UI/Dialogue Box");

        _dialogueSection = _dialogueBox.transform.Find("Dialogue").GetComponent<TextMeshProUGUI>();
        _speakerSection = _dialogueBox.transform.Find("Speaker Box/Speaker").GetComponent<TextMeshProUGUI>();
    }

    void Update() {
        if (!Player.s_ConversationActive) {
            // multiple conversations will be setting this this false, but only if there's no convo active
            _dialogueBox.SetActive(false);
        }
    }

    // Advance the convo and return if there is more
    public void Advance() {
        if (Globals.s_GameIsPaused) return;  // dont advance dialogue when paused

        // player must press advance in order to end a conversation
        if (index == dialogueChain.Length) {
            // end the convo
            index = 0;
            Player.s_ConversationActive = false;  // oh god this definitely violates some sort of architecture rules :skull:
            Player.s_CurrentConversation = null;
            _dialogueBox.SetActive(false);

            if (nextConversation) {
                _conversationPrompt.SwitchConversation(nextConversation);
            }

            // Signal to the prompt that it's finished
            _conversationPrompt.SignalConversationFinished();

            return;
        }

        _dialogueBox.SetActive(true);
        Dialogue dialogue = dialogueChain[index];
        _speakerSection.text = dialogue.speaker.name;

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
            _dialogueSection.text = _currentDialogue;

            index++;
            _currentDialogue = string.Empty;
        }
    }

    // Set which prompt triggers this convo
    public void SetPrompt(ConversationPrompt prompt) {
        _conversationPrompt = prompt;
    }

    // Typewriter coroutine
    private IEnumerator Typewrite(Dialogue dialogue) {
        AudioClip[] clips = dialogue.speaker.GetComponent<DialogueAudio>().clips;

        // Typewrite the dialogue
        foreach (char c in dialogue.dialogue.ToCharArray()) {
            if (Globals.s_GameIsPaused) {
                yield return new WaitUntil(() => !Globals.s_GameIsPaused);
            }

            _currentDialogue += c;
            _dialogueSection.text = _currentDialogue;
            AudioSource.PlayClipAtPoint(clips[Random.Range(0, clips.Length)], dialogue.speaker.transform.position);
            yield return new WaitForSeconds(_typeDelay);
        }

        // Typewriter effect has been completed, reset for the next advancement
        // (i.e. there is nothing to skip, next advancement is when player presses button)
        index++;
        _currentDialogue = string.Empty;
    }
}
