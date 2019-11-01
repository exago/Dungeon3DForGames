using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthBehaviour : MonoBehaviour
{
    private bool _isStealth = false;
    public bool IsStealth { get { return _isStealth; } set { _isStealth = value; } }

    private Renderer _playerRenderer = null;
    private Material _playerMaterial = null;

    [SerializeField]
    private float _alphaChangeSpeed = 0.5f;

    [SerializeField]
    private float _minAlpha = 0.2f;

    private void Awake()
    {
        _playerRenderer = GetComponentInChildren<Renderer>();
        _playerMaterial = _playerRenderer.material;
    }

    private void Update()
    {
        UpdateAlphaForStealth();
    }

    const float EPSILON = 0.1f;
    private void UpdateAlphaForStealth()
    {
        float currentAlphaState = _playerMaterial.color.a;

        if (currentAlphaState != 1 && !_isStealth)
        {
            currentAlphaState = Mathf.Lerp(currentAlphaState, 1, _alphaChangeSpeed * Time.deltaTime);
            
            if (currentAlphaState >= 1 - EPSILON)
                currentAlphaState = 1;
        }
        if (_isStealth && currentAlphaState > _minAlpha)
        {
            currentAlphaState = Mathf.Lerp(currentAlphaState, _minAlpha, _alphaChangeSpeed * Time.deltaTime);

            if (currentAlphaState <= _minAlpha + EPSILON)
                currentAlphaState = _minAlpha;
        }

        Color newColor = _playerMaterial.GetColor("_Color");
        newColor.a = currentAlphaState;
        _playerRenderer.material.SetColor("_Color", newColor);
    }

    public void ToggleStealth()
    {
        Debug.Log("ToggleStealth = " + _isStealth);
        _isStealth = !_isStealth;
    }
}
