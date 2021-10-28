using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickmanPool : MonoBehaviour
{
    //Int = key
    Dictionary<int, Queue<GameObject>> poolDictionary = new Dictionary<int, Queue<GameObject>>();

    public Queue<GameObject> availableObjcts = new Queue<GameObject>();

    [System.Serializable]
    public class Pool
    {
        public int tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;

    public static StickmanPool Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

      

    }
    public void Start()
    {
          foreach (Pool pool in pools)
          {

              for (int i = 0; i < pool.size; i++)
              {
                  GameObject obj = Instantiate(pool.prefab);
                  obj.SetActive(true);
                  availableObjcts.Enqueue(obj);
              }
              poolDictionary.Add(pool.tag, availableObjcts);
          }
    
    }

    public GameObject GetFromPool(int tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag" + tag + "not exist");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;


        return objectToSpawn;
    }
    public void AddToPool(int tag, GameObject prefab)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag" + tag + "not exist");
            return;
        }

        prefab.SetActive(false);
        //Add to the pool
        availableObjcts.Enqueue(prefab);

    }

}
