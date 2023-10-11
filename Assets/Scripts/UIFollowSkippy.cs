using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollowSkippy : MonoBehaviour
{
    private Transform player;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        Vector3 pos = cam.WorldToScreenPoint(player.position);

        if (transform.position != pos)
        {
            transform.position = pos;
        }
    }
}
