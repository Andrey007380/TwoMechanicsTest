using System.Collections.Generic;
using UnityEngine;

public class CollisionAction : ObstacleCollision
{
    [SerializeField] private GameObject _bluehuman;

    public delegate void DestroyDelegate();
    public static event DestroyDelegate OnAllObjectsDestroyed;
    
    public delegate void AllObjectsDestroyedDelegate(GameObject InstantiatedGameobject);
    public static event AllObjectsDestroyedDelegate OnEmptyAvailableObject;
    

    [SerializeField]private List<GameObject> _listofavailableobjects = new List<GameObject>();

    public delegate void InstantiateDelegate(GameObject InstantiatedGameobject);
    public static event InstantiateDelegate OnInstantiate;
    
    

    private void Start()
    {
        foreach (var item in FindObjectsOfType<CollisionAction>())
        {
            _listofavailableobjects.Add(item.gameObject);
        }
    }
    
   protected override void MakeAction(Collider other)
    {
        GameObject instantiated = Instantiate(_bluehuman, gameObject.transform.position, Quaternion.identity);
        OnInstantiate?.Invoke(instantiated);

        OnEmptyAvailableObject?.Invoke(gameObject);
        
        if(_listofavailableobjects.Count == 0)
            OnAllObjectsDestroyed?.Invoke();
        
        Destroy(gameObject);
    }
    
    private void OnEnable() => OnEmptyAvailableObject += Remove;
    private void OnDisable() => OnEmptyAvailableObject -= Remove;
    private void Remove(GameObject gameObject) => _listofavailableobjects.Remove(gameObject);

    

}
