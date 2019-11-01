using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshMovementBehaviour : MovementBehaviour
{
    [SerializeField]
    private NavMeshAgent _navMeshAgent = null;

    protected override void Awake()
    {
        base.Awake();

        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = _movementSpeed;

    }

    protected override void HandleMovement()
    {        
        if (_targetPosition == Vector3.zero)
            _navMeshAgent.isStopped = true;
        else
        {
            _navMeshAgent.isStopped = false;
            _navMeshAgent.SetDestination(_targetPosition);
        }
    }

    protected override void HandleRotation()
    {
        base.HandleRotation();
    }

}
