using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

    public class AttackState : State
    {
        private LayerMask _attackMask;
        private Collider[] _targetInRadius;
        private NavMeshAgent _meshAgent;
        private Animator _animator;
        private float _enemyDetection;
        private Transform _basePoint;
        private int _numColliders = 20;
        

        public AttackState(AIStateMachine baseAI) : base(baseAI)
        {
            _attackMask = baseAI.attackMask;
            _meshAgent = baseAI.meshAgent;
            _animator = baseAI.animator;
            _enemyDetection = baseAI.enemyDetection;
            _basePoint = baseAI.basePoint;
        }

        public override void Enter()
        {
            base.Enter();
            _meshAgent.stoppingDistance = 0;
            
        }

        public override void Execute()
        {
            base.Execute();
           
            
            _targetInRadius =  Physics.OverlapSphere(_meshAgent.transform.position, _enemyDetection, _attackMask);
                if (_targetInRadius.Length != 0){
                    _animator.Play("Run");
                        _meshAgent.SetDestination(GetNewTarget(_targetInRadius).position);
                }
                else
                {
                    _baseAI.ChangeState(new IdleState(_baseAI));
                }
                Transform GetNewTarget(Collider[] colliders)
                {
                    Transform bestTarget = null;
                    float closestDistanceSqr = Mathf.Infinity;
                    Vector3 currentPosition = _meshAgent.transform.position;

                    foreach (Collider potentialTarget in colliders)
                    {
                        Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
                        float dSqrToTarget = directionToTarget.sqrMagnitude;
                        if (dSqrToTarget < closestDistanceSqr)
                        {
                            closestDistanceSqr = dSqrToTarget;
                            bestTarget = potentialTarget.transform;
                        }
                    }
                    return bestTarget;
                }
        }

        public override void Exit()
        {
            base.Exit();
        }

         

    }