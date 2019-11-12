using System;
using UnityEngine;

public class ShootingBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject _primaryGunTemplate = null;

    [SerializeField]
    private Transform _primarySocket = null;

    public Weapon CurrentWeapon = null;

    private bool _playerCanShoot = false;
    private bool _checkForSound = false;

    private void Awake()
    {
        //SpawnGuns
        if (_primaryGunTemplate && _primarySocket)
        {
            GameObject g = Instantiate(_primaryGunTemplate, _primarySocket);
            g.transform.localPosition = Vector3.zero;
            g.transform.localRotation = Quaternion.identity;
            CurrentWeapon = g.GetComponent<Weapon>();
        }
    }

    private void FixedUpdate()
    {
        if (_checkForSound)
            CheckSound();
    }

    private void CheckSound()
    {
        Collider[] enemies = Physics.OverlapSphere(this.transform.position, CurrentWeapon.SoundRadius, 1 << 9);

        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].GetComponent<Enemy>().State != EnemyState.active)
                enemies[i].GetComponent<Enemy>().State = EnemyState.alerted;
        }
    }

    public void Fire()
    {
        if (!CurrentWeapon)
        {
            Debug.Log(this.gameObject + "doesn't have a gun");
        }
        
        if(CurrentWeapon.Ammo != 0)
        {
            if(this.gameObject.layer == 13)
            {
                if (_playerCanShoot)
                {
                    CurrentWeapon.Shoot();
                    _checkForSound = true;      
                    
                }
                    
            }
            else
                CurrentWeapon.Shoot();
        }
            
    }

    public void Release()
    {
        CurrentWeapon.Release();
        _playerCanShoot = true;
        _checkForSound = false;
    }

    public void Reload()
    {
        if (!CurrentWeapon)
        {
            return;
        }
            

        if(CurrentWeapon.Ammo != CurrentWeapon.MagazineSize)
        {
            CurrentWeapon.Reload();
        }

    }

}
