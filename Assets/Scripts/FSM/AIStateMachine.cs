using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace FSM
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Rigidbody))]
    public class AIStateMachine : MonoBehaviour
    {
        private State _currentState;

        public NavMeshAgent _meshAgent;
        public Animator _animator;
        public ParticleSystem _particleSystem;
        public LayerMask _attackMask;
        public float _enemyDetection = 50f;

        [SerializeField] private Transform basePoint;
        [SerializeField] private Collider[] targetInRadius;

        public Transform BasePoint { get => basePoint; set => basePoint = value; }
        public Collider[] TargetInRadius { get => targetInRadius; set => targetInRadius = value; }


        private void Awake()
        {
            _particleSystem = GetComponent<ParticleSystem>();
            _animator = GetComponent<Animator>();
            _meshAgent = GetComponent<NavMeshAgent>();
        }

        private void Start() => ApplyState(new ChaseState(this));

        public void ApplyState(State state)
        {
            
            _currentState = state;
            StartCoroutine(_currentState?.Execute());  
        }

        private void Attack() => ApplyState(new AttackState(this));   
   
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
            
            if ((_attackMask & (1 << other.gameObject.layer)) == 0) return;
            
            if (_particleSystem.isPlaying.Equals(true)) return;
            _particleSystem.Play();

            Destroy(other.gameObject, 1);
            Destroy(gameObject, 1);
        }
    }
}
