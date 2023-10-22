using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedBounce : MonoBehaviour
{
    public float bounceForce = 5.0f;

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            if (rb.velocity.y < 0)
            {
                rb.velocity = new Vector3(rb.velocity.x, Mathf.Abs(rb.velocity.y) + bounceForce, rb.velocity.z);
            }
        }
    }
}
