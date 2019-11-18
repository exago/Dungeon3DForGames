using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : AnimatedObjectBehaviour
{
    private bool _isDoorOpen = false;
    public bool IsDoorOpen { get { return _isDoorOpen; } set { _isDoorOpen = value; } }

    [SerializeField]
    private float _closeDoorTimer = 3.0f;
    private float _timerForClosingDoor = 0;

    private Collider _thisCollider = null;

    protected override void Awake()
    {
        _thisCollider = GetComponent<Collider>();
        base.Awake();
    }

    protected override void Update()
    {
        AutomaticClosingDoor();
        SetCollider();
        SetAnimation();
        base.Update();
    }

    

    public override void Interaction()
    {
        if(RoomManager.CanOpenDoors)
            OpenDoor();
    }

    private void AutomaticClosingDoor()
    {
        if (_isDoorOpen)
        {
            _timerForClosingDoor += Time.deltaTime;

            if (_timerForClosingDoor >= _closeDoorTimer)
            {
                IsDoorOpen = false;
                _timerForClosingDoor = 0;
            }

        }
    }
    private void SetCollider()
    {
        if (_isDoorOpen)
            _thisCollider.enabled = false;
        else
            _thisCollider.enabled = true;
    }
    protected override void SetAnimation()
    {
        if (_isDoorOpen != _animationControlBehaviour.IsOpen)
            _animationControlBehaviour.IsOpen = _isDoorOpen;

    }
    private void OpenDoor()
    {
        _isDoorOpen = true;
    }

}
