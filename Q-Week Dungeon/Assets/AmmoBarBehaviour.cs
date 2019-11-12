using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBarBehaviour : MonoBehaviour
{
    private Player _player = null;
    private RectTransform _thisRectTrans = null;
    [SerializeField] private GameObject _ammoBarPrefab = null;

    private int _currentAmmo = 0;
    private bool _reloaded = false;
    private GameObject[] _ammoArray = new GameObject[0];

    private void Awake()
    {
        _player = FindObjectOfType<Player>().GetComponent<Player>();
        _thisRectTrans = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if(_player == null)
            FindObjectOfType<Player>().GetComponent<Player>();
        else
        {
            
            if (_currentAmmo == _player.ShootingBehaviour._currentWeapon.MagazineSize && !_reloaded)
                InstantiateAmmo();
            
            if(_currentAmmo > _player.ShootingBehaviour._currentWeapon.Ammo) 
            {
                Debug.Log(_currentAmmo);
                DeleteInstantion(_currentAmmo - 1);                
                _reloaded = false;
            }
            else
            {
                _reloaded = false;
            }                

            _currentAmmo = _player.ShootingBehaviour._currentWeapon.Ammo;
        }
    }

    private void DeleteInstantion(int index)
    {
        if(_ammoArray.Length!= 0)
            Destroy(_ammoArray[index].gameObject);
    }

    private void InstantiateAmmo()
    {
        if(_ammoArray.Length != 0)
        {
            for (int i = 0; i < _ammoArray.Length; i++)
            {
                DeleteInstantion(i);
            }
        }

        _ammoArray = new GameObject[_player.ShootingBehaviour._currentWeapon.MagazineSize];
        _currentAmmo = _player.ShootingBehaviour._currentWeapon.MagazineSize;
        _reloaded = true;

        float scale = 1.0f / _player.ShootingBehaviour._currentWeapon.MagazineSize;
        float currentScale = 1.0f;


        for (int i = 0; i < _ammoArray.Length; i++)
        {
            GameObject go = Instantiate(_ammoBarPrefab, this.transform);
            go.GetComponent<RectTransform>().localScale = new Vector3(currentScale, 1, 1);
            currentScale -= scale;
            _ammoArray[_currentAmmo - (i+1)] = go;
        }

    }
}
