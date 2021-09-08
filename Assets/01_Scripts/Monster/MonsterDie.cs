using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDie : MonoBehaviour
{
    private static MonsterDie instance;
    public static MonsterDie Instance => instance;

    public MonsterDie()
    {
        //자기 자신에 대한 정보를 static 형태의 instacne 변수에 저장하여 외부에서 접근이 쉽도록 함
        instance = this;
    }

    //죽은몬스터 count 확인 용
    public Dictionary<string, int> DieMonsterDic = new Dictionary<string, int>()
    {
        {"mushroom", 0},
        {"radish", 0},
        {"crytal", 0}
    };

    public void UpdateDictionary(Dictionary<string, int> dic, string key, int newValue)
    {
        int val;

        if (dic.TryGetValue(key, out val))
        {
            dic[key] = val + newValue;
        }
    }


    public int GetDieMonsterCount(string type)
    {
        int count = 0;

        foreach (int Value in DieMonsterDic.Values)
        {
            count = DieMonsterDic[type];
        }

        return count;
    }
}
