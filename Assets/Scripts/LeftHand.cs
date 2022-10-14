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

    public GameObject Ball;
    public GameObject Player;

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
            _leftPutter = leftHand.transform.GetChild(0).GetChild(0).gameObject;
        }
        _rightPutter.SetActive(!thisHandActive);
        _leftPutter.SetActive(thisHandActive);
        RightHand right = gameObject.GetComponent<RightHand>();
        right.thisHandActive = false;
        Vector3 forward = Player.transform.forward;
        Player.transform.position = Ball.transform.position - (0.5f * forward);
    }
}
