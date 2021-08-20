using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    ObjectPooler objectPooler;
    public Transform Player = null;

    public float x,y,z;

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
        ObjectPooler.Instance.spawnFromPool("spear", transform.position, Quaternion.Euler(x, Player.rotation.eulerAngles.y + y, z));
    }

    public void Arrow2Spawn()
    {
        ObjectPooler.Instance.spawnFromPool("spear2", transform.position, Quaternion.Euler(x, Player.rotation.eulerAngles.y + y, z));
    }

    public void Arrow3Spawn()
    {
        ObjectPooler.Instance.spawnFromPool("spear3", transform.position, Quaternion.Euler(x, Player.rotation.eulerAngles.y + y, z));
    }

    public void Arrow4Spawn()
    {
        ObjectPooler.Instance.spawnFromPool("spear4", transform.position, Quaternion.Euler(x, Player.rotation.eulerAngles.y + y, z));
    }

    public void Arrow5Spawn()
    {
        ObjectPooler.Instance.spawnFromPool("spear5", transform.position, Quaternion.Euler(x, Player.rotation.eulerAngles.y + y, z));
    }

    public void DestroyArrow()
    {
        //가용 숫자가 2면 arrow 1을 모두 destroy
        if (PlayerControlManager.Instance.MyArrow == 2)
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag("spear");
            for (int i = 0; i < objects.Length; i++)
                Destroy(objects[i]);
        }

        //가용 숫자가 3면 arrow 2을 모두 destroy
        if (PlayerControlManager.Instance.MyArrow == 3)
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag("spear2");
            for (int i = 0; i < objects.Length; i++)
                Destroy(objects[i]);
        }

        //가용 숫자가 4면 arrow 3을 모두 destroy
        if (PlayerControlManager.Instance.MyArrow == 4)
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag("spear3");
            for (int i = 0; i < objects.Length; i++)
                Destroy(objects[i]);
        }

        //가용 숫자가 5면 arrow 4을 모두 destroy
        if (PlayerControlManager.Instance.MyArrow == 5)
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag("spear4");
            for (int i = 0; i < objects.Length; i++)
                Destroy(objects[i]);
        }
    }
}
