using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class HandAnim : MonoBehaviour
{
    [SerializeField] private ActionBasedController _controller;
    [SerializeField] private Animator _anim;
    [SerializeField] private InputActionAsset _inputAction;
    [SerializeField] private float _speed = 6;
    public enum Hand { Left, Right }
    public Hand thisHand;

    private float _gripCurrent;
    private float _gripTarget;

    private float _triggerCurrent;
    private float _triggerTarget;

    private float _thumbCurrent;
    private float _thumbTarget;

    private float _pointCurrent;
    private float _pointTarget;

    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        AnimateGripAndPinch();
        AnimateThumb();
        AnimatePointing();
    }

    private void AnimateGripAndPinch()
    {
        _gripTarget = _controller.selectAction.action.ReadValue<float>();
        if (_gripCurrent != _gripTarget)
        {
            _gripCurrent = Mathf.MoveTowards(_gripCurrent, _gripTarget, Time.deltaTime * _speed);
        }

        _triggerTarget = _controller.activateAction.action.ReadValue<float>();
        if (_triggerCurrent != _triggerTarget)
        {
            _triggerCurrent = Mathf.MoveTowards(_triggerCurrent, _triggerTarget, Time.deltaTime * _speed);
        }

        _anim.SetFloat("Flex", _gripCurrent);
        _anim.SetFloat("Pinch", _triggerCurrent);
    }

    private void AnimateThumb()
    {
        if (thisHand == Hand.Left)
        {
            _thumbTarget = _inputAction.actionMaps[2].actions[8].ReadValue<float>();
        }
        else if (thisHand == Hand.Right)
        {
            _thumbTarget = _inputAction.actionMaps[5].actions[8].ReadValue<float>();
        }

        //if the target = 0 then it will equal 1. If the target = 1 then it will equal 0.
        //If this code isn't here then the thumb will stick out when you touch the controller.
        _thumbTarget = _thumbTarget == 0 ? _thumbTarget = 1 : _thumbTarget = 0;

        if (_thumbCurrent != _thumbTarget)
        {
            _thumbCurrent = Mathf.MoveTowards(_thumbCurrent, _thumbTarget, Time.deltaTime * _speed);
        }
        _anim.SetLayerWeight(1, _thumbCurrent);
    }

    private void AnimatePointing()
    {
        if (thisHand == Hand.Left)
        {
            _pointTarget = _inputAction.actionMaps[2].actions[9].ReadValue<float>();
        }
        else if (thisHand == Hand.Right)
        {
            _pointTarget = _inputAction.actionMaps[5].actions[9].ReadValue<float>();
        }

        _pointTarget = _pointTarget == 0 ? _pointTarget = 1 : _pointTarget = 0;

        if (_pointCurrent != _pointTarget)
        {
            _pointCurrent = Mathf.MoveTowards(_pointCurrent, _pointTarget, Time.deltaTime * _speed);
        }
        _anim.SetLayerWeight(2, _pointCurrent);
    }
}
