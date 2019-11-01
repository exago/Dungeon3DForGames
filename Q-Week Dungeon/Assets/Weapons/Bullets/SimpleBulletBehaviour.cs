using UnityEngine;

public class SimpleBulletBehaviour : Bullet
{
    protected override void CalculatePath()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, transform.position + transform.forward, Time.deltaTime * _movementSpeed);
    }
}
