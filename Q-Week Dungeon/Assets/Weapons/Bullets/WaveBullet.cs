using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveBullet : Bullet
{
    private Sides _sideToStart = Sides.left;
    public Sides SideToStart { get { return _sideToStart; } set { _sideToStart = value; } }
    [SerializeField]
    private float _amplitude = 20.0f;
    [SerializeField]
    private float _frequency = 1.0f;

    [SerializeField]
    private float _increaseAmplitudeOverTime = 0;

    private float _timer = 0.0f;
    private Vector3 pos = Vector3.zero;

    protected override void Awake() 
    {
        base.Awake();
        pos = this.transform.position;
        
    }

    const float MIN_AMPLITUDE = 0.5f;
    protected override void CalculatePath()
    {
        _timer += Time.deltaTime;

        if(_amplitude + _increaseAmplitudeOverTime > MIN_AMPLITUDE)
            _amplitude += _increaseAmplitudeOverTime * Time.deltaTime;

        pos += this.transform.forward * Time.deltaTime * _movementSpeed;        
        Vector3 sinusMovement = this.transform.right * Mathf.Sin(_timer * _frequency) * _amplitude;
        this.transform.position = sinusMovement * (int)_sideToStart + pos;
    }
}
