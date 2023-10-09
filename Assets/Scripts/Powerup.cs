using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : Interactable
{
    public enum PowerupType
    {
        DoubleJump,
        SpeedBoost,
    }
    
    [SerializeField]
    private PowerupType typeOfPowerup;

    public PowerupType Type => typeOfPowerup;
    
    public override void Activate()
    {
        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            switch (typeOfPowerup)
            {
                case PowerupType.DoubleJump:
                    player.EnableDoubleJump();
                    break;
                case PowerupType.SpeedBoost:
                    player.EnableSpeedBoost();
                    break;
                default:
                    Debug.LogWarning($"Unhandled power-up type: {typeOfPowerup}");
                    break;
            }
        }
        
    }
}
