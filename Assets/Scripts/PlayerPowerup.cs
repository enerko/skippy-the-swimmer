using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Powerup powerup = other.GetComponent<Powerup>();
        if (powerup != null)
        {
            powerup.Activate();
        }
    }
}
