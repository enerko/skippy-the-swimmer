using System.Collections;
using UnityEngine;

public class MagpieSoundHandler : MonoBehaviour
{
    public AudioSource audioSource; // The AudioSource component attached to the magpie GameObject
    public AudioClip magpieSound;   // The magpie sound clip

    // Start is called before the first frame update
    void Start()
    {
        // Start the coroutine to play the magpie sound at random intervals
        StartCoroutine(PlayMagpieSound());
    }

    // Coroutine to handle the timing and playback of the magpie sound
    private IEnumerator PlayMagpieSound()
    {
        while (true) // Loop indefinitely
        {
            // Wait for a random time between 45 and 70 seconds
            yield return new WaitForSeconds(Random.Range(45f, 60f));

            // Check if the AudioSource is active and enabled
            if (audioSource != null && audioSource.gameObject.activeInHierarchy && audioSource.enabled)
            {
                // Play the magpie sound
                audioSource.PlayOneShot(magpieSound);
            }
        }
    }
}