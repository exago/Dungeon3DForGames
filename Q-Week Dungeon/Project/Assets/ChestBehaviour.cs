using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestBehaviour : InterActableBehaviour
{
    private bool _isChestOpen = false;
    public bool IsChestOpen { get { return _isChestOpen; } set { _isChestOpen = value; } }

    [SerializeField] private AudioClip _chestSound = null;

    [SerializeField] private GameObject _prefabKnife = null;
    [SerializeField] private Collider _chestRoom = null;

    [SerializeField] private WeaponTutorialBehaviour _weaponTutorial = null;
    [SerializeField] private Sprite _knifeUISprite = null;

    protected override void Awake()
    {

        base.Awake();
    }
    protected override void Update()
    {
        if (!IsChestOpen && RoomManager.CurrentRoom == _chestRoom)
            RoomManager.CanOpenDoors = false;

        base.Update();
    }

    public override void Interaction()
    {
        if (!_isChestOpen)
        {
            Debug.Log("Open");
            OpenChest();

            FindObjectOfType<Player>().ShootingBehaviour.CurrentWeapon = _prefabKnife.GetComponent<Knife>();
            FindObjectOfType<Player>().ShootingBehaviour.SpawnGuns(_prefabKnife);
            RoomManager.CanOpenDoors = true;
            _weaponTutorial.EnableImage(_knifeUISprite, 10);
        }
            
    }

    private void OpenChest()
    {
        _isChestOpen = true;
        AudioManager.Instance.PlayAudioClip(_chestSound);
    }
}
