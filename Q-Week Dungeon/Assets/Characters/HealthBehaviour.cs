using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBehaviour : MonoBehaviour
{
    [SerializeField]
    private int _startHealth = 0;

    private int _currentHealth = 0;
    public int CurrentHealth { get { return _currentHealth; }}

    private void Awake()
    {
        _currentHealth = _startHealth;
    }
    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
    }

    public void Kill()
    {
        Invoke("Destroy", 0.05f);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
