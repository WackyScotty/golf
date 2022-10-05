using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperScript : MonoBehaviour
{
    public Rigidbody ballRigidBody;
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
            ballRigidBody.AddForce(20 * collision.contacts[0].normal, ForceMode.Impulse);
            Debug.DrawRay(collision.contacts[0].point, collision.contacts[0].normal);
        }
    }
}
