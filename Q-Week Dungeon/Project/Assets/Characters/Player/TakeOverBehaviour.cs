using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TakeOverBehaviour : MonoBehaviour
{
    private Player _playerScript;

    [SerializeField]
    private int _playerInDisguiseLayerMask = 10;
    [SerializeField]
    private GameObject _visuals = null;
    [SerializeField]
    private GameObject _logistics = null;
    [SerializeField]
    private int _knockBackPower = 20;

    private Rigidbody _playerRigidbody = null;
    private ShootingBehaviour _playerShootingBehaviour = null;
    private StealthBehaviour _playerStealthBehaviour = null;
    private Collider _playerCollider = null;
    private HealthBehaviour _healthBehaviour = null;

    private GameObject _takeOverObject = null;
    public bool TakenOver = false;


    private void Awake()
    {
        _playerScript = GetComponent<Player>();
        _playerRigidbody = _playerScript.GetComponent<Rigidbody>();
        _playerShootingBehaviour = _playerScript.GetComponent<ShootingBehaviour>();
        _playerStealthBehaviour = _playerScript.GetComponent<StealthBehaviour>();
        _playerCollider = _playerScript.GetComponent<Collider>();
        _healthBehaviour = _playerScript.GetComponent<HealthBehaviour>();
    }

    private void Update()
    {
        CheckIfTakenOverObjectIsStillAlive();
    }

    private void CheckIfTakenOverObjectIsStillAlive()
    {
        if(_takeOverObject == null && TakenOver)
        {
            ReturnToNormal();
        }
    }

    public void TakeOver(GameObject target)
    {
        _playerCollider.enabled = false;
        gameObject.layer = _playerInDisguiseLayerMask;
        _visuals.SetActive(false);
        _logistics.SetActive(false) ;

        _takeOverObject = target;

        if (_playerStealthBehaviour.IsStealth)
            _playerStealthBehaviour.ToggleStealth();

        target.GetComponent<MovementBehaviour>().enabled = false;
        target.GetComponent<Enemy>().enabled = false;

        _playerScript.MovementBehaviour.Rigidbody = target.GetComponent<Rigidbody>();
        _playerScript.MovementBehaviour.Rigidbody.isKinematic = false;
        _playerScript.ShootingBehaviour = target.GetComponent<ShootingBehaviour>();
        _playerScript.HealthBehaviour = target.GetComponent<HealthBehaviour>();
        TakenOver = true;
        _takeOverObject.layer = 13;

        RoomManager.SetEnemyCounterAsync();
    }

    public void ReturnToNormal()
    {
        if (_takeOverObject != null)
        {
            _takeOverObject.GetComponent<Enemy>().DoWhenDead();
            _takeOverObject = null;
        }

        _playerCollider.enabled = true;
        gameObject.layer = 0;
        _visuals.SetActive(true);
        _logistics.SetActive(true);

        this.transform.position = transform.position + Vector3.up;
        _playerRigidbody.position = _playerRigidbody.position + Vector3.up;
        _playerRigidbody.AddForce(_knockBackPower * -transform.forward.normalized, ForceMode.Impulse);


        _playerScript.MovementBehaviour.Rigidbody = _playerRigidbody;
        _playerScript.ShootingBehaviour = _playerShootingBehaviour;
        _playerScript.HealthBehaviour = _healthBehaviour;

        TakenOver = false;
        _takeOverObject.layer = 9;

        RoomManager.SetEnemyCounterAsync();

    }
}
