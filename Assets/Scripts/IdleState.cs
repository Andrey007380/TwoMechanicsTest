using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class IdleState : State
{
    public IdleState(BaseAI baseAI) : base(baseAI)
    {

    }

    public override IEnumerator Idle()
    {
        while (true)
        {
        _baseAI.Animator.Play("Idle");
        yield return new WaitUntil(() => _baseAI.MeshAgent.remainingDistance <= _baseAI.MeshAgent.stoppingDistance);
        _baseAI.ApplyState(new AttackState(_baseAI));
         break;
        }
        
    }
}