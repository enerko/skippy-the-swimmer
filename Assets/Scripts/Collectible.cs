using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private Collectibles _collectibles;
    private AudioClip collectibleSound;


    void Start() {
        _collectibles = FindObjectOfType<Collectibles>();
        collectibleSound = Resources.Load<AudioClip>("sfx_collectible");
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector3 pos = gameObject.transform.position;
            AudioSource.PlayClipAtPoint(collectibleSound, pos);
            Destroy(gameObject);
            _collectibles.CollectNew();
        }
    }

}
