using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.SceneManagement;

public class PhysicsButton : MonoBehaviour
{
    [SerializeField] private float threshold = 0.1f;
    [SerializeField] private float deadZone = 0.025f;

    private bool _isPressed;
    private Vector3 _startPos;
    private ConfigurableJoint _joint;

    public UnityEvent onPressed, onReleased;

    void Start()
    {
        _startPos = transform.localPosition;
        _joint = GetComponent<ConfigurableJoint>();
    }

    void Update()
    {
        if (!_isPressed && GetValue() + threshold >= 1)
        {
            Pressed();
            Invoke(nameof(MoveToScene), 1.0f);
        }           
        if (_isPressed && GetValue() - threshold <= 0)
        {   
            Realeased();
        }      
    }

    private float GetValue()
    {
        var value = Vector3.Distance(_startPos, transform.localPosition) / _joint.linearLimit.limit;

        if (Math.Abs(value) < deadZone)
            value = 0;

        return Mathf.Clamp(value, -1f, 1f);
    }

    private void Pressed()
    {
        _isPressed = true;
        onPressed.Invoke();
        Debug.Log("Pressed");
    }

    private void Realeased()
    {
        _isPressed = false;
        onReleased.Invoke();
        Debug.Log("Released");
    }

    public void MoveToScene()
    {
        Debug.Log("Calling Move to scene");
        SceneManager.LoadSceneAsync("Scenes/Level 2");
    }
}
