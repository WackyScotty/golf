using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallWater : MonoBehaviour
{
    public Vector3 oldPositionFromhit;

    public Rigidbody myRigidBody; 
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (myRigidBody.velocity.magnitude == 0 && myRigidBody.angularVelocity.magnitude == 0)
        {
            oldPositionFromhit = gameObject.transform.position;
            // TODO maybe elevate this slightly above the ground
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("lake"))
        {
            this.gameObject.transform.position = oldPositionFromhit;
            myRigidBody.velocity = Vector3.zero;
            myRigidBody.angularVelocity = Vector3.zero;

        } 
    }
}
