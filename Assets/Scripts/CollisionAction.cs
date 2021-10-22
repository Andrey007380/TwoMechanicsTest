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

    private List<GameObject> _listofavailableobjects = new List<GameObject>();

    public delegate void InstantiateDelegate(GameObject InstantiatedGameobject);
    public static event InstantiateDelegate OnInstantiate;

    private void Start()
    {
        foreach (var item in FindObjectsOfType<CollisionAction>())
        {
            _listofavailableobjects.Add(item.gameObject);
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
        GameObject instantiated = Instantiate(_bluehuman, gameObject.transform.position, Quaternion.identity);
        OnInstantiate(instantiated);

        Destroy(gameObject);
        OnEmptyAvailableObjecrts(gameObject);

        if (_listofavailableobjects.Count < 20)
        {
            OnAllObjectsDestroyed();
        }

    }

    private void OnEnable() => OnEmptyAvailableObjecrts += Remove;
    private void OnDisable() => OnEmptyAvailableObjecrts -= Remove;

    private void Remove(GameObject gameObject) => _listofavailableobjects.Remove(gameObject);

}
