using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterActableBehaviour : MonoBehaviour
{
    private bool _highlight = false;
    public bool Highlight { get { return _highlight; } set { _highlight = value; } }

    private Renderer[] _rendererArray = null;

    [SerializeField] private Material _highlightMaterial = null;

    private List<Material> _previousMaterials = new List<Material>();
    
    protected virtual void Awake()
    {
        _rendererArray = GetComponentsInChildren<Renderer>();
        GetMaterials();
    }

    private void GetMaterials()
    {
        for (int i = 0; i < _rendererArray.Length; i++)
        {
            _previousMaterials.Add( _rendererArray[i].material);
        }
    }

    protected virtual void Update()
    {
        HighlightObject();
    }

    private void HighlightObject()
    {
        if (_highlight)
        {
            for (int i = 0; i < _rendererArray.Length; i++)
            {

                _rendererArray[i].material = _highlightMaterial;

            }
        }
        else
        {
            for (int i = 0; i < _rendererArray.Length; i++)
            {

                _rendererArray[i].material = _previousMaterials[i];
            
            }

        }
    }

    public virtual void Interaction()
    {

    }
}
