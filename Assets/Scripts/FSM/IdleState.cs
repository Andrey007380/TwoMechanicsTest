using System.Collections;
using UnityEngine;

namespace FSM
{
    public class IdleState : State
    {
        private Animator _animator;
        public IdleState(AIStateMachine baseAI) : base(baseAI)
        {
            _animator = baseAI._animator;
        }

        public override IEnumerator Execute()
        {
            _animator.Play("Idle");
            yield return new WaitForSeconds(2f);

        }
    }
}