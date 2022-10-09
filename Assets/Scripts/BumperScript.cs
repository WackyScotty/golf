using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperScript : MonoBehaviour
{
    public Rigidbody ballRigidBody;

    public AudioClip bounceSound;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ball"))
        {
            ballRigidBody.AddForce(10 * collision.contacts[0].normal, ForceMode.Impulse);
            Debug.DrawRay(collision.contacts[0].point, collision.contacts[0].normal);
            audioSource.PlayOneShot(bounceSound);
        }
    }
}
