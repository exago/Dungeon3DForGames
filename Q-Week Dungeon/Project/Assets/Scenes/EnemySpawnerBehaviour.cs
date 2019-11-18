using System;
using UnityEngine;

public class EnemySpawnerBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemy = null;
    [SerializeField]
    private int _amountOfEnemies;

    [SerializeField]
    private float _radius = 5;

    [SerializeField]
    private bool _circleSpawn = false;


    private void Awake()
    {
        if (_circleSpawn)
            SpawnCircle();
        else
            SpawnPoint();
    }

    private void SpawnPoint()
    {
        Instantiate(_enemy, this.transform.position, _enemy.transform.rotation);
    }

    private void SpawnCircle()
    {
        for (int i = 0; i < _amountOfEnemies; i++)
        {
            Vector3 spawnPosition = SpawnPositionCircle(i, _amountOfEnemies);
            GameObject newEnemy = Instantiate(_enemy, spawnPosition, Quaternion.identity) ;
            newEnemy.GetComponent<MovementBehaviour>().DesiredLookAtPoint = this.transform.position;
        }

    }

    private Quaternion CalculateRotation(int i, float amount)
    {
        float angle = 360 / amount;
        angle *= i + 90;

        Quaternion rotation = Quaternion.Euler(0, angle, 0);
        Debug.Log(rotation.eulerAngles);

        return rotation;
    }

    private Vector3 SpawnPositionCircle(int i, float amount)
    {
        float angle = 360 / amount;
        angle *= i;

        float x = this.transform.position.x + _radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        float y = this.transform.position.y;
        float z = this.transform.position.z + _radius * Mathf.Cos(angle * Mathf.Deg2Rad);

        return new Vector3(x, y, z);
    }
}
