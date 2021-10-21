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

        float scaleZ =- Vector3.Distance(_startPos.position, _endPos.position);
        float scaleX =  1f;
        float scaleY = 1f;

        _knife.position = _endPos.position;
        _knife.LookAt(_startPos);
        _knife.localScale = new Vector3(scaleX , scaleY, scaleZ);
    }
}
