using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponTutorialBehaviour : MonoBehaviour
{
    private Image Image = null;
    public Sprite Sprite = null;
    private float _timer = 0;
    private float _maxTimer = 0;


    private void Awake()
    {
        Image = GetComponent<Image>();
    }
    private void Update()
    {
        if (Image.enabled)
        {
            TimerCheck(); 
        }
        SetImage();

    }

    private void SetImage()
    {
        if(Image.sprite != Sprite && Sprite)
            Image.sprite = Sprite;
    }

    private void TimerCheck()
    {
        _timer += Time.deltaTime;

        if (_timer >= _maxTimer)
        {
            Image.enabled = false;
            _timer = 0;
            Image.color = Color.clear;
            RoomManager.CanOpenDoors = true;
        }
    }

    public void EnableImage(Sprite sprite, float duration)
    {
        Image.enabled = true;
        Image.sprite = sprite;
        _maxTimer = duration;
        Image.color = Color.white;
        RoomManager.CanOpenDoors = false;
    }
}
