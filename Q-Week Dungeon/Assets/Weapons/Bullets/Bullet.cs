
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    protected float _movementSpeed = 0.0f;
    public float MovementSpeed { get { return _movementSpeed; } set { _movementSpeed = value; } }

    protected int _damage = 0;
    public int Damage { get { return _damage; } set { _damage = value; } }

    [SerializeField]
    protected float _lifeTime = 3.0f;

    protected virtual void Awake()
    {

        Invoke("Kill", _lifeTime);
    }

    private void Update()
    {
        CalculatePath();
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        HealthBehaviour health = other.gameObject.GetComponent<HealthBehaviour>();

        if(health != null)
        {
            health.TakeDamage(_damage);
        }
        else
        {
            Debug.Log(other.gameObject + "cannot take damage");
        }

        
        Invoke("Kill", 0.02f);
    }
    protected virtual void CalculatePath() 
    {
        
    }
    protected virtual void Kill()
    {
        Destroy(this.gameObject);
    }
}
