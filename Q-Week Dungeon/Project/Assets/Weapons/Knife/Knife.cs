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
        Ray knifeRay = new Ray(_playerPosition.position - (Vector3.up / 2), transform.forward);

        if(Physics.Raycast(knifeRay,out hit, _knifingDistance, _enemyLayerMask))
        {
            return true;
        }

        return false;
    }

    private void CalculateIfPlayerHitsFromBehind(RaycastHit hit)
    {
        Vector3 playerForwardVector = transform.forward;
        Vector3 enemyForwardVector = hit.collider.gameObject.transform.forward;

        
        //reset height
        playerForwardVector.y = enemyForwardVector.y;
        Debug.DrawRay(hit.collider.transform.position, hit.collider.gameObject.transform.forward, Color.red, 3.0f);
        Debug.DrawRay(this.transform.position, transform.forward, Color.green, 3.0f);
        //check if player attacks from behind
        float dotProductResult = Vector3.Dot(playerForwardVector, enemyForwardVector);
        dotProductResult = dotProductResult / (playerForwardVector.magnitude * enemyForwardVector.magnitude);
        Debug.Log(Mathf.Acos(dotProductResult));
        if (Mathf.Acos(dotProductResult) < _acceptedAngleFromBehindInRadians)
        {
            _takeOverBehaviour.TakeOver(hit.collider.gameObject);
        }
    }

}
