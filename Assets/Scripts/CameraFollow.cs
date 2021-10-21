using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public List<GameObject> Targets;
    public Vector3 _offset;

    private float _minOffset = 30f;
    private float _maxOffset = 20f;


    private void LateUpdate()
    {
        if (Targets.Count > 0)
        {
            Zoom();
            Move();
        }
    }
    private void Zoom()
    {
        float zoom = Mathf.Lerp(_maxOffset, _minOffset, GetGreatestDistance());
        _offset.y = Mathf.Lerp(_offset.y, zoom, Time.deltaTime);
    }
    private void Move()
    {
        Vector3 newCenterPositionWithOffset = _offset + CenterPoint();

        transform.position = Vector3.Lerp(transform.position, newCenterPositionWithOffset, Time.deltaTime);
    }
    private Vector3 CenterPoint()
    {
        if (Targets.Count == 1)
        {
            return Targets[0].transform.position;
        }

        var bounds = new Bounds(Targets[0].transform.position, Vector3.zero);
        for(int i = 0; i< Targets.Count; i++)
        {
          
            bounds.Encapsulate(Targets[i].transform.position);
        }
        return bounds.center;
    }

    private float GetGreatestDistance()
    {
        var bounds = new Bounds(Targets[0].transform.position, Vector3.zero);

        for (int i = 0; i < Targets.Count; i++)
        {
            bounds.Encapsulate(Targets[i].transform.position);
        }
        return bounds.size.z;
    }

    private void OnEnable()
    {
        CollisionAction.OnInstantiate += Targets.Add;
    }

    private void OnDisable()
    {
        CollisionAction.OnInstantiate += Targets.Add;
    }
}
