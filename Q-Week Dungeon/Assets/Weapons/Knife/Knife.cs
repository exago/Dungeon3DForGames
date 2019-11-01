using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : Weapon
{
    private TakeOverBehaviour _takeOverBehaviour = null;

    [SerializeField]
    private float _acceptedAngleFromBehind = 90;
    [SerializeField]
    private float _knifingDistance = 5;
    [SerializeField]
    private LayerMask _enemyLayerMask = 9;
    [SerializeField]
    private Transform _playerPosition = null;

    private bool _isStabbing = false;

    private float _acceptedAngleFromBehindInRadians = 0;
    
    private void Awake()
    {        
        MagazineSize = 1;
        _ammo = MagazineSize;

        _acceptedAngleFromBehindInRadians = _acceptedAngleFromBehind * Mathf.Deg2Rad;
        _playerPosition = FindObjectOfType<Player>().transform;
        _takeOverBehaviour = FindObjectOfType<Player>().GetComponent<TakeOverBehaviour>();

    }

    private void FixedUpdate()
    {
        if (_isStabbing)
        {
            RaycastHit hit;

            if (CheckIfEnemyHit(out hit))
            {
                CalculateIfPlayerHitsFromBehind(hit);
            }
            ToggleStab();            
        }
    }

    public override void Shoot()
    {
        base.Shoot();

        if(_canShoot)
            ToggleStab();

        Reload();

    }

    public override void Reload()
    {
        _ammo = MagazineSize;
    }

    private void ToggleStab()
    {
        _isStabbing = !_isStabbing;
    }

    private bool CheckIfEnemyHit(out RaycastHit hit)
    {
        Ray knifeRay = new Ray(_playerPosition.position, transform.forward);
        Debug.DrawRay(knifeRay.origin, knifeRay.direction);

        if(Physics.Raycast(knifeRay,out hit, _knifingDistance, _enemyLayerMask))
        {
            return true;
        }

        return false;
    }

    private void CalculateIfPlayerHitsFromBehind(RaycastHit hit)
    {
        Vector3 playerForwardVector = transform.forward;
        Vector3 enemyForwardVector = hit.collider.transform.forward;

        //reset height
        playerForwardVector.y = enemyForwardVector.y;

        //check if player attacks from behind
        float dotProductResult = Vector3.Dot(playerForwardVector, enemyForwardVector);

        if (Mathf.Acos(dotProductResult) < _acceptedAngleFromBehindInRadians)
        {
            _takeOverBehaviour.TakeOver(hit.collider.gameObject);
        }
    }

}
