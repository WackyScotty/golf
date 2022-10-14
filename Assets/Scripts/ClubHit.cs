using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
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
    public MeshRenderer clubMeshRenderer;
    private Rigidbody _ballRigidbody;
    private Rigidbody _rigidBody;
    public XRBaseController Controller;
    public float hitTime;
    public int strokeCount;
    public AudioSource sound;
    public AudioClip hitSound;
    public bool right;
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

        Controller = transform.parent.parent.GetComponent<ActionBasedController>();
        _currentDirIndex = 0;
        
        ballRef = GameObject.FindWithTag("ball");
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
                    clubMeshRenderer.material = defaultMaterial;
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
            clubMeshRenderer.material = hitMaterial;
            // take the average of our direction array
            Vector3 arraysum = Vector3.zero;
            for (var i = 0; i < _directionArrays.Length; i++)
            {
                arraysum += _directionArrays[i];
            }

            arraysum.y += (arraysum.magnitude * 0.3f);
            _ballRigidbody.AddForce(5 * arraysum, ForceMode.Impulse);
            SendHaptics();
            sound.PlayOneShot(hitSound);
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