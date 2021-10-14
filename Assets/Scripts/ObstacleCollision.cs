using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCollision : ColosionAction
{
    protected override void MakeAction(Collider other)
    {
        Destroy(other.gameObject);
        //TODO: Make gameover restart
    }
}
