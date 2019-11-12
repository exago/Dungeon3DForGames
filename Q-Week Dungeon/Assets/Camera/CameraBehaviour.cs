using System;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField]
    private Transform _playerPosition = null;
    private Vector3 _previousPlayerPosition = Vector3.zero;
    [SerializeField]
    private float _inertiaSpeed = 2.0f;

    private Vector3 offset = Vector3.zero;
    [SerializeField]
    private float _radius = 4.0f;
    [SerializeField]
    private float _reach = 2.0f;

    private Vector3 movement = Vector3.zero;

    private void Awake()
    {
        _playerPosition = FindObjectOfType<Player>().transform;
        _previousPlayerPosition = _playerPosition.position;
        offset = this.transform.position - _playerPosition.position ;
    }
    private void FixedUpdate()
    {
        if (_playerPosition != null)
        {
            movement = Vector3.zero;
            HandleMousePosition();
            UpdatePosition();
        }
    }
    private void HandleMousePosition()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 mousePosition = _playerPosition.position;
        RaycastHit hitInfo;

        if(Physics.Raycast(mouseRay, out hitInfo, Camera.main.farClipPlane, LayerMask.GetMask("Ground")))
        {
            mousePosition = hitInfo.point;
        }
        mousePosition.y = _playerPosition.position.y;
        Vector3 playerToMouse = mousePosition - _playerPosition.position;

        if(playerToMouse.magnitude >= _radius)
            movement += playerToMouse.normalized * playerToMouse.magnitude / _reach;
        
    }
    private void UpdatePosition()
    {      

        this.transform.position = Vector3.Lerp(this.transform.position, _playerPosition.position + offset + movement, _inertiaSpeed * Time.deltaTime);
        _previousPlayerPosition = _playerPosition.position;
        
    }

}
