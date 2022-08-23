using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverObject : MonoBehaviour
{
    [SerializeField] private float _speed = 0.5f;
    [SerializeField] private Rigidbody _rb;

    private bool _canHover;

    private float _movementRange;

    private Vector3 _position;
    private Vector3 _currentPosition;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _position.y = 0.02f;
    }

    void Update()
    {
        if(_canHover == true)
        {
            Hover();
        }
    }

    public void StopHovering()
    {
        _rb.isKinematic = false;
        _rb.freezeRotation = false;
        _canHover = false;
    }

    public void StartHovering()
    {
        _currentPosition = transform.position;
        _rb.isKinematic = true;
        _canHover = true;
        _rb.freezeRotation = true;
    }

    public void Hover()
    {
        float cycle = Time.time / _speed;
        float newCycle = Mathf.Sin(cycle);
        _movementRange = newCycle;
        Vector3 newPosition = _position * _movementRange;
        transform.position = _currentPosition + newPosition;
    }
}
