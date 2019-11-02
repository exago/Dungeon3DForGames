using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public MovementBehaviour MovementBehaviour = null;
    public ShootingBehaviour ShootingBehaviour = null;
    private StealthBehaviour _stealthBehaviour = null;
    private TakeOverBehaviour _takeOverBehaviour = null;
    private InterActionBehavior _interActionBehaviour = null;
    private HealthBehaviour _healthBehaviour = null;

    private void Awake()
    {
        MovementBehaviour = GetComponent<MovementBehaviour>();
        ShootingBehaviour = GetComponent<ShootingBehaviour>();
        _stealthBehaviour = GetComponent<StealthBehaviour>();
        _takeOverBehaviour = GetComponent<TakeOverBehaviour>();
        _interActionBehaviour = GetComponent<InterActionBehavior>();
        _healthBehaviour = GetComponent<HealthBehaviour>();
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

        Vector3 positionOfMouseInWorld = transform.position;

        if (Physics.Raycast(mouseRay, out RaycastHit hitInfo, Camera.main.farClipPlane, LayerMask.GetMask("Ground")))
        {
            positionOfMouseInWorld = hitInfo.point;
        }

        positionOfMouseInWorld.y = transform.position.y;
        MovementBehaviour.DesiredLookAtPoint = positionOfMouseInWorld;
    }
    private void HandleGeneralInput()
    {

        if (Input.GetButton("Fire"))
            ShootingBehaviour.Fire();

        if (Input.GetButtonDown("Reload"))
            ShootingBehaviour.Reload();

        if (Input.GetButtonDown("Stealth") && !_takeOverBehaviour.TakenOver )
            _stealthBehaviour.ToggleStealth();

        if (Input.GetButtonDown("Interact"))
            _interActionBehaviour.Interact();
    }
    private void HandleHealth()
    {
        if(_healthBehaviour.CurrentHealth <= 0)
        {
            DoWhenDead();
        }
    }

    private void DoWhenDead()
    {
        _healthBehaviour.Kill();
    }
}
