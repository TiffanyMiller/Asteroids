using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Object Pool", menuName = "Scriptable Objects/New Object Pool")]
public class ObjectPool : ScriptableObject
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;
    private Dictionary<string, Queue<GameObject>> _poolDictionary;

    public void SetupPool()
    {
        _poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (var pool in pools)
        {
            var objectPool = new Queue<GameObject>();

            for (var i = 0; i < pool.size; i++)
            {
                var obj = Instantiate(pool.prefab);
                obj.name += i;
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            
            _poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector2 pos, Vector3 rot)
    {
        if (!_poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }
        
        var objectToSpawn = _poolDictionary[tag].Dequeue();
        
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = pos;
        objectToSpawn.transform.localEulerAngles = rot;
        
        var pooledObject = objectToSpawn.GetComponent<IPooledObject>();

        pooledObject?.OnObjectSpawn();

        _poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}
