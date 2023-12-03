using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class Ending : MonoBehaviour
{
    private PlayableDirector director;
    public GameUIManager gameUI;

    // Start is called before the first frame update
    void Start()
    {
        director = GetComponent<PlayableDirector>();
    }

    private void OnTriggerEnter(Collider other)
    {
        director.Play();
        gameUI.HideAll();
        CameraMain.s_CutSceneActive = true;
    }
}
