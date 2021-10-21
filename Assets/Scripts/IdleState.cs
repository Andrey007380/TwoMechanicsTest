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
        
        yield return new WaitForSeconds(1f);
        _baseAI.Animator.Play("Idle");
         Debug.Log("Idle");
        _baseAI.ApplyState(new AttackState(_baseAI));
    }
}