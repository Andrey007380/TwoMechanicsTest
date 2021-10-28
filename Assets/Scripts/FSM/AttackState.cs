using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace FSM
{
    public class AttackState : State
    {
        private LayerMask _attackMask;
        private Collider[] _targetInRadius;
        private NavMeshAgent _meshAgent;
        private Animator _animator;
        private float _enemyDetection;
        private Transform _basePoint;

        public AttackState(AIStateMachine baseAI) : base(baseAI)
        {
            _attackMask = baseAI._attackMask;
            _targetInRadius = baseAI.TargetInRadius;
            _meshAgent = baseAI._meshAgent;
            _animator = baseAI._animator;
            _enemyDetection = baseAI._enemyDetection;
            _basePoint = baseAI.BasePoint;
        }

        public override IEnumerator Execute()
        {
            yield return null;
            while (true)
            {
                _targetInRadius = Physics.OverlapSphere(_meshAgent.transform.position, _enemyDetection, _attackMask);
                if (_targetInRadius.Length != 0){
                    _animator.Play("Run");
                    _meshAgent.SetDestination(GetNewTarget(_targetInRadius).position);
                }
                else
                {
                    _meshAgent.SetDestination(_basePoint.position);
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
                break;
            }
        }
    
    }
}