using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveTrigger : MonoBehaviour
{
    public string newObjective;

    private Objectives _objectives;
    private bool _activated;

    void Start()
    {
        _objectives = GameObject.Find("/Game UI/Objective").GetComponent<Objectives>();   
    }

    // May assume that other is the player if include/exclude options are set properly
    void OnTriggerEnter(Collider other) 
    {
        if (_activated) return;

        _objectives.UpdateObjective(newObjective);

        _activated = true;

    }
}
