using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AttackState : State
{
    public AttackState(BaseAI baseAI) : base(baseAI) {}
    List<Transform> enemies;
    public override IEnumerator Attack()
    {
        yield return new WaitForSeconds(1f);

        _baseAI.MeshAgent.SetDestination(GetClosestEnemy().position);
        _baseAI.Animator.Play("Run");
        Debug.Log("Attack");

       
    }
   private Transform GetClosestEnemy()
    {
        

        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = _baseAI.transform.position;

        foreach (Collider collider in _baseAI.TargetInRadius)
        {
            enemies.Add(collider.transform);
            foreach (Transform potentialTarget in enemies)
            {
                Vector3 directionToTarget = potentialTarget.position - currentPosition;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if (dSqrToTarget < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToTarget;
                    bestTarget = potentialTarget;
                }
            }

        }
        Debug.Log(bestTarget.position);
        return bestTarget;
    }
}