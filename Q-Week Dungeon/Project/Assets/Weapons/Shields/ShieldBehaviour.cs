using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBehaviour : MonoBehaviour
{
    [SerializeField] private HealthBehaviour _healthBehaviour = null;

    [SerializeField] private float _timeToRechargeShieldHealth = 0.5f;
    [SerializeField] private float _shieldLifeTime = 0.1f;

    public bool CanBeUsed = false;

    Renderer _renderer = null;
    Collider _collider = null;

    private float _timerHealth = 0.0f;
    private float _timerLifeTime = 0.0f;

    private bool _active = false;

    private void Awake()
    {
        _renderer = GetComponentInChildren<Renderer>();
        _collider = GetComponent<Collider>();
        _healthBehaviour = GetComponent<HealthBehaviour>();
    }

    private void Update()
    {
        ShieldRecharge();
        ShieldLifeTimeChange();
        CheckIfShieldCanBeUsed();
    }

    private void CheckIfShieldCanBeUsed()
    {
        if (_healthBehaviour.CurrentHealth > 0)
            CanBeUsed = true;
        else
            CanBeUsed = false;
    }

    private void ShieldRecharge()
    {
        if (_healthBehaviour.CurrentHealth < _healthBehaviour.MaxHealth)
        {
            _timerHealth += Time.deltaTime;

            if (_timerHealth >= _timeToRechargeShieldHealth)
            {
                _timerHealth -= _timeToRechargeShieldHealth;
                _healthBehaviour.TakeDamage(-1);
            }
        }
    }
    private void ShieldLifeTimeChange()
    {
        if (_active)
        {
            _timerLifeTime += Time.deltaTime;

            if (_timerLifeTime >= _shieldLifeTime)
            {
                SetActiveState(false);
                _timerLifeTime = 0;
            }
        }
    }

    public void SetActiveState(bool active)
    {
        _renderer.enabled = active;
        _collider.enabled = active;
        _active = active;
    }
}
