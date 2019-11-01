using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBarrelScript : AnimatedObjectBehaviour
{
    private HealthBehaviour _healthBehaviour = null;
    [SerializeField] private float _radius = 5.0f;
    [SerializeField] private float _power = 500.0f;
    [SerializeField] private float _rawPower = 100;
    [SerializeField] private LayerMask _enemyLayerMask = 9;
    protected override void Awake()
    {
        _healthBehaviour = GetComponent<HealthBehaviour>();
        base.Awake();
    }

    protected override void Update()
    {
        CheckHealth();
        base.Update();
    }
    public override void Interaction()
    {
        Explode();
    }

    private void CheckHealth()
    {
        if(_healthBehaviour.CurrentHealth <= 0)
        {
            Explode();
            _healthBehaviour.Kill();
        }
    }

    private void Explode()
    {
        Debug.Log("Boom");

        Collider[] colliderArray = Physics.OverlapSphere(this.transform.position, _radius, _enemyLayerMask);

        for (int i = 0; i < colliderArray.Length; i++)
        {
            Vector3 positionFromBlast = colliderArray[i].attachedRigidbody.position - this.transform.position;

            float distanceFromBlast = positionFromBlast.magnitude;
            float actualForce = _rawPower + (Mathf.Pow(_power, -distanceFromBlast) * colliderArray[i].attachedRigidbody.mass);

            colliderArray[i].attachedRigidbody.constraints = RigidbodyConstraints.None;
            colliderArray[i].attachedRigidbody.isKinematic = false;
            colliderArray[i].attachedRigidbody.AddForce(positionFromBlast.normalized * actualForce);

        }
        _healthBehaviour.TakeDamage(1000);
    }
}
