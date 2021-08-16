using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class CHILD
    {
        public List <Vector3> childTrans;
    }

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

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    public CHILD _child;

    // Start is called before the first frame update
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach(Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            
            for(int i = 0; i < pool.size; ++i)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);        
        }
    }

    public GameObject spawnFromPool (string tag, Vector3 position, Quaternion rotation)
    {
        float fallSpeed = Random.Range(3, 7);

        if (!poolDictionary.ContainsKey(tag))
        {
            // Debug.LogWarning("Pool with tag " + tag + "doesn't excist.");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        objectToSpawn.GetComponent<Rigidbody>().drag = fallSpeed;

        IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();

        if(pooledObj != null)
        {
            pooledObj.OnObjectSpawn();
        }

        //spear 2
        if(objectToSpawn.name == "spear2(Clone)")
        {
            //0번째 자식과 1번째 자식을 초기화
            objectToSpawn.transform.GetChild(0).localPosition = _child.childTrans[0];
            objectToSpawn.transform.GetChild(1).localPosition = _child.childTrans[1];
        }

        //spear 3
        if (objectToSpawn.name == "spear3(Clone)")
        {
            objectToSpawn.transform.GetChild(0).localPosition = _child.childTrans[2];
            objectToSpawn.transform.GetChild(1).localPosition = _child.childTrans[3];
            objectToSpawn.transform.GetChild(2).localPosition = _child.childTrans[4];
        }

        //spear 4
        if (objectToSpawn.name == "spear4(Clone)")
        {
            objectToSpawn.transform.GetChild(0).localPosition = _child.childTrans[5];
            objectToSpawn.transform.GetChild(1).localPosition = _child.childTrans[6];
            objectToSpawn.transform.GetChild(2).localPosition = _child.childTrans[7];
            objectToSpawn.transform.GetChild(3).localPosition = _child.childTrans[8];
        }

        //spear 5
        if (objectToSpawn.name == "spear5(Clone)")
        {
            objectToSpawn.transform.GetChild(0).localPosition = _child.childTrans[9];
            objectToSpawn.transform.GetChild(1).localPosition = _child.childTrans[10];
            objectToSpawn.transform.GetChild(2).localPosition = _child.childTrans[11];
            objectToSpawn.transform.GetChild(3).localPosition = _child.childTrans[12];
            objectToSpawn.transform.GetChild(4).localPosition = _child.childTrans[13];
        }

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}
