using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.XR.Interaction.Toolkit;

public class ClubHit : MonoBehaviour
{
    // Start is called before the first frame update
    // Idea, we want to be averaging the golf hits over a set amount of
    // For now the last half of a second
    // fixedUpdate updates every 0.02 seconds
    public GameObject clubHead;
    private Vector3[] _directionArrays;
    private int _currentDirIndex;
    private int _arraysize = 20;
    private Vector3 _hitDirection;
    private Vector3 _oldPosition;
    private Vector3 _arraySum; // declared here to avoid the cost of declaration at collision
    private bool _hit; 
    public Material defaultMaterial;
    public Material hitMaterial;
    private GameObject ballRef;
    private MeshRenderer _ballMeshRender;
    private Rigidbody _ballRigidbody;
    private Rigidbody _rigidBody;
    public XRBaseController Controller;
    
    void Start()
    {
        _counter = 0;
        _hit = false;
        _directionArrays = new Vector3[_arraysize];
        _rigidBody = this.gameObject.GetComponent<Rigidbody>();
        for(var i = 0; i < _directionArrays.Length; i++)
        {
            _directionArrays[i] = Vector3.zero;
        }
        _currentDirIndex = 0;

        GameObject[] controllers;
        controllers = GameObject.FindGameObjectsWithTag("controller");
        Debug.Assert(controllers.Length == 1);
        GameObject rightController = controllers[0];
        Controller = rightController.GetComponent<XRBaseController>();
        ballRef = GameObject.FindWithTag("ball");
        _ballMeshRender = ballRef.GetComponent<MeshRenderer>();
        _ballRigidbody = ballRef.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_hit)
        {

            Rigidbody rb = ballRef.GetComponent<Rigidbody>();
            if (rb.velocity.magnitude < 0.2)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                Debug.Log("Can hit again");
                _hit = false;
                _rigidBody.detectCollisions = true;
                if (ballRef)
                {
                    ballRef.GetComponent<MeshRenderer>().material = defaultMaterial;
                }
            }
        }
    }

    private int _counter;

    private void FixedUpdate()
    {
        _counter++;
        if (_counter == 2)
        {
            Vector3 currentPosition = clubHead.transform.position; 
            Debug.DrawLine(_oldPosition, currentPosition, Color.red, 1000);
            _directionArrays[_currentDirIndex] = currentPosition - _oldPosition;
            _currentDirIndex = (_currentDirIndex + 1) % _arraysize;
            _oldPosition = currentPosition;
            _counter = 0;
        }

        
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("ball") && !_hit)
        {
            _hit = true;
            _rigidBody.detectCollisions = false;
            _ballMeshRender.material = hitMaterial;
            // take the average of our direction array
            Vector3 arraysum = Vector3.zero;
            for (var i = 0; i < _directionArrays.Length; i++)
            {
                arraysum += _directionArrays[i];
            }

            arraysum.y = 0;
            _ballRigidbody.AddForce(4 * arraysum, ForceMode.Impulse);
            SendHaptics();
        }
        else if (collision.gameObject.CompareTag("ball"))
        {
            Debug.Log("Too soon!");
        }
    }
    private void SendHaptics()
    {
        const float amplitude = 1;
        const float duration = 0.1f;
        Controller.SendHapticImpulse(amplitude, duration);
    }
    
    
    // If not hit, on trigger button press. teleport the player to the ball
}