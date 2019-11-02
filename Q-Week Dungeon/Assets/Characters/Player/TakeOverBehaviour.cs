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

    private Rigidbody _playerRigidbody = null;
    private ShootingBehaviour _playerShootingBehaviour = null;
    private StealthBehaviour _playerStealthBehaviour = null;
    private Collider _playerCollider = null;

    private GameObject _takeOverObject = null;
    public bool TakenOver = false;


    private void Awake()
    {
        _playerScript = GetComponent<Player>();
        _playerRigidbody = _playerScript.GetComponent<Rigidbody>();
        _playerShootingBehaviour = _playerScript.GetComponent<ShootingBehaviour>();
        _playerStealthBehaviour = _playerScript.GetComponent<StealthBehaviour>();
        _playerCollider = _playerScript.GetComponent<Collider>();
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
        TakenOver = true;
    }

    public void ReturnToNormal()
    {
        _playerCollider.enabled = true;
        gameObject.layer = 0;
        _visuals.SetActive(true);
        _logistics.SetActive(true);

        this.transform.position = transform.position + Vector3.up;
        _playerRigidbody.position = _playerRigidbody.position + Vector3.up;


        _playerScript.MovementBehaviour.Rigidbody = _playerRigidbody;
        _playerScript.ShootingBehaviour = _playerShootingBehaviour;

        TakenOver = false;

    }
}
