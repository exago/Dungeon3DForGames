using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public MovementBehaviour MovementBehaviour = null;
    public ShootingBehaviour ShootingBehaviour = null;
    private StealthBehaviour _stealthBehaviour = null;
    private TakeOverBehaviour _takeOverBehaviour = null;
    private InterActionBehavior _interActionBehaviour = null;
    public HealthBehaviour HealthBehaviour = null;
    private DefendingBehaviour _defendingBehaviour = null;
    private SwitchTakeOverBehaviour _swithcTakeOverBehaviour = null;

    private void Awake()
    {
        MovementBehaviour = GetComponent<MovementBehaviour>();
        ShootingBehaviour = GetComponent<ShootingBehaviour>();
        _stealthBehaviour = GetComponent<StealthBehaviour>();
        _takeOverBehaviour = GetComponent<TakeOverBehaviour>();
        _interActionBehaviour = GetComponent<InterActionBehavior>();
        HealthBehaviour = GetComponent<HealthBehaviour>();
        _defendingBehaviour = GetComponent<DefendingBehaviour>();
        _swithcTakeOverBehaviour = GetComponent<SwitchTakeOverBehaviour>(); ;
    }

    private void Update()
    {
        HandleGeneralInput();
        HandleHealth();
    }

  

    private void FixedUpdate()
    {
        HandleMovementInput();
    }



    private void HandleMovementInput()
    {
        if (MovementBehaviour == null)
            return;

        //Position
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        Vector3 movement = horizontalMovement * Vector3.right + verticalMovement * Vector3.forward;
        movement.Normalize();

        MovementBehaviour.DesiredMovementDirection = movement;

        //Rotation
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(mouseRay.origin, mouseRay.direction);

        Vector3 positionOfMouseInWorld = transform.position;

        if (Physics.Raycast(mouseRay, out RaycastHit hitInfo, Camera.main.farClipPlane, LayerMask.GetMask("Ground")))
        {
            positionOfMouseInWorld = hitInfo.point;
        }

        MovementBehaviour.DesiredLookAtPoint = positionOfMouseInWorld - this.transform.position;
    }
    private void HandleGeneralInput()
    {
        if (Input.GetButtonUp("Fire"))
            ShootingBehaviour.Release();

        if (Input.GetButton("Fire"))
        {
            if (_swithcTakeOverBehaviour.IsSwitching)
                _swithcTakeOverBehaviour.SwitchTakeOver();
            else
                ShootingBehaviour.Fire();
        }     
        
        if (Input.GetButton("Defend"))
            _defendingBehaviour.Defend();

        if (Input.GetButtonDown("Reload"))
            ShootingBehaviour.Reload();

        if (Input.GetButtonDown("Stealth") && !_takeOverBehaviour.TakenOver )
            _stealthBehaviour.ToggleStealth();

        if (Input.GetButtonDown("Interact"))
            _interActionBehaviour.Interact();

        if (Input.GetButtonDown("Switch") && _takeOverBehaviour.TakenOver)
            _swithcTakeOverBehaviour.Switch();
    }
    private void HandleHealth()
    {
        if(HealthBehaviour.CurrentHealth <= 0)
        {
            DoWhenDead();
        }
    }

    private void DoWhenDead()
    {
        if(_takeOverBehaviour.TakenOver)
        {
            _takeOverBehaviour.ReturnToNormal();
        }
        else
        {
            HealthBehaviour.Kill();
        }
    }
}
