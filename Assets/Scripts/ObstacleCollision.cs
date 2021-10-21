using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCollision : CollisionAction
{
    protected override void MakeAction(Collider other)
    {
        Destroy(other.gameObject);
        //TODO: Make gameover restart
    }
}
