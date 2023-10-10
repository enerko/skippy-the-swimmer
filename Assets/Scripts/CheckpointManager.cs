using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    
    public static CheckpointManager Instance;
    
    private static Vector3 lastCheckpointPosition = new Vector3(-13.96f, 1.75f, -3.806f);

    public static void SetCheckpoint(Vector3 newCheckpoint)
    {
        lastCheckpointPosition = newCheckpoint;
    }

    public static void RespawnAtLastCheckpoint()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player)
        {
            player.transform.position = lastCheckpointPosition;
            // reset the player's health, velocity, powerups?
        }
    }
}
