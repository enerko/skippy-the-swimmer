using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class Knockable : Interactable
{
    private int _timelineIndex = 0;  // which timeline to play on hit?

    public Prompt interactPrompt;
    public PlayableDirector director;
    public TimelineAsset[] actionTimelines;  // the object may play multiple timelines before being knocked down

    public override void Activate() {
        Activated = true;  // don't reactivate until timeline is done, or if final action is done

        PlayableAsset nextAction = actionTimelines[_timelineIndex];
        director.playableAsset = nextAction;
        interactPrompt?.SetHidden(true);
        director.Play();  // play the timeline

        Invoke("LoadNextTimeline", (float)nextAction.duration);
    }

    private void LoadNextTimeline() {
        _timelineIndex++;

        if (_timelineIndex == actionTimelines.Length) {
            // the final timeline is finished, this object is done and cannot be interacted with more
            interactPrompt?.Disable();
            return;
        }

        interactPrompt?.SetHidden(false);
        Activated = false;
    }
}
