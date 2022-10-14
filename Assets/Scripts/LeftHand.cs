using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class LeftHand : MonoBehaviour
{
    public InputActionReference leftTrigger = null;

    public GameObject rightHand;

    public GameObject leftHand;

    public bool thisHandActive = false;

    public Transform prefabTransform;

    private GameObject _rightPutter;
    private GameObject _leftPutter;
    private ClubHit _rightHit;
    private ClubHit _leftHit;

    public GameObject Ball;
    public GameObject Player;
    public GameObject nextLevel;

    private void Awake()
    {
        leftTrigger.action.started += Toggle;

    }

    private void OnDestroy()
    {
        leftTrigger.action.started -= Toggle;
    }

    private void Toggle(InputAction.CallbackContext context)
    {
        thisHandActive = true;
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
        _rightPutter.SetActive(!thisHandActive);
        _leftPutter.SetActive(thisHandActive);
        if (_leftHit.hit)
        {
            _rightHit.hit = true;
        }
        if (_rightHit.hit)
        {
            _leftHit.hit = true;
        }
        RightHand right = gameObject.GetComponent<RightHand>();
        right.thisHandActive = false;
        Vector3 forward = Player.transform.forward;
        if (nextLevel.activeInHierarchy)
        {
            Vector3 newPosition = nextLevel.transform.position - (0.75f * forward);
            newPosition.y = Player.transform.position.y;
            Player.transform.position = newPosition;
        }
        else
        {
            Player.transform.position = Ball.transform.position - (0.5f * forward);
        }
    }
}
