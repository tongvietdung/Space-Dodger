using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    // Define a custom class Pool which store essential attribute
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    #region Singleton
    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    // Make a list of pools of GameObject if there are many pools. Each pool in list store 1 type of GameObject 
    public List<Pool> pools;

    // Make a Dictionary which key is the tag and Queue is a queue of GameObject
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    // Start is called before the first frame update
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation, Vector2 direction, ParticleSystem particle)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + "doesn't exist!");
            return null;
        }

        // Get the object out of the queue 
        GameObject objToSpawn = poolDictionary[tag].Dequeue();

        // Check if object is reused. If not reused then not play particle
        bool wasActive = objToSpawn.activeSelf;
        if (wasActive)
        {
            PlayParticle(objToSpawn, particle);
        }
         
        // Set object to active and set the position and rotation
        objToSpawn.SetActive(true);
        objToSpawn.transform.position = position;
        objToSpawn.transform.rotation = rotation;

        // When object is spawned
        IPooledObject pooledObj = objToSpawn.GetComponent<IPooledObject>();
        if (pooledObj != null)
        {
            pooledObj.OnObjectSpawn(direction);
        }

        poolDictionary[tag].Enqueue(objToSpawn);
        return objToSpawn;
    }

    void PlayParticle(GameObject objToSpawn, ParticleSystem particle)
    {
        particle.transform.position = objToSpawn.transform.position;
        particle.Emit(50);
    }
}
