using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable: MonoBehaviour
{
    public bool Activated { get; protected set; } = false;
    public bool FinishedAction { get; protected set; } = false;
    public abstract void Activate();
}
