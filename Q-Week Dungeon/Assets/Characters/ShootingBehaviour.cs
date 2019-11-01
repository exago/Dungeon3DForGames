using UnityEngine;

public class ShootingBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject _primaryGunTemplate = null;

    [SerializeField]
    private Transform _primarySocket = null;

    public Weapon _currentWeapon = null;

    private void Awake()
    {
        //SpawnGuns
        if (_primaryGunTemplate && _primarySocket)
        {
            GameObject g = Instantiate(_primaryGunTemplate, _primarySocket);
            g.transform.localPosition = Vector3.zero;
            g.transform.localRotation = Quaternion.identity;
            _currentWeapon = g.GetComponent<Weapon>();
        }
    }

    public void Fire()
    {
        if (!_currentWeapon)
        {
            Debug.Log(this.gameObject + "doesn't have a gun");
        }

        if(_currentWeapon.Ammo!= 0)
            _currentWeapon.Shoot();
    }

    public void Reload()
    {
        if (!_currentWeapon)
        {
            return;
        }
            

        if(_currentWeapon.Ammo != _currentWeapon.MagazineSize)
        {
            _currentWeapon.Reload();
        }

    }
}
