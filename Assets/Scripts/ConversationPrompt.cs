using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationPrompt : MonoBehaviour, ITalkable
{
    private bool _enabled = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // When player enters range (collider should be set to ignore everything else)
    void OnTriggerEnter(Collider other) {

        if(other.CompareTag("Player") && other.TryGetComponent(out Player player))
        {
            _enabled = true;

            player.Talkable = this;
        }
    }

    void OnTriggerExit(Collider other) {
        _enabled = false;

        if (other.CompareTag("Player") && other.TryGetComponent(out Player player))
        {
            if(player.Talkable is ConversationPrompt conversationPrompt && conversationPrompt == this) {
                _enabled = false;

                player.Talkable = null;
            }
           
        }
    }

    public void Talk(Player player)
    {
        Debug.Log("Talk to me baby!");
    }
}
