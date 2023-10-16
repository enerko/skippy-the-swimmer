using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private Collectibles _collectibles;

    void Start() {
            _collectibles = FindObjectOfType<Collectibles>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            _collectibles.CollectNew();
        }
    }

}
