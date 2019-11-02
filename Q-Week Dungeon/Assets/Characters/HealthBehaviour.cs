using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBehaviour : MonoBehaviour
{
    [SerializeField]
    private int _startHealth = 0;

    private int _currentHealth = 0;
    public int CurrentHealth { get { return _currentHealth; }}


    protected virtual void Awake()
    {
        _currentHealth = _startHealth;
    }

    protected virtual void DoOnDeath()
    {
        Debug.Log(this.gameObject.name + " is dead");
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
