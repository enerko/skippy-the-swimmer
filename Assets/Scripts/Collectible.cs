using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private Collectibles _collectibles;
    private Checklist _checklist;

    public string description;

    void Start() {
        _collectibles = FindObjectOfType<Collectibles>();
        _checklist = FindObjectOfType<Checklist>();
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            _collectibles.CollectNew();
            _checklist.UpdateChecklist(description);
        }
    }

}
