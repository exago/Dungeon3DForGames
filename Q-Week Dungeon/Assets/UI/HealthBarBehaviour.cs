using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HealthBarBehaviour : MonoBehaviour
{
    private Player _player = null;
    private RectTransform _thisRectTransform = null;

    private float _previousHealthScale = 1.0f;
    private float _currentHealthScale = 1.0f;

    [SerializeField] private float _healthChangeSpeed = 2f;

    private void Awake()
    {
        _thisRectTransform = GetComponent<RectTransform>();
        _player = FindObjectOfType<Player>().GetComponent<Player>();
    }

    private void Update()
    {
        if(_player == null)
            _player = FindObjectOfType<Player>().GetComponent<Player>();
        else
        {
            _currentHealthScale = Mathf.Lerp(_previousHealthScale, (float)_player.HealthBehaviour.CurrentHealth / (float)_player.HealthBehaviour.StartHealth, _healthChangeSpeed * Time.deltaTime);
            _thisRectTransform.localScale = new Vector3(_currentHealthScale, 1, 1);
            _previousHealthScale = _currentHealthScale;
        }
       
    }
}
