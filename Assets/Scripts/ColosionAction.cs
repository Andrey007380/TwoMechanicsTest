using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]

public class ColosionAction : MonoBehaviour
{
    [SerializeField] private GameObject _bluehuman;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ScaleKnife>())
        {
            Debug.Log("Game");
            MakeAction(other);
        }
    }
    virtual protected void MakeAction(Collider other)
    {
        Instantiate(_bluehuman, transform.position, Quaternion.identity);
        // TODO: Create instation of blue_enemies  
        Destroy(gameObject);
    }
}
