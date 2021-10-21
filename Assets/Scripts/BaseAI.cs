using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BaseAI : MonoBehaviour
{
    private State _currentState;

    public NavMeshAgent MeshAgent;
    public Animator Animator;
    public float EnemyDetection;
    public LayerMask attackMask;

    [SerializeField] private Transform basePoint;
    [SerializeField] private Collider[] targetInRadius;

    public Transform BasePoint { get => basePoint; set => basePoint = value; }
    public Collider[] TargetInRadius { get => targetInRadius; set => targetInRadius = value; }

    private void Awake()
    {
        Animator = GetComponent<Animator>();
        MeshAgent = GetComponent<NavMeshAgent>();

    }

    private void Start() => ApplyState(new ChaseState(this));
    public void ApplyState(State state)
    {
            _currentState = state;
            StartCoroutine(_currentState.Chase());  
    }

    public void Attack() => StartCoroutine(_currentState.Attack());
    public void Idle()
    {
        if (!_currentState.Equals(null))
        {
            StartCoroutine(_currentState.Idle());
        }
        else
        {
             Debug.Log("Error");
        }
    }

    public void Chase() => StartCoroutine(_currentState.Chase());
    private void OnEnable()
    {
        CollisionAction.OnAllObjectsDestroyed += Attack;
        ChaseState.OnReachPosition += Idle;
    }

    private void OnDisable()
    {
        CollisionAction.OnAllObjectsDestroyed -= Attack;
        ChaseState.OnReachPosition += Idle;
    }
    
}
