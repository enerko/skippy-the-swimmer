using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveTrigger : MonoBehaviour
{
    public string newObjective;

    public Objectives _objectives;
    private bool _activated;


    // May assume that other is the player if include/exclude options are set properly
    void OnTriggerEnter(Collider other) 
    {
        if (_activated) return;

        _objectives.UpdateObjective(newObjective);

        _activated = true;

    }
}
