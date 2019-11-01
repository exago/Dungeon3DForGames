using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected int _ammo = 0;
    public int Ammo { get { return _ammo; } set { _ammo = value; } }
    [SerializeField]
    protected int _magazineSize = 0;
    public int MagazineSize { get { return _magazineSize; } set{ _magazineSize = value; } }
    [SerializeField]
    protected float _attackSpeed = 0.0f;

    [SerializeField]
    protected float _reloadTime = 0.0f;

    [SerializeField]
    protected float _bulletSpeed = 0.0f;
    [SerializeField]
    protected GameObject _bulletPrefab = null;

    [SerializeField]
    protected List<Transform> _fireOutputPositions = new List<Transform>();
    protected float _fireTimer = 0.0f;
    protected bool _canShoot = false;

    [SerializeField]
    protected int _damage = 0;

    private void Awake()
    {
        _ammo = MagazineSize;
    }

    private void Update()
    {
        _fireTimer += Time.deltaTime;
    }
    public virtual void Shoot() 
    {
        if (_fireTimer < _attackSpeed)
            _canShoot = false;
        else if (_ammo >= 0)
        {
            _canShoot = true;
            _fireTimer = 0.0f;
            _ammo--;
        }

    }
    public virtual void Reload() 
    {
        if (_ammo != _magazineSize)
            Invoke("FillMagazine", _reloadTime);
    }

    protected virtual float CalculateBulletSpeed()
    {
        return 0.0f;
    }

    private void FillMagazine()
    {
        _ammo = _magazineSize;
    }
}
