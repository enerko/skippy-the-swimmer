using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformStand : MonoBehaviour
{
    private GameObject _target;
    private Vector3 _offset;


    void Update() {
        if (!_target) {
            return;
        }

        // manually apply offset
        _target.transform.position = transform.position + transform.TransformDirection(_offset);
    }

    // Assume collider can only collide with the player
    void OnCollisionStay(Collision other)
    {
        _target = other.collider.gameObject;
        _offset = transform.InverseTransformDirection(_target.transform.position - transform.position);
    }

    void OnCollisionExit(Collision other)
    {
        _target = null;
    }
}
