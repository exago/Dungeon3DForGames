using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicWeapon : Weapon
{
    public override void Shoot()
    {
        base.Shoot();

        if (_canShoot)
        {
            for (int i = 0; i < _fireOutputPositions.Count; i++)
            {
                GameObject inst = Instantiate(_bulletPrefab, _fireOutputPositions[i].position, _fireOutputPositions[i].rotation);

                Bullet bulletScript = inst.GetComponent<Bullet>();
                bulletScript.Damage = _damage;
                bulletScript.MovementSpeed = _bulletSpeed;
            }
        }
    }

    public override void Reload()
    {
        Debug.Log("Reload");
        base.Reload();
    }

}
