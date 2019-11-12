using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public static class RoomManager
{
    private static Text _enemyCounterText = null;

    private static Collider _currentRoom = null;
    public static Collider CurrentRoom { get { return _currentRoom; } set { _currentRoom = value; } }

    private static bool _canOpenDoors = true;
    public static bool CanOpenDoors { get { return _canOpenDoors; } set { _canOpenDoors = value; } }

    public static async Task SetEnemyCounterAsync()
    {
        await Task.Delay(500);

        if (_enemyCounterText == null)
            _enemyCounterText = GameObject.FindGameObjectWithTag("EnemyCounter").GetComponent<Text>();

        _enemyCounterText.text = $"{CalculateEnemies().ToString()}x";
    }

    private static int CalculateEnemies()
    {
        Collider[] enemies = Physics.OverlapBox(_currentRoom.transform.position, _currentRoom.bounds.extents, _currentRoom.transform.rotation, 1<<9);

        if (enemies.Length == 0)
            _canOpenDoors = true;
        else
            _canOpenDoors = false;

        return enemies.Length;
    }
}
