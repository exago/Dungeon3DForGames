using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class WaveRifle : Weapon
{
    public override void Shoot()
    {
        base.Shoot();

        if (_canShoot)
        {
            int side = 1;
            for (int i = 0; i < 2; i++)
            {
                GameObject inst = Instantiate(_bulletPrefab, _fireOutputPositions[0].position, _fireOutputPositions[0].rotation);

                WaveBullet bulletScript = inst.GetComponent<WaveBullet>();
                bulletScript.Damage = _damage;
                bulletScript.MovementSpeed = _bulletSpeed;
                bulletScript.SideToStart = (Sides)side;

                side *= -1;
            }
        }
    }

}