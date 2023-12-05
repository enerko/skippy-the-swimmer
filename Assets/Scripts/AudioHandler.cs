using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class AudioHandler : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip wakeupMusic;
    public AudioClip tutorialMusic; // New AudioClip for the tutorial level
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
            // Check if the current level is the tutorial
            if (IsTutorialLevel())
            {
                // Play the tutorial music
                audioSource.clip = tutorialMusic;
                audioSource.Play();
            }
            else
            {
                // Play level 1 music
                audioSource.clip = level1Music;
                audioSource.Play();
            }
        }
    }

    public IEnumerator PlayWakeupMusic()
    {
        audioSource.PlayOneShot(wakeupMusic);
        yield return new WaitForSeconds(wakeupMusic.length - 8);
        audioSource.clip = level1Music;
        audioSource.Play();
    }

    // Method to determine if the current level is the tutorial
    private bool IsTutorialLevel()
    {
        return SceneManager.GetActiveScene().name == "Tutorial";
    }

}