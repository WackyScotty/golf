using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RightHand : MonoBehaviour
{
    public InputActionReference rightTrigger = null;

    public GameObject rightHand;

    public GameObject leftHand;

    public bool thisHandActive = true;

    public Transform prefabTransform;

    private GameObject _rightPutter;
    private GameObject _leftPutter;
    private ClubHit _rightHit;
    private ClubHit _leftHit;

    public GameObject Ball;
    public GameObject Player;

    private void Awake()
    {
        rightTrigger.action.started += Toggle;

    }

    private void OnDestroy()
    {
        rightTrigger.action.started -= Toggle;
    }

    private void Toggle(InputAction.CallbackContext context) { thisHandActive = true;
        Debug.Log("Switching Hands");
        // Get the XR controller of our two hands
        // Set the prefab of the active hand to None
        // Set the prefab of the inactive hand to putter 2
        // switch the active hand
        if (!_rightPutter && !_leftPutter)
        {
            Debug.Log("Got controllers");
            // Get the putter object (child of child of left and right hand
            _rightPutter = rightHand.transform.GetChild(0).GetChild(0).gameObject;
            _rightHit = _rightPutter.GetComponent<ClubHit>();
            _leftPutter = leftHand.transform.GetChild(0).GetChild(0).gameObject;
            _leftHit = _leftPutter.GetComponent<ClubHit>();
        }
        _rightPutter.SetActive(thisHandActive);
        if (_leftHit.hit)
        {
            _rightHit.hit = true;
        }
        if (_rightHit.hit)
        {
            _leftHit.hit = true;
        }
        _leftPutter.SetActive(!thisHandActive);
        LeftHand left = gameObject.GetComponent<LeftHand>();
        left.thisHandActive = false;
        Vector3 forward = Player.transform.forward;
        Player.transform.position = Ball.transform.position - (0.5f * forward);
    }
}
