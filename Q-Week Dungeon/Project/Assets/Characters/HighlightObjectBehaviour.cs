using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightObjectBehaviour : MonoBehaviour
{
    private bool _highlight = false;
    public bool Highlight { get { return _highlight; } set { _highlight = value; } }

    [SerializeField] private bool _timedHighlight = false;
    [SerializeField] private float _timeOfHighlight = 0.0f;
    private float _highlightTimer = 0.0f;

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
            _previousMaterials.Add(_rendererArray[i].material);
        }
    }

    protected virtual void Update()
    {

        CheckIfStillHighlighted();
        HighlightObject();
        
    }

    private void CheckIfStillHighlighted()
    {
        if (_timedHighlight)
        {
            _highlightTimer += Time.deltaTime;

            if(_highlightTimer >= _timeOfHighlight)
            {
                _highlightTimer = 0.0f;
                _highlight = false;
            }
        }
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

}
