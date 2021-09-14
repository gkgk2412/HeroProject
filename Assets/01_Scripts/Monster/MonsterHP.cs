using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHP : Monster
{
    [SerializeField] GameObject mHP_Prefab = null;
    List<Transform> m_MonsterList = new List<Transform>();
    List<GameObject> m_hpbarList = new List<GameObject>();

    private float maxHP = 100;
    private float speed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        mHP_Prefab.transform.GetChild(1).GetComponent<Image>().fillAmount = (float)PlayerControlManager.Instance.MyCurHP / (float)maxHP;
        GameObject[] t_objects = GameObject.FindGameObjectsWithTag("Monster");

        for(int i = 0; i < t_objects.Length; ++i)
        {
            m_MonsterList.Add(t_objects[i].transform);
            GameObject t_hpbar = Instantiate(mHP_Prefab, t_objects[i].transform.position, Quaternion.identity, transform);
            m_hpbarList.Add(t_hpbar);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i <m_MonsterList.Count;++i)
        {
            m_hpbarList[i].transform.position = m_MonsterList[i].position + new Vector3(0, 1.15f, 0);
        }

        HandleHP();
    }


    public void HandleHP()
    {
        mHP_Prefab.transform.GetChild(1).GetComponent<Image>().fillAmount = Mathf.Lerp(mHP_Prefab.transform.GetChild(1).GetComponent<Image>().fillAmount, hp / (float)maxHP, Time.deltaTime * speed);
    }
}
