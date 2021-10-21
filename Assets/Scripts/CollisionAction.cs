using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SphereCollider))]

public class CollisionAction : MonoBehaviour
{
    [SerializeField] private GameObject _bluehuman;

    public delegate void DestroyDelegate();
    public static event DestroyDelegate OnAllObjectsDestroyed;

    public delegate void AllObjectsDestroyedDelegate(GameObject InstantiatedGameobject);
    public static event AllObjectsDestroyedDelegate OnEmptyAvailableObjecrts;

    [SerializeField] private List<GameObject> Listofavailableobjects = new List<GameObject>();

    public delegate void InstantiateDelegate(GameObject InstantiatedGameobject);
    public static event InstantiateDelegate OnInstantiate;

    private void Start()
    {
        foreach (var item in FindObjectsOfType<CollisionAction>())
        {
            Listofavailableobjects.Add(item.gameObject);
        }
    }

    virtual protected void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ScaleKnife>())
        {
            MakeAction(other);
        }
    }
    virtual protected void MakeAction(Collider other)
    {
        Destroy(gameObject);
        GameObject instantiated= Instantiate(_bluehuman, gameObject.transform.position, Quaternion.identity);

        OnInstantiate(instantiated);
        OnEmptyAvailableObjecrts(gameObject);

        if(Listofavailableobjects.Count < 20 || Listofavailableobjects.Count == 0) {  
        OnAllObjectsDestroyed();
        }
    }

    private void OnEnable() => OnEmptyAvailableObjecrts += Remove;
    private void OnDisable() => OnEmptyAvailableObjecrts -= Remove;

    private void Remove(GameObject gameObject)
    {
        Listofavailableobjects.Remove(gameObject);
    }

}
