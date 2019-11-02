using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControlBehaviour : MonoBehaviour
{
    private Animator _animator = null;

    private bool _isOpen = false;
    public bool IsOpen { get { return _isOpen; } set { _isOpen = value; _animator.SetBool("isOpen", _isOpen); } }

    private bool _isExploding = false;
    public bool IsExploding { get { return _isExploding; } set { _isExploding = value; _animator.SetBool("isExploding", _isExploding); } }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
}
