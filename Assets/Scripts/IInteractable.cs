using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void Activate();
    bool GetActivated();
    bool GetFinishedAction();
}
