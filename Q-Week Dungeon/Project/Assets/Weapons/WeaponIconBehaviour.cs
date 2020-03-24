using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponIconBehaviour : MonoBehaviour
{
    private Image _thisImage = null;
    private Player _player = null;

    private void Awake()
    {
        _player = FindObjectOfType<Player>().GetComponent<Player>();
        _thisImage = GetComponent<Image>();
    }

    private void Update()
    {
        if (_player == null)
            _player = FindObjectOfType<Player>().GetComponent<Player>();
        else
        {
            if(_player.ShootingBehaviour.CurrentWeapon != null)
                _thisImage.sprite = _player.ShootingBehaviour.CurrentWeapon.Icon;
        }
    }
}
