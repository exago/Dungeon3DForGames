using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefendingBehaviour : MonoBehaviour
{
    [SerializeField] private ShieldBehaviour _shieldBehaviour = null;
    
    [SerializeField] private float _coolDownTime = 3.0f;

    private bool _coolDown = true;

    private float _timerCooldown = 0.0f;

    private void Awake()
    {
        _shieldBehaviour = GetComponentInChildren<ShieldBehaviour>();
    }

    private void Update()
    {
        CoolDownCalculation();
    }

    private void CoolDownCalculation()
    {
        if (!_coolDown)
        {
            _timerCooldown += Time.deltaTime;

            if(_timerCooldown >= _coolDownTime)
            {
                _coolDown = true;
                _timerCooldown = 0;
            }
        }
    }

    public void Defend()
    {
        if(_shieldBehaviour.CanBeUsed && _coolDown)
        {
            _shieldBehaviour.SetActiveState(true);
            _coolDown = false;
        }

    }
}
