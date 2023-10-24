using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainDistanceConstraint : MonoBehaviour
{
    public Transform[] chain;  // source (root) is first, tip is last
    public float tolerance;

    private float[] _distances;  // the distances to constrain each transform (except the root) to

    // Start is called before the first frame update
    void Start()
    {
        _distances = new float[chain.Length - 1];

        // record initial distances
        for (int i = 1; i < chain.Length; i++) {
            Transform constrained = chain[i];
            Transform source = chain[i - 1];
            _distances[i - 1] = (constrained.position - source.position).magnitude;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // loop thru the chain from root to tip to enforce distance constraint
        // the way it was before could have been susceptible to race conditions, depending on which bone's LateUpdate ran first
        for (int i = 1; i < chain.Length; i++) {
            Transform constrained = chain[i];
            Transform source = chain[i - 1];

            float currDistance = (constrained.position - source.position).magnitude;
            if (currDistance <= _distances[i - 1] + tolerance) return;  // do nothing

            // adjust position
            Vector3 currOffset = constrained.position - source.position;
            constrained.position = source.position + currOffset.normalized * (_distances[i - 1] + tolerance);
        }
    }
}
