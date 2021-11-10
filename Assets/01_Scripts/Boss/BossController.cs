using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossController : Boss
{
    private Animator _bossAnimator;
    private Rigidbody rb;

    public GameObject bossUI;

    private bool isGround = false;
    private bool isSkill2_do = false;

    private int randNum;

    #region Internal-------------------------- # Please close # --------------------------
    private enum StateFlow
    {
        ENTER,
        UPDATE,
        EXIT
    }

    private void SetStartState(BossState startState)
    {
        currentState = startState;

        switch (currentState)
        {
            case BossState.IDLE: IDLE(StateFlow.ENTER); break;
            case BossState.SKILL01_ROCK_THROW: SKILL01_ROCK_THROW(StateFlow.ENTER); break;
            case BossState.SKILL02_BODY_BlOW: SKILL02_BODY_BlOW(StateFlow.ENTER); break;
            case BossState.DIE: DIE(StateFlow.ENTER); break;
        }
    }

    private void CommonUpdate()
    {
        //보스의 체력이 0이면
        if (b_CurrentHp <= 0)
        {
            _bossAnimator.SetBool("isDie", true);
            ChangeState(BossState.DIE);
        }

        //땅에 닿으면
        if (isGround)
        {
            //nav agent 작동
            //_bossAgent.enabled = true;

            //약 1초 후에 보스 체력바 등 UI 띄우기
            Invoke("ShowUI", 1.0f);
        }
    }

    void Update()
    {
        CommonUpdate();

        switch (currentState)
        {
            case BossState.IDLE: IDLE(StateFlow.UPDATE); break;
            case BossState.SKILL01_ROCK_THROW: SKILL01_ROCK_THROW(StateFlow.UPDATE); break;
            case BossState.SKILL02_BODY_BlOW: SKILL02_BODY_BlOW(StateFlow.UPDATE); break;
            case BossState.DIE: DIE(StateFlow.UPDATE); break;
        }
    }

    private void ChangeState(BossState nextState)
    {
        switch (this.currentState)
        {
            case BossState.IDLE: IDLE(StateFlow.EXIT); break;
            case BossState.SKILL01_ROCK_THROW: SKILL01_ROCK_THROW(StateFlow.EXIT); break;
            case BossState.SKILL02_BODY_BlOW: SKILL02_BODY_BlOW(StateFlow.EXIT); break;
            case BossState.DIE: DIE(StateFlow.EXIT); break;
        }

        this.currentState = nextState;

        switch (this.currentState)
        {
            case BossState.IDLE: IDLE(StateFlow.ENTER); break;
            case BossState.SKILL01_ROCK_THROW: SKILL01_ROCK_THROW(StateFlow.ENTER); break;
            case BossState.SKILL02_BODY_BlOW: SKILL02_BODY_BlOW(StateFlow.ENTER); break;
            case BossState.DIE: DIE(StateFlow.ENTER); break;
        }
    }

    #endregion;

    // Start is called before the first frame update
    void Start()
    {
        _bossAnimator = this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody>();

        bossUI.SetActive(false);

        b_CurrentHp = b_hp;
    }

    private void IDLE(StateFlow stateFlow)
    {
        if (isGround)
        {
            switch (stateFlow)
            {
                case StateFlow.ENTER:
                    {
                        _bossAnimator.SetBool("isIdle", true);
                        _bossAnimator.SetBool("isThrow", false);
                        _bossAnimator.SetBool("isBodyBlow", false);
                        randNum = Random.RandomRange(0, 2); //0~2까지 중 1개 선택
                    }
                    break;

                case StateFlow.UPDATE:
                    {
                        Debug.Log("randNum  : " + randNum);
                        isSkill2_do = false;

                        if (isGround)
                            LookPlayer();

                        //5초 간격으로 스킬 3개 중 1개를 실행한다.
                        if (randNum == 0)
                        {
                            Invoke("InvokeSkill01", 5.0f);
                            randNum = 99;
                        }
                        else if (randNum == 1)
                        {
                            Invoke("InvokeSkill02", 5.0f);
                            randNum = 99;
                        }
                    }
                    break;

                case StateFlow.EXIT:
                    {
                    }
                    break;
            }
        }
    }
    private void SKILL01_ROCK_THROW(StateFlow stateFlow)
    {
        switch (stateFlow)
        {
            case StateFlow.ENTER:
                {
                    _bossAnimator.SetBool("isIdle", false);
                    _bossAnimator.SetBool("isThrow", true);

                    //스킬실행
                    BezierCurve.Instance.SkillRock();
                }
                break;

            case StateFlow.UPDATE:
                {
                    LookPlayer();

                    //idle로 복귀
                    ChangeState(BossState.IDLE);
                }
                break;

            case StateFlow.EXIT:
                {
                }
                break;
        }
    }

    private void SKILL02_BODY_BlOW(StateFlow stateFlow)
    {
        switch (stateFlow)
        {
            case StateFlow.ENTER:
                {
                }
                break;

            case StateFlow.UPDATE:
                {
                    //1초 동안 이펙트 등 띄우고 다 띄워지면 아래부분 실행하기
                    //보스가 현재 바라보고 있는 위치와 그 끝.. 직선으로.. 이펙트 표시
                    _bossAnimator.SetBool("isIdle", false);
                    _bossAnimator.SetBool("isBodyBlow", true);

                    if(!isSkill2_do)
                        Invoke("Skill02", 1.5f);

                    //직선으로 뛰기
                    if(isSkill2_do)
                    {
                        Vector3 movement = transform.forward.normalized * moveSpeed * Time.deltaTime;
                        rb.MovePosition(transform.position + movement);
                    }                    
                }
                break;

            case StateFlow.EXIT:
                {
                }
                break;
        }
    }

    private void DIE(StateFlow stateFlow)
    {
        switch (stateFlow)
        {
            case StateFlow.ENTER:
                {
                }
                break;

            case StateFlow.UPDATE:
                {
                    _CameraController.MyCameraBoss = false;
                }
                break;

            case StateFlow.EXIT:
                {
                }
                break;
        }
    }

    private void ShowUI()
    {
        bossUI.SetActive(true);
    }

    //subCamera.cs에서 event를 통해서 호출
    public void setGround()
    {
        isGround = true;
    }

    private void LookPlayer()
    {
        //계속 플레이어 방향 쪽으로 바라보기
        Vector3 dir = (Player.position - this.transform.position);

        var rot = Quaternion.LookRotation(dir);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rot, Time.deltaTime * rotSpeed);
    }

    private void Skill02()
    {
        isSkill2_do = true;
    }

    private void InvokeSkill01()
    {
        ChangeState(BossState.SKILL01_ROCK_THROW);
    }

    private void InvokeSkill02()
    {
        ChangeState(BossState.SKILL02_BODY_BlOW);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //스킬 2 시전하는 상태일 때 플레이어나 충돌벽에 부딪히면 멈춤
        if(isSkill2_do && collision.gameObject.tag == "StopBoss")
        {
            //idle로 복귀
            ChangeState(BossState.IDLE);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(isSkill2_do && other.gameObject.tag == "StopBoss")
        {
            //idle로 복귀
            ChangeState(BossState.IDLE);
        }
    }
}
