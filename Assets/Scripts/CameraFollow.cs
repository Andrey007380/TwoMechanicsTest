using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private List<GameObject> _targets;
    private Vector3 _cameraOffset;
    private Transform _cameraRotation;

    private float _minOffset = 40f;
    private float _maxOffset = 10f;
    private float _xAngle = 90f;
    private bool _isActive = false;

    private void Start()
    {
        _cameraRotation = transform;
    }

    private void LateUpdate()
    {
        if (_targets.Count <= 0 || _isActive != true) return;
        Zoom();
        Move();
    }
    private void Zoom()
    {
        float zoom = Mathf.Lerp(_maxOffset, _minOffset, GetGreatestDistance());
        _cameraOffset.y = Mathf.Lerp(_cameraOffset.y, zoom, Time.deltaTime);
    }
    private void Move()
    {
        Vector3 newCenterPositionWithOffset = _cameraOffset + CenterPoint();
        transform.position = Vector3.Lerp(transform.position, newCenterPositionWithOffset, Time.deltaTime);
        _cameraRotation.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(_xAngle,0,0) , Time.deltaTime);
    }
    private Vector3 CenterPoint()
    {
        if (_targets.Count == 1)
        {
            return _targets[0].transform.position;
        }

        var bounds = new Bounds(_targets[0].transform.position, Vector3.zero);
        for(int i = 0; i< _targets.Count; i++)
        {
          
            bounds.Encapsulate(_targets[i].transform.position);
        }
        return bounds.center;
    }

    private float GetGreatestDistance()
    {
        
        for (var index = 0; index < _targets.Count; index++)
        {
            var target = _targets[index];
            if (target.Equals(null))
                _targets.Remove(target);
        }
        var bounds = new Bounds(_targets[0].transform.position, Vector3.zero);
        foreach (var target in _targets.ToArray())
        {
            bounds.Encapsulate(target.transform.position);
        }

        return bounds.size.z;
    }
    
    private void ActiveSelf()
    {
        _isActive = true;
    }
    
    private void OnEnable()
    {
        CollisionAction.OnAllObjectsDestroyed += ActiveSelf;
        CollisionAction.OnInstantiate += _targets.Add;
    }

    private void OnDisable()
    {
        CollisionAction.OnAllObjectsDestroyed -= ActiveSelf;
        CollisionAction.OnInstantiate -= _targets.Add;
    }
    
}
