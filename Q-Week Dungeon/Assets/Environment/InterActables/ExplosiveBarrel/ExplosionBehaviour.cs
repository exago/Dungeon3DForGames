using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehaviour : MonoBehaviour
{
    [SerializeField] private float _radius = 5.0f;
    [SerializeField] private int _maxDamage = 20;
    [SerializeField] private float _power = 500.0f;
    [SerializeField] private float _rawPower = 100;
    [SerializeField] private LayerMask _enemyLayerMask = 9;

    private bool _hasAlreadyExploded = false;


    public void Explode()
    {
        if (!_hasAlreadyExploded)
        {
            _hasAlreadyExploded = true;

            Collider[] colliderArray = Physics.OverlapSphere(this.transform.position, _radius, _enemyLayerMask);

            for (int i = 0; i < colliderArray.Length; i++)
            {
                Vector3 positionFromBlast = colliderArray[i].attachedRigidbody.position - this.transform.position;

                float distanceFromBlast = positionFromBlast.magnitude;
                float actualForce = _rawPower + (Mathf.Pow(_power, -distanceFromBlast) * colliderArray[i].attachedRigidbody.mass);

                colliderArray[i].attachedRigidbody.constraints = RigidbodyConstraints.None;
                colliderArray[i].attachedRigidbody.isKinematic = false;
                colliderArray[i].attachedRigidbody.AddForce(positionFromBlast.normalized * actualForce);

                int damage = _maxDamage - (int)distanceFromBlast;
                colliderArray[i].GetComponent<HealthBehaviour>().TakeDamage(damage);

            }
        }
    }
}
