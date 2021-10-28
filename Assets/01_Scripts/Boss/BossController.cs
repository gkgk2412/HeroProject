using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossController : Boss
{
    private Animator _bossAnimator;
    private NavMeshAgent _bossAgent;

    private bool isGround = false;
    public GameObject bossUI;

    private bool isOnce;

    // Start is called before the first frame update
    void Start()
    {
        _bossAnimator = this.GetComponent<Animator>();
        _bossAgent = this.GetComponent<NavMeshAgent>();

        _bossAgent.enabled = false;
        bossUI.SetActive(false);

        b_CurrentHp = b_hp;
    }

    // Update is called once per frame
    void Update()
    {
        //땅에 닿으면
        if(isGround)
        {
            //nav agent 작동
            _bossAgent.enabled = true;

            //약 1초 후에 보스 체력바 등 UI 띄우기
            Invoke("ShowUI", 1.0f);
        }

        //보스의 체력이 0이면
        if (b_CurrentHp <= 0)
        {
            _bossAnimator.SetBool("isDie", true);
        }
    }

    private void ShowUI()
    {
        bossUI.SetActive(true);

        isGround = false;
    }


    //subCamera.cs에서 event를 통해서 호출
    public void setGround()
    {
        isGround = true;
    }
}
