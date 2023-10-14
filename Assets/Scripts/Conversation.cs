using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conversation : MonoBehaviour
{
    public GameObject[] speakers;  // store the game object that contains the meshes of speakers
    public string[] dialogues;  // each dialogue piece, the speaker for dialogues[i] is speakers[i]

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Begin the conversation
    public void Begin() {
        Debug.Assert(dialogues.Length == speakers.Length);
        for (int i = 0; i < dialogues.Length; i++) {
            GameObject speaker = speakers[i];
            string dialogue = dialogues[i];

            Debug.Log(speaker.name + ": " + dialogue);
        }
    }
}
