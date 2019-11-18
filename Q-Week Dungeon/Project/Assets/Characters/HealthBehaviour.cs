using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBehaviour : MonoBehaviour
{
    [SerializeField]
    private int _startHealth = 0;
    public int StartHealth { get { return _startHealth; } }

    private int _currentHealth = 0;
    public int CurrentHealth { get { return _currentHealth; }}

    [SerializeField]
    private int _maxHealth = 3;
    public int MaxHealth { get { return _maxHealth; } set { _maxHealth = value; } }


    protected virtual void Awake()
    {
        _currentHealth = _startHealth;
    }

    public void TakeDamage(int damage)
    {
        Debug.Log(this.gameObject.name + " took damage: " + damage);
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
