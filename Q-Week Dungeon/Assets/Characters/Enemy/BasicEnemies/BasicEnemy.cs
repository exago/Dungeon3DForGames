using UnityEngine;

public class BasicEnemy : Enemy
{
    protected override void BehaviourPerState()
    {
        CheckEnemyState();

        switch (_state)
        {
            case EnemyState.passive:
               
                CheckForReload();
                break;

            case EnemyState.active:
                
                WalkToPlayer();
                LookAtPlayer();
                CheckDistanceForShooting();
                CheckForReload();                
                break;

            case EnemyState.alerted:
                
                LookAtPlayer();
                UpdateActiveAndAlertedState();
                SetActive();
                break;
        }
            
    }

    private void WalkToPlayer()
    {
        _movementBehaviour.DesiredMovementDirection = (_player.position - transform.position).normalized;
    }
    private void InSightCalculation()
    {
        //gets vector to the player
        Vector3 playerPositionOnOurHeight = _player.position;
        playerPositionOnOurHeight.y = transform.position.y;
        Vector3 vectorToThePlayer = playerPositionOnOurHeight - this.transform.position;
        vectorToThePlayer = Vector3.Normalize(vectorToThePlayer);

        //calculate if player is in viewingcone
        float isInViewingCone = Vector3.Dot(vectorToThePlayer, transform.forward);

        if (Mathf.Acos(isInViewingCone) < _attentionAngleInRadians / 2 && Vector3.Distance(_player.position, this.transform.position) < _attackRadius)
        {
            _state = EnemyState.alerted;
        }
    }

    private void CheckDistanceForShooting()
    {
        float distance = Vector3.Distance(this.transform.position, _player.position);

        if (distance > _attackRadius)
            return;
        else
        {
            _shootingBehaviour.Fire();
        }
    }
    private void CheckEnemyState()
    {
       
        if (_player == null)
        {
            _state = EnemyState.passive;
            return;
        }

        if (!_playerStealthBehaviour.IsStealth)
            InSightCalculation();


    }

    private void LookAtPlayer()
    {
        Vector3 lookAtPoint = _player.position;
        lookAtPoint.y = this.transform.position.y;
        _movementBehaviour.DesiredLookAtPoint = lookAtPoint;
    }

}
