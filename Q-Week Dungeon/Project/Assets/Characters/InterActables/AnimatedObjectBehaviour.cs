using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedObjectBehaviour : InterActableBehaviour
{
    protected AnimationControlBehaviour _animationControlBehaviour = null;

    protected override void Awake()
    {
        GetAnimator();
        base.Awake();
    }

    private void GetAnimator()
    {
        _animationControlBehaviour = GetComponent<AnimationControlBehaviour>();

        if(_animationControlBehaviour == null)
            Debug.Log(gameObject.name + " does not have a animator");
    }

    protected virtual void SetAnimation()
    { 

    }
}
