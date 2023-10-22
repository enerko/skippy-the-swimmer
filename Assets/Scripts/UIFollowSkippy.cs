using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollowSkippy : MonoBehaviour
{
    private Transform player;
    private Camera cam;
    private RectTransform _rect;

    void Start()
    {
        cam = Camera.main;
        player = GameObject.FindWithTag("Player").transform;
        _rect = GetComponent<RectTransform>();
    }

    void Update()
    {
        Vector3 pos = CameraMain.CustomWorldToScreenPoint(player.position);
        _rect.anchoredPosition = pos;
    }
}
