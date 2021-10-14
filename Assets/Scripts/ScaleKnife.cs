using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleKnife : MonoBehaviour
{
    [SerializeField] private Transform _startPos;
    [SerializeField] private Transform _endPos;

    private Transform _knife;

    private void Start()
    {
        _knife = GetComponent<Transform>();
    }

    private void Update()
    {
        SpawnGround();
    }
    private void SpawnGround()
    {
        Vector3 centerPos = new Vector3(_endPos.position.x, _endPos.position.y, _endPos.position.z);

        float scaleZ = Vector3.Distance(_startPos.position, _endPos.position);
        float scaleX =  1f;
        float scaleY = 1f;

        _knife.transform.position = centerPos;
        _knife.transform.LookAt(_startPos);
        _knife.transform.localScale = new Vector3(scaleX , scaleY, -scaleZ);
    }
}
