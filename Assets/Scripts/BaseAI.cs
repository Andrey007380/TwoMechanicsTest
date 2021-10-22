using System;
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
    public ParticleSystem ParticleSystem;
    public float EnemyDetection;
    public LayerMask attackMask;

    [SerializeField] private Transform basePoint;
    [SerializeField] private Collider[] targetInRadius;

    public Transform BasePoint { get => basePoint; set => basePoint = value; }
    public Collider[] TargetInRadius { get => targetInRadius; set => targetInRadius = value; }

    private void Awake()
    {
        ParticleSystem = GetComponent<ParticleSystem>();
        Animator = GetComponent<Animator>();
        MeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start() => ApplyState(new ChaseState(this));
    public void ApplyState(State state)
    {
        _currentState = state;
        StartCoroutine(_currentState.Chase());  
    }
    public void Chase() => StartCoroutine(_currentState.Chase());
    public void Idle() => StartCoroutine(_currentState.Idle());
    public void Attack() => StartCoroutine(_currentState.Attack());
   
    private void OnEnable()
    {
        CollisionAction.OnAllObjectsDestroyed += Attack;
    }

    private void OnDisable()
    {
        CollisionAction.OnAllObjectsDestroyed -= Attack;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == attackMask)
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            RandomPrticle(other.GetComponent<BaseAI>().ParticleSystem, ParticleSystem);

        }
    }

    private void RandomPrticle(ParticleSystem particleSystem1, ParticleSystem particleSystem2)
    {
        UnityEngine.Random.Range(1, 2);
    }
}
