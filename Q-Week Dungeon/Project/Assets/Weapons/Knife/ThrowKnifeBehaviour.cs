using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowKnifeBehaviour : Bullet
{
    public TakeOverBehaviour TakeOverBehaviour = null;

    protected override void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 9)
            TakeOverBehaviour.TakeOver(other.gameObject);
        
        Invoke("Kill", 0.02f);
        TimeBehaviour.EndSlowMotion();
    }

    protected override void CalculatePath()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, transform.position + transform.forward, Time.deltaTime * _movementSpeed);
    }
}
