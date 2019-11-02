using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected MovementBehaviour _movementBehaviour = null;
    protected ShootingBehaviour _shootingBehaviour = null;

    protected StealthBehaviour _playerStealthBehaviour = null;
    protected HealthBehaviour _healthBehaviour = null;
    protected LootDropBehaviour _lootDropBehaviour = null;

    protected Transform _player = null;
    [SerializeField]
    protected float _attackRadius = 15.0f;
    [SerializeField]
    protected float _attentionAngleInDegrees = 120.0f; //between 1 ° and 89°
    protected float _attentionAngleInRadians = 0.0f;

    [SerializeField]
    protected float _enemyTimeToTakeAction = 0.5f;

    protected float _alertedToActiveTimer = 0.0f;

    protected EnemyState _state = EnemyState.passive;

    protected void Awake()
    {
        _movementBehaviour = GetComponent<MovementBehaviour>();
        _shootingBehaviour = GetComponent<ShootingBehaviour>();
        _healthBehaviour = GetComponent<HealthBehaviour>();
        _lootDropBehaviour = GetComponent<LootDropBehaviour>();

        _state = EnemyState.passive;

        _player = FindObjectOfType<Player>().transform;
        _playerStealthBehaviour = _player.GetComponent<StealthBehaviour>();
        _attentionAngleInRadians = (_attentionAngleInDegrees * Mathf.PI) / 180.0f;
    }

    protected void Update()
    {
        BehaviourPerState();
        
        if(_healthBehaviour.CurrentHealth <= 0)
        {
            DoWhenDead();
        }
    }

    protected virtual void BehaviourPerState() { }

    protected virtual void DoWhenDead() { _healthBehaviour.Kill(); _lootDropBehaviour.Drop(); }


    protected void CheckForReload()
    {
        int currentAmmo = _shootingBehaviour._currentWeapon.Ammo;
        int magazineSize = _shootingBehaviour._currentWeapon.MagazineSize;

        if (currentAmmo == magazineSize)
            return;

        if (currentAmmo == 0)
            _shootingBehaviour.Reload();

        if (_state == EnemyState.passive)
            _shootingBehaviour.Reload();
    }

    protected void SetActive()
    {
        if (_alertedToActiveTimer >= _enemyTimeToTakeAction)
            _state = EnemyState.active;
    }

    protected void UpdateActiveAndAlertedState()
    {
        _alertedToActiveTimer += Time.deltaTime;
    }

    
}