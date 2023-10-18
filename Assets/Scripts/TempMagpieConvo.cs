using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TempMagpieConvo : MonoBehaviour
{
    public Conversation convo;
    public Conversation completeConvo;
    public bool forceConvo = false;  // force the player into a convo when they get in range

    private GameObject _promptGui;  // in the future, might change this to a panel or something
    [SerializeField] private Collectibles _collectibles;
    [SerializeField] private GameObject blockade;
    private int goalNum = 2;

    void Start()
    {
        _promptGui = GameObject.Find("/Game UI/Prompt");
    }

    // Update is called once per frame
    void Update()
    {
        // disable it when the player talks to an npc
        if (Player.s_ConversationActive)
            _promptGui.SetActive(false);
        else if (Player.s_CurrentConversation == convo|| Player.s_CurrentConversation == completeConvo)
        {  // if this prompt has control over the gui
            _promptGui.SetActive(true);

            // move it with the camera
            _promptGui.GetComponent<RectTransform>().anchoredPosition = Camera.main.WorldToScreenPoint(transform.position);
        }
        else if (Player.s_CurrentConversation is null)
        {  // disable it when player is out of any prompt's range
            _promptGui.SetActive(false);
        }
    }

    // When player enters range (collider should be set to ignore everything else, so you can assume other is the player)
    void OnTriggerEnter(Collider other)
    {
        
        if (_collectibles.numCollected >= goalNum)
        {
            Player.s_CurrentConversation = completeConvo;
            blockade.SetActive(false);
        }
        else
        {
            Player.s_CurrentConversation = convo;
        }

        // force without prompting
        if (forceConvo)
        {
            Player.s_ConversationActive = true;
            convo.Advance();
            forceConvo = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // don't disable other conversations, if they exist
        if (Player.s_CurrentConversation == convo || Player.s_CurrentConversation == completeConvo)
        {
            Player.s_CurrentConversation = null;
        }
    }
}
