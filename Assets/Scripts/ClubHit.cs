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
    private int _arraysize = 5;
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
    public float hitTime;
    public int strokeCount;
    void Start()
    {
        strokeCount = 0;
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
            if (_ballRigidbody.velocity.magnitude < 0.3 && hitTime > 1.0f)
            {
                _ballRigidbody.velocity = Vector3.zero;
                _ballRigidbody.angularVelocity = Vector3.zero;
                _hit = false;
                if (ballRef)
                {
                    _ballMeshRender.material = defaultMaterial;
                }
                _rigidBody.detectCollisions = true;
            }
        }
    }


    private void FixedUpdate()
    {
        Vector3 currentPosition = clubHead.transform.position; 
        Debug.DrawLine(_oldPosition, currentPosition, Color.red, 1000);
        _directionArrays[_currentDirIndex] = currentPosition - _oldPosition;
        _currentDirIndex = (_currentDirIndex + 1) % _arraysize;
        _oldPosition = currentPosition;
        if (_hit)
        {
            hitTime += Time.fixedDeltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("ball") && !_hit)
        {
            strokeCount++;
            _rigidBody.detectCollisions = false;
            _hit = true;
            hitTime = 0;
            _ballMeshRender.material = hitMaterial;
            // take the average of our direction array
            Vector3 arraysum = Vector3.zero;
            for (var i = 0; i < _directionArrays.Length; i++)
            {
                arraysum += _directionArrays[i];
            }

            //arraysum.y = 0;
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