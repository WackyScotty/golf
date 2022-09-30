using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TeleportOnTrigger : MonoBehaviour
{
    public InputActionReference teleportReference = null;
    public GameObject Ball;
    public GameObject Player;
    private void Awake()
    {
        teleportReference.action.started += Toggle;
    }

    private void OnDestroy()
    {
        teleportReference.action.started -= Toggle;
    }

    private void Toggle(InputAction.CallbackContext context)
    {
        Vector3 forward = Player.transform.forward;
        Player.transform.position = Ball.transform.position - (0.5f * forward);
    }
}
