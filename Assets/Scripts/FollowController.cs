using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// For moving the Skippy mesh to follow the controller object
public class FollowController : MonoBehaviour
{
    public Transform controller;
    
    private Vector3 _offset;
    
    // Start is called before the first frame update
    void Start()
    {
        _offset = transform.InverseTransformDirection(transform.position - controller.position);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 goalPosition = controller.position + transform.TransformDirection(_offset);
        transform.position = Vector3.Lerp(transform.position, goalPosition, 10f * Time.deltaTime);

        if (!Player.s_IsAttacking)
            transform.LookAt(controller.position + controller.forward, controller.up);
    }
}
