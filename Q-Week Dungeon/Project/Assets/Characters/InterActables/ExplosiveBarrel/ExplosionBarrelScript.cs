using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBarrelScript : AnimatedObjectBehaviour
{
    private ExplosionBehaviour _explosionBehaviour = null;
    private HealthBehaviour _healthBehaviour = null;

    [SerializeField] private float _explosionTimer = 0.5f;
    [SerializeField] private AudioClip _explosionAudio = null;

    protected override void Awake()
    {
        base.Awake();
        _explosionBehaviour = GetComponent<ExplosionBehaviour>();
        _healthBehaviour = GetComponent<HealthBehaviour>();
    }

    public override void Interaction()
    {
        InitExplosion();
    }

    private void InitExplosion()
    {
        _animationControlBehaviour.IsExploding = true;
        Invoke("Explode", _explosionTimer);
    }

    protected override void Update()
    {
        base.Update();

        if (_healthBehaviour.CurrentHealth <= 0)
        {
            DoOnDeath();
        }
    }

    private void DoOnDeath()
    {
        InitExplosion();
    }

    private void Explode()
    {
        _explosionBehaviour.Explode();

        AudioManager.Instance.PlayAudioClip(_explosionAudio);

        _healthBehaviour.Kill();
    }
}
