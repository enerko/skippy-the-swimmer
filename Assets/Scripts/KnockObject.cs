using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class KnockObject : MonoBehaviour
{
    [SerializeField] private Vector3 direction;
    private Rigidbody _rb;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip breakSound;
    [SerializeField] private AudioClip fallSound;
    private bool isFalling = false;


    public IEnumerator Knock()
    {
        
        _rb = gameObject.AddComponent<Rigidbody>();
        _rb.AddForce(direction, ForceMode.Impulse);
        audioSource.clip = breakSound;
        audioSource.Play();
        isFalling = true;
        yield return new WaitForSeconds(3);
        audioSource.clip = fallSound;
        audioSource.Play();
        Destroy(_rb);
        

    }

}
