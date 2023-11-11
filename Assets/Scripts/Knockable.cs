using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Knockable : Interactable
{
    private Rigidbody _rb;

    public Prompt interactPrompt;
    public PlayableDirector director;

    private void Start() {
        _rb = GetComponent<Rigidbody>();
    }

    public override void Activate() {
        Activated = true;

        interactPrompt?.Disable();
        director.Play();
    }
}
