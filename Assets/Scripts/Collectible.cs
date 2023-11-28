using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private Collectibles _collectibles;
    private AudioClip collectibleSound;
    public event Action<GameObject> ObjectCollectedEvent;
    public string description;

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
            CameraMain.PlaySFX(collectibleSound);
            _collectibles.CollectNew();
            Destroy(gameObject);
        }
    }

}
