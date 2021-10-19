using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    ObjectPooler objectPooler;

    public Transform[] mushRoomPos;
    public Transform[] radishPos;
    public Transform[] crystalPos;

    //게임 시작하면 최초 몬스터를 한번 스폰한다.
    private bool isSpawnMonsterOnce = false;


    [HideInInspector] public bool isSpawnMushRoom = false;
    [HideInInspector] public bool isSpawnRadish = false;
    [HideInInspector] public bool isSpawnCrystal = false;

    #region Singleton

    public static MonsterSpawner Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    // Start is called before the first frame update
    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }

    private void Update()
    {
        if(!isSpawnMonsterOnce)
        {
            Monster_MushRoom_Spawn();
            Monster_Radish_Spawn();
            Monster_Crystal_Spawn();

            isSpawnMonsterOnce = true;
        }

        if ((float)MonsterDie.Instance.CopyDieMonsterCount("mushroom") % 3 == 0.0f && !isSpawnMushRoom) 
        {
            //mushroom의 상태 초기화 시킴
            MonsterDie.Instance.MyMushRoom = 0;

            //재스폰
            Invoke("Monster_MushRoom_Spawn", 5.0f);

            isSpawnMushRoom = true;
        }
        
        if ((float)MonsterDie.Instance.CopyDieMonsterCount("radish") % 3 == 0.0f && !isSpawnRadish) 
        {
            //Radish 상태 초기화 시킴
            MonsterDie.Instance.MyRadish = 0;

            //재스폰
            Invoke("Monster_Radish_Spawn", 5.0f);

            isSpawnRadish = true;
        }  
        
        if ((float)MonsterDie.Instance.CopyDieMonsterCount("crystal") % 3 == 0.0f && !isSpawnCrystal) 
        {
            //Crystal 상태 초기화 시킴
            MonsterDie.Instance.MyCrystal = 0;

            //재스폰
            Invoke("Monster_Crystal_Spawn", 5.0f);

            isSpawnCrystal = true;
        }
    }

    public bool MonsterRespawnCheck(string name)
    {
        if (name == "mushroom")
        {
            return isSpawnMushRoom = false;
        }

        else if (name == "radish")
        {
            return isSpawnRadish = false;
        }

        else if (name == "crystal")
        {
            return isSpawnCrystal = false;
        }

        else return true;
    }

    public void Monster_MushRoom_Spawn()
    {
        ObjectPooler.Instance.spawnFromPool("mushroom", mushRoomPos[0].position, this.transform.rotation);
        ObjectPooler.Instance.spawnFromPool("mushroom", mushRoomPos[1].position, this.transform.rotation);
        ObjectPooler.Instance.spawnFromPool("mushroom", mushRoomPos[2].position, this.transform.rotation);
    }

    public void Monster_Radish_Spawn()
    {
        ObjectPooler.Instance.spawnFromPool("radish", radishPos[0].position, this.transform.rotation);
        ObjectPooler.Instance.spawnFromPool("radish", radishPos[1].position, this.transform.rotation);
        ObjectPooler.Instance.spawnFromPool("radish", radishPos[2].position, this.transform.rotation);
    } 
    
    public void Monster_Crystal_Spawn()
    {
        ObjectPooler.Instance.spawnFromPool("crystal", crystalPos[0].position, this.transform.rotation);
        ObjectPooler.Instance.spawnFromPool("crystal", crystalPos[1].position, this.transform.rotation);
        ObjectPooler.Instance.spawnFromPool("crystal", crystalPos[2].position, this.transform.rotation);
    }
}
