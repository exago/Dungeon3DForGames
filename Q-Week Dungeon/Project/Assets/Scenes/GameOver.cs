using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    private Player _player;
    [SerializeField]
    private Collider _spawnRoom;

    private void Awake()
    {
        _player = GameObject.FindObjectOfType<Player>();
        RoomManager.CurrentRoom = _spawnRoom;
        RoomManager.SetEnemyCounterAsync();

    }

    private void Update()
    {
        if (_player == null)
            TriggerGameOver();
    }

    private void TriggerGameOver()
    {
        SceneManager.LoadScene(0);
    }
}
