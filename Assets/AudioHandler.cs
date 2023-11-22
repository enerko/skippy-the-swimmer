using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class AudioHandler : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip wakeupMusic;
    public AudioClip level1Music;
    public Player player;

    public void Start()
    {
        PlayableDirector director = player.GetComponent<PlayableDirector>();
        if (director != null && !Globals.s_Restarted)
        {
            StartCoroutine(PlayWakeupMusic());
        }
        else
        {
            audioSource.clip = level1Music;
            audioSource.Play();
        }
    }

    public IEnumerator PlayWakeupMusic()
    {
        audioSource.PlayOneShot(wakeupMusic);
        yield return new WaitForSeconds(wakeupMusic.length - 8);
        audioSource.clip = level1Music;
        audioSource.Play();
    }
}

