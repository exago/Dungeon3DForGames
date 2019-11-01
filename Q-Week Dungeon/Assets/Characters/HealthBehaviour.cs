using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBehaviour : MonoBehaviour
{
    [SerializeField]
    private int _startHealth = 0;

    private int _currentHealth = 0;

    private void Awake()
    {
        _currentHealth = _startHealth;
    }
    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
            Invoke("Kill", 0.02f);
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}
