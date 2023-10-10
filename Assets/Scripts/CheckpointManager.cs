using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    
    public static CheckpointManager Instance;
    
    private Vector3 lastCheckpointPosition = new Vector3(-13.96f, 1.75f, -3.806f);

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetCheckpoint(Vector3 newCheckpoint)
    {
        lastCheckpointPosition = newCheckpoint;
    }

    public void RespawnAtLastCheckpoint()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = lastCheckpointPosition;
            // reset the player's health, velocity, powerups?
        }
    }
}
