using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class ChaseState : State
{
    public delegate void ReachPositionDelegate();
    public static event ReachPositionDelegate OnReachPosition;
    public ChaseState(BaseAI baseAI) : base(baseAI)
    {

    }
    public override IEnumerator Chase()
    {
        _baseAI.MeshAgent.SetDestination(_baseAI.BasePoint.position);
        _baseAI.Animator.Play("Run");
        Debug.Log(_baseAI.MeshAgent.pathPending);
        
      
        Debug.Log("Chase");

        if (_baseAI.MeshAgent.remainingDistance <= _baseAI.MeshAgent.stoppingDistance)
        {
            if (!_baseAI.MeshAgent.hasPath || _baseAI.MeshAgent.velocity.sqrMagnitude == 0f)
            {
                if (_baseAI.MeshAgent.pathStatus == NavMeshPathStatus.PathComplete)
                {
                    yield return new WaitForSeconds(1f);
                    Debug.Log("Swith to Idle");
                    _baseAI.ApplyState(new IdleState(_baseAI));
                    OnReachPosition();

                }

            }
        }

    }
}