
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

    private bool _canDamage = true;

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
        HighlightObjectBehaviour highlight = other.GetComponent<HighlightObjectBehaviour>();

        if(health != null && _canDamage)
        {
            health.TakeDamage(_damage);

            if (highlight != null) highlight.Highlight = true;
        }
        else
        {
            Debug.Log(other.gameObject + "cannot take damage");
        }

        _canDamage = false;
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
