using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    ObjectPooler objectPooler;

    #region Singleton

    public static ArrowSpawner Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion
    
    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }

    public void ArrowSpawn()
    {
        ObjectPooler.Instance.spawnFromPool("spear", transform.position, Quaternion.Euler(0.0f, -18.36f, -10f));
    }
}
