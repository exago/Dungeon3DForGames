using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehaviour : MonoBehaviour
{
    [SerializeField]
    protected float _movementSpeed = 5.0f;
    public float MovementSpeed { get { return _movementSpeed; } set { _movementSpeed = value; } }

    protected Rigidbody _rigidbody;
    public Rigidbody Rigidbody { get { return _rigidbody; } set { _rigidbody = value; } }

    protected Vector3 _desiredMovementDirection = Vector3.zero;
    public Vector3 DesiredMovementDirection { get { return _desiredMovementDirection; } set { _desiredMovementDirection = value; } }

    protected Vector3 _desiredLookAtPoint = Vector3.zero;
    public Vector3 DesiredLookAtPoint { get { return _desiredLookAtPoint; } set {   _desiredLookAtPoint = value; } }
    

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _desiredLookAtPoint =  transform.forward;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
     
        HandleMovement();
        HandleRotation();
    }

    protected virtual void HandleMovement()
    {
        Vector3 movement = _desiredMovementDirection * _movementSpeed * Time.deltaTime;
        
        if(_rigidbody == null)
            _rigidbody = this.gameObject.GetComponent<Rigidbody>();
        else
            _rigidbody.MovePosition(_rigidbody.position + movement);

        transform.position = _rigidbody.position;
    }

    protected virtual void HandleRotation()
    {
        _desiredLookAtPoint.y = this.transform.position.y;
        transform.LookAt(_desiredLookAtPoint, Vector3.up);
        Rigidbody.rotation = transform.rotation;
    }
}
