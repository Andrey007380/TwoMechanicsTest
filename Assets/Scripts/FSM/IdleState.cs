using System.Collections;
using UnityEngine;

    public class IdleState : State
    {
        private Animator _animator;

        public IdleState(AIStateMachine baseAI) : base(baseAI)
        {
            _animator = baseAI.animator;
        }
        
        public override void Enter()
        {
            base.Enter();
        }

        public override void Execute()
        {
            base.Execute();
            _animator.Play("Idle");
            if (_baseAI.isActive == true)
            {
                _baseAI.ChangeState(new AttackState(_baseAI));
               
            }
        }
        public override void Exit()
        {
            base.Exit();
            
        }
    }
