using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Rigidbody))]
    public class AIStateMachine : MonoBehaviour
    {
        private State _currentState;

        public NavMeshAgent meshAgent;
        public Animator animator;
        public ParticleSystem particleSystem;
        public LayerMask attackMask;
        public float enemyDetection = 50f;
        public Transform basePoint;
        [HideInInspector]public bool isActive = false;
        

        private void Awake()
        {
            
            particleSystem = GetComponent<ParticleSystem>();
            animator = GetComponent<Animator>();
            meshAgent = GetComponent<NavMeshAgent>();
        }

        public void Start() => ChangeState(new ChaseState(this));
        public void Update() => _currentState?.Execute();

        
        public void ChangeState(State state)
        {
            _currentState?.Exit();
            _currentState = state;
            _currentState?.Enter();
        }
        
        private void Attack() => isActive = true;   
   
        private void OnEnable() => CollisionAction.OnAllObjectsDestroyed += Attack;

        private void OnDisable() => CollisionAction.OnAllObjectsDestroyed -= Attack;
        
    }
