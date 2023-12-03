using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class Ending : MonoBehaviour
{
    private PlayableDirector director;
    private GameObject gameUI;

    // Start is called before the first frame update
    void Start()
    {
        director = GetComponent<PlayableDirector>();
        gameUI = GameObject.Find("Game UI");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        director.Play();
        gameUI.SetActive(false);
        CameraMain.s_CutSceneActive = true;
    }
}
