using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class ChaseState : State
{
    public ChaseState(BaseAI baseAI) : base(baseAI)
    {

    }

    public override IEnumerator Chase()
    {
        while (true)
        {
            _baseAI.MeshAgent.SetDestination(_baseAI.BasePoint.position);
            _baseAI.Animator.Play("Run");

            yield return null;

            if (!_baseAI.MeshAgent.pathPending)
            {
                if (_baseAI.MeshAgent.remainingDistance <= _baseAI.MeshAgent.stoppingDistance)
                {
                    if (!_baseAI.MeshAgent.hasPath || _baseAI.MeshAgent.velocity.sqrMagnitude == 0f)
                    {
                        if (_baseAI.MeshAgent.pathStatus == NavMeshPathStatus.PathComplete)
                        {                            
                            Debug.Log("Swith to Idle");
                            _baseAI.ApplyState(new IdleState(_baseAI));
                            _baseAI.Idle();
                            break;
                        }

                    }
                }
            }
        }
    }
}