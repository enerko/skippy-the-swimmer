using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private Collectibles _collectibles;
    private AudioClip collectibleSound;
    public event Action<GameObject> ObjectCollectedEvent;

    void Start() {
        _collectibles = FindObjectOfType<Collectibles>();
        collectibleSound = Resources.Load<AudioClip>("sfx_collectible");
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ObjectCollectedEvent?.Invoke(gameObject);
            Vector3 pos = gameObject.transform.position;
            AudioSource.PlayClipAtPoint(collectibleSound, pos);
            Destroy(gameObject);
            _collectibles.CollectNew();
        }
    }

}
