using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class KnockObject : MonoBehaviour
{
    [SerializeField] Vector3 direction;
    private Rigidbody _rb;


    public IEnumerator Knock()
    {
        _rb = gameObject.AddComponent<Rigidbody>();
        _rb.AddForce(direction, ForceMode.Impulse);
        yield return new WaitForSeconds(5);
        Destroy(_rb);

    }
}
