using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClubHit : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 _hitDirection;
    private Vector3 _oldPosition;
    void Start()
    {
        _hitDirection = new Vector3(0, 0, 0);
        _oldPosition = this.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPosition = this.gameObject.transform.position;
        _hitDirection = currentPosition - _oldPosition;
        _oldPosition = currentPosition;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Ball Hit!!");
        if (collision.gameObject.CompareTag("ball"))
        {
            // TODO compute this vector
            collision.gameObject.GetComponent<Rigidbody>().AddForce(200 * _hitDirection);
        }
    }
}
