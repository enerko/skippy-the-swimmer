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

    private bool _collected = false;

    void Start() {
        _collectibles = FindObjectOfType<Collectibles>();
        collectibleSound = Resources.Load<AudioClip>("sfx_collectible");
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player" && !_collected)
        {
            _collected = true;
            ObjectCollectedEvent?.Invoke(gameObject);
            Vector3 pos = gameObject.transform.position;
            float volume = PlayerPrefs.GetFloat("SFX Volume", 1);
            AudioSource.PlayClipAtPoint(collectibleSound, pos, volume);
            _collectibles.CollectNew();
            Destroy(gameObject);
        }
    }

}
