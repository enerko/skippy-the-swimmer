using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : Interactable
{   
    [SerializeField]
    private PowerupType typeOfPowerup;
    
    public override void Activate()
    {
        switch (typeOfPowerup) {
            case PowerupType.DoubleJump:
                PlayerPowerup.EnableDoubleJump();
                break;
            default:
                Debug.LogWarning($"Unhandled power-up type: {typeOfPowerup}");
                break;
        } 
    }
}
