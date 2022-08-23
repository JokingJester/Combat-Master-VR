using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OffsetInteractable : XRGrabInteractable
{
    [SerializeField] private bool _useOffsetInteractable = true;
    [SerializeField] private float _speed = 0.5f;
    private Rigidbody _rb;

    private bool _canHover;
    private float _movementRange;
    private Vector3 _position;
    private Vector3 _currentPosition;

    protected override void Awake()
    {
        base.Awake();
        _position.y = 0.02f;
        _rb = GetComponent<Rigidbody>();
        if(_useOffsetInteractable == true)
            CreateAttachTransform();
    }
    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        base.OnSelectEntering(args);
        StopHovering();
        if(_useOffsetInteractable == true)
            MatchAttachPoint(args.interactorObject);
    }

    private void Update()
    {
        if(_canHover == true)
        {
            Hover();
        }
    }

    protected void MatchAttachPoint(IXRInteractor interactor)
    {
        if (IsFirstSelecting(interactor))
        {
            bool isDirect = interactor is XRDirectInteractor;
            attachTransform.position = isDirect ? interactor.GetAttachTransform(this).position : transform.position;
            attachTransform.rotation = isDirect ? interactor.GetAttachTransform(this).rotation : transform.rotation;
        }
    }

    private bool IsFirstSelecting(IXRInteractor interactor)
    {
        return interactor == firstInteractorSelecting;
    }

    private void CreateAttachTransform()
    {
        if (attachTransform == null)
        {
            GameObject createdAttachTransform = new GameObject();
            createdAttachTransform.transform.parent = this.transform;
            attachTransform = createdAttachTransform.transform;
        }
    }

    public void StopHovering()
    {
        _rb.isKinematic = false;
        _rb.freezeRotation = false;
        _canHover = false;
        attachTransform.position = new Vector3();
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
