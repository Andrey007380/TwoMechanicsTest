using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

    public class ChaseState : State
    {
        private NavMeshAgent _meshAgent;
        private Animator _animator;

        public ChaseState(AIStateMachine baseAI) : base(baseAI)
        {
            _meshAgent = baseAI.meshAgent;
            _animator = baseAI.animator;
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Execute()
        {
            base.Execute();
                _meshAgent.SetDestination(_baseAI.basePoint.position);
                _animator.Play("Run");
                
                if (_meshAgent.pathPending)return;
                
                if (_meshAgent.remainingDistance > _meshAgent.stoppingDistance)return;
                
                if (_meshAgent.hasPath && _meshAgent.velocity.sqrMagnitude != 0f)return;
                
                if (_meshAgent.pathStatus != NavMeshPathStatus.PathComplete) return;
                
                    _baseAI.ChangeState(new IdleState(_baseAI));
        }

        public override void Exit()
        {
            base.Exit();
        }
    }