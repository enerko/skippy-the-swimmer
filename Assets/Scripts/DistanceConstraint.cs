using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceConstraint : MonoBehaviour
{
    public Transform constrained;
    public Transform source;
    public float tolerance;

    private float _distance;

    // Start is called before the first frame update
    void Start()
    {
        _distance = (constrained.position - source.position).magnitude;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float currDistance = (constrained.position - source.position).magnitude;
        if (currDistance <= _distance + tolerance) return;  // do nothing

        // adjust position
        Vector3 currOffset = constrained.position - source.position;
        constrained.position = source.position + currOffset.normalized * (_distance + tolerance);
    }
}
