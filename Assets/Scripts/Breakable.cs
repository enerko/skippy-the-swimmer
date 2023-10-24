using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : Interactable
{
    [SerializeField] private AudioClip _activateSound;
    [SerializeField] private bool _dropsWater;
    [SerializeField] private GameObject _puddle;

    public Prompt interactPrompt;

    public override void Activate() {
        Activated = true;
        FinishedAction = true;

        // play activated sound if there is one
        if (_activateSound) {
            AudioSource.PlayClipAtPoint(_activateSound, transform.position);
        }

        if (_dropsWater) {
            // raycast downwards to see where to spawn the water
            // TODO: should we account for inclined surfaces here?
            Ray ray = new Ray(transform.position, Vector3.down);
            RaycastHit hit;
            int mask = ~LayerMask.GetMask("Interactable") & ~LayerMask.GetMask("Player");
            if (Physics.Raycast(ray, out hit, 5, mask, QueryTriggerInteraction.Ignore)) {
                Instantiate(_puddle, hit.point + new Vector3(0, 0.1f, 0), Quaternion.identity);
            }
        }

        interactPrompt?.Disable();

        Destroy(gameObject);
    }
}
