using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanRotate : MonoBehaviour
{
    private const float AngularSpeed = 45;  // angles per second

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, AngularSpeed * Time.deltaTime);
    }
}
