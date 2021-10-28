using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace FSM
{
    public class ChaseState : State
    {
        private NavMeshAgent _meshAgent;
        private Animator _animator;

        public ChaseState(AIStateMachine baseAI) : base(baseAI)
        {
            _meshAgent = baseAI._meshAgent;
            _animator = baseAI._animator;
        }

        public override IEnumerator Execute()
        {
            while (true)
            {
                _meshAgent.SetDestination(_baseAI.BasePoint.position);
                _animator.Play("Run");

                yield return null;

                if (_meshAgent.pathPending)
                    continue;
                if (_meshAgent.remainingDistance > _meshAgent.stoppingDistance)
                    continue;
                if (_meshAgent.hasPath && _meshAgent.velocity.sqrMagnitude != 0f)
                    continue;
                if (_meshAgent.pathStatus != NavMeshPathStatus.PathComplete)
                    continue;
                _baseAI.ApplyState(new IdleState(_baseAI));
                break;
            }
        }
    }
}