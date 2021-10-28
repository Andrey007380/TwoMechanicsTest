using UnityEngine;

public class ObstacleCollision : MonoBehaviour
{
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<ScaleKnife>()) return;
        MakeAction(other);
    }
    
    protected virtual void MakeAction(Collider other)
    {
        Destroy(other.gameObject);
        
        Application.LoadLevel(Application.loadedLevel);
    }
}
