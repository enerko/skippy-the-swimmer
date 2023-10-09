using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueShower : MonoBehaviour
{
    public string dialogue;
    public float cooldown;  // set to <= 0 to make it not reenable

    private bool _active = true;
    
    void OnTriggerEnter(Collider other) {
        if (!_active) return;
        if (other.gameObject.tag != "Player") return;

        _active = false;  // once activated, set it to not active
        Debug.Log(dialogue);

        if (cooldown > 0)
            Invoke("Reactivate", cooldown);
    }

    private void Reactivate() {
        _active = true;
    }
}
