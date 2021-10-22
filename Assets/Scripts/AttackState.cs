using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AttackState : State
{
    public AttackState(BaseAI baseAI) : base(baseAI) {}
    public override IEnumerator Attack()
    {
        while (true)
        {

        _baseAI.TargetInRadius = Physics.OverlapSphere(_baseAI.MeshAgent.transform.position, _baseAI.EnemyDetection, _baseAI.attackMask);

        yield return null /*WaitUntil(() => _baseAI.MeshAgent.remainingDistance <= _baseAI.MeshAgent.stoppingDistance)*/;
        _baseAI.MeshAgent.SetDestination(GetNewTarget(_baseAI.TargetInRadius).position);
        _baseAI.Animator.Play("Run");


        Transform GetNewTarget(Collider[] colliders)
        {
            Transform bestTarget = null;
            float closestDistanceSqr = Mathf.Infinity;
            Vector3 currentPosition = _baseAI.transform.position;

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