using UnityEngine;

public class EnemySpawnerBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _enemiesArray;

    [SerializeField]
    private float _radius = 5;
    private float _offset = 0;

    private void Awake()
    {
        SpawnCircle(0 ,5 );
        SpawnCircle(0, 5);
        SpawnPoint(0);
    }

    private void SpawnPoint(int index)
    {
        Instantiate(_enemiesArray[index], this.transform.position, _enemiesArray[index].transform.rotation);
    }

    private void SpawnCircle(int index, float amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Instantiate(_enemiesArray[index], SpawnPositionCircle(i, amount), _enemiesArray[index].transform.rotation);
        }

        _offset += Mathf.PI / 5;

    }

    private Vector3 SpawnPositionCircle(int i, float amount)
    {
        float angle = 360 / amount;
        angle *= i;
        angle += _offset;

        float x = this.transform.position.x + _radius * Mathf.Sin(angle * Mathf.Deg2Rad);
        float y = this.transform.position.y;
        float z = this.transform.position.z + _radius * Mathf.Cos(angle * Mathf.Deg2Rad);

        return new Vector3(x, y, z);
    }
}
