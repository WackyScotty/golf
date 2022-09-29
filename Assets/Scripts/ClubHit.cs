using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClubHit : MonoBehaviour
{
    // Start is called before the first frame update
    // Idea, we want to be averaging the golf hits over a set amount of
    // For now the last half of a second
    // fixedUpdate updates every 0.02 seconds
    public GameObject clubHead;
    private Vector3[] _directionArrays;
    private int _currentDirIndex;
    
    private Vector3 _hitDirection;
    private Vector3 _oldPosition;
    private Vector3 _arraySum; // declared here to avoid the cost of declaration at collision
    void Start() 
    {
        _directionArrays = new Vector3[50];
        for(var i = 0; i < _directionArrays.Length; i++)
        {
            _directionArrays[i] = Vector3.zero;
        }

        _currentDirIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        Vector3 currentPosition = clubHead.transform.position;
        Debug.DrawLine(_oldPosition, currentPosition, Color.red, 1000);
        Debug.Log("old Position");
        Debug.Log(_oldPosition);
        Debug.Log("New Position");
        Debug.Log(currentPosition);
        _directionArrays[_currentDirIndex] = currentPosition - _oldPosition;
        _currentDirIndex = (_currentDirIndex + 1) % 50;
        _oldPosition = currentPosition;

    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("ball"))
        {
            Debug.Log("Ball Hit!!");
            // take the average of our direction array
            Vector3 _arraysum = Vector3.zero;
            for (var i = 0; i < _directionArrays.Length; i++)
            {
                _arraysum += _directionArrays[i];
            }

            _arraysum /= _directionArrays.Length;
            collision.gameObject.GetComponent<Rigidbody>().AddForce(5 * _arraysum, ForceMode.Impulse);
            Debug.DrawRay(collision.gameObject.transform.position, 10 * _arraysum, Color.blue, 10000);
        }
    }
}
