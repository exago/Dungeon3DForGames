using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTakeOverBehaviour : MonoBehaviour
{
    TakeOverBehaviour _takeOverBehaviour = null;

    public bool IsSwitching = false;
    private bool _canSwitch = true;

    [SerializeField] private float _switchTime = 0.0f;
    private float _switchTimer = 0.0f;

    [SerializeField] private float _switchCooldownTime = 0.0f;
    private float _cooldownTimer = 0.0f;

    [SerializeField] private Transform _throwKnifePosition = null;
    [SerializeField] private GameObject _throwKnifePrefab = null;

    private bool _throwKnife = false;
    private bool _threwOnce = false;


    private void Awake()
    {
        _takeOverBehaviour = GetComponent<TakeOverBehaviour>();
    }

    public void Switch()
    {
        if (_canSwitch)
        {
            TimeBehaviour.StartSlowMotion(0.6f);
            IsSwitching = true;
            _canSwitch = false;
            _takeOverBehaviour.ReturnToNormal();
        }
    }


    private void ThrowKnife()
    {
        GameObject knife = Instantiate(_throwKnifePrefab, _throwKnifePosition.position, _throwKnifePosition.rotation);
        knife.GetComponent<ThrowKnifeBehaviour>().TakeOverBehaviour = _takeOverBehaviour;
        _throwKnife = false;
        _threwOnce = true;
    }

    private void Update()
    {
        if (_throwKnife && !_threwOnce)
        {
            ThrowKnife();
        }
        if (IsSwitching)
        {
            CheckTimer();
        }

        if (!_canSwitch)
        {
            CheckCooldownTimer();
        }

    }

    private void CheckCooldownTimer()
    {
        _cooldownTimer += Time.deltaTime;

        if (_cooldownTimer >= _switchCooldownTime)
        {
            _cooldownTimer = 0.0f;
            _canSwitch = true;
            _threwOnce = false;
            _throwKnife = false;
        }
    }

    private void CheckTimer()
    {
        _switchTimer += Time.deltaTime;

        if (_switchTimer >= _switchTime)
        {
            IsSwitching = false;
            TimeBehaviour.EndSlowMotion();
            _switchTimer = 0.0f;
            _throwKnife = false;
        }
    }

    public void SwitchTakeOver()
    {
        _throwKnife = true;
    }
}
