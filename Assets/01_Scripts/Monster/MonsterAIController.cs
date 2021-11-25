using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MonsterAIController : Monster
{
    NavMeshAgent agent;
    Animator Mon_animator;

    public AudioSource audiosource;
    public AudioSource audiosource2;
    public AudioClip audioClip;
    public AudioClip audioClip2;

    private bool isCheckAttackDamage = false;
    private bool once = false;

    #region Internal-------------------------- # Please close # --------------------------
    private enum StateFlow
    {
        ENTER,
        UPDATE,
        EXIT
    }

    private void SetStartState(MonsterState startState)
    {
        currentState = startState;

        switch (currentState)
        {
            case MonsterState.IDLE: IDLE(StateFlow.ENTER); break;
            case MonsterState.WALK: WALK(StateFlow.ENTER); break;
            case MonsterState.TRACE: TRACE(StateFlow.ENTER); break;
            case MonsterState.ATTACK: ATTACK(StateFlow.ENTER); break;
            case MonsterState.DIE: DIE(StateFlow.ENTER); break;
        }
    }

    void Update()
    {
        CommonUpdate();

        switch (currentState)
        {
            case MonsterState.IDLE: IDLE(StateFlow.UPDATE); break;
            case MonsterState.WALK: WALK(StateFlow.UPDATE); break;
            case MonsterState.TRACE: TRACE(StateFlow.UPDATE); break;
            case MonsterState.ATTACK: ATTACK(StateFlow.UPDATE); break;
            case MonsterState.DIE: DIE(StateFlow.UPDATE); break;
        }
    }

    private void ChangeState(MonsterState nextState)
    {
        switch (this.currentState)
        {
            case MonsterState.IDLE: IDLE(StateFlow.EXIT); break;
            case MonsterState.WALK: WALK(StateFlow.EXIT); break;
            case MonsterState.TRACE: TRACE(StateFlow.EXIT); break;
            case MonsterState.ATTACK: ATTACK(StateFlow.EXIT); break;
            case MonsterState.DIE: DIE(StateFlow.EXIT); break;
        }

        this.currentState = nextState;

        switch (this.currentState)
        {
            case MonsterState.IDLE: IDLE(StateFlow.ENTER); break;
            case MonsterState.WALK: WALK(StateFlow.ENTER); break;
            case MonsterState.TRACE: TRACE(StateFlow.ENTER); break;
            case MonsterState.ATTACK: ATTACK(StateFlow.ENTER); break;
            case MonsterState.DIE: DIE(StateFlow.ENTER); break;
        }
    }

    #endregion;

    public void Start()
    {
        HpFill.fillAmount = 1.0f;

        currentHp = hp;

        Player = GameObject.Find("Player").transform;
        _arrow = GameObject.Find("ArrowManager").GetComponent<ArrowValue>();
        agent = this.GetComponent<NavMeshAgent>();
        Mon_animator = this.GetComponent<Animator>();

        //nav agent 끄고 생성
        agent.enabled = false;

        //원래있던위치를 저장함.
        oriPos = this.gameObject.transform.position;
    }

    private void CommonUpdate()
    { 
        //체력이 0보다 작거나 같아 질 경우 DIE 상태로 전환
        if(currentHp <= 0)
            ChangeState(MonsterState.DIE);


        if ((float)MonsterDie.Instance.CopyDieMonsterCount("mushroom") % 3 == 0.0f && !MonsterSpawner.Instance.isSpawnMushRoom)
        {
            Invoke("InitMonster_MushRoom", 5.0f);
        }

        if ((float)MonsterDie.Instance.CopyDieMonsterCount("radish") % 3 == 0.0f && !MonsterSpawner.Instance.isSpawnRadish)
        {
            Invoke("InitMonster_Radish", 5.0f);
        }

        if ((float)MonsterDie.Instance.CopyDieMonsterCount("crystal") % 3 == 0.0f && !MonsterSpawner.Instance.isSpawnCrystal)
        {
            Invoke("InitMonster_Crystal", 5.0f);
        }
    }

    private void IDLE(StateFlow stateFlow)
    {
        switch (stateFlow)
        {
            case StateFlow.ENTER:
                {
                    SetAnimationName("isIdle");
                    ChangeAgentComponent("isIdle");
                    Mon_animator.SetBool("isWalk", false);
                    Mon_animator.SetBool("isIdle", true);
                }
                break;

            case StateFlow.UPDATE:
                {
                    //agent 켜기
                    agent.enabled = true;

                    //몬스터 시야에 발각 || 몬스터가 피격당함
                    if (isSeePlayer || isHit)
                    {
                        Invoke("WaitChangeToTrace", 0.4f);
                    }
                }
                break;

            case StateFlow.EXIT:
                { isDieflag = false; }
                break;
        }
    }
    
    private void WALK(StateFlow stateFlow)
    {
        switch (stateFlow)
        {
            case StateFlow.ENTER:
                {
                }
                break;

            case StateFlow.UPDATE:
                {
                    /*원래 있던 위치로 돌아간다.*/

                    //원래 있던 위치일 경우, idle로 상태전환
                    if (Vector3.Distance(oriPos, this.transform.position) <= 0.1f)
                    {
                        Mon_animator.SetBool("isTrace", false);
                        ChangeState(MonsterState.IDLE);
                    }

                    //원래 있던 위치가 아닐 경우, walk 애니메이션
                    else
                    {
                        SetAnimationName("isWalk");
                        ChangeAgentComponent("isWalk");
                        Mon_animator.SetBool("isWalk", true);
                        Mon_animator.SetBool("isTrace", false);
                        agent.SetDestination(oriPos);
                    }
                }
                break;

            case StateFlow.EXIT:
                { isDieflag = false; }
                break;
        }
    }

    private void TRACE(StateFlow stateFlow)
    {
        switch (stateFlow)
        {
            case StateFlow.ENTER:
                {
                    SetAnimationName("isTrace");
                    ChangeAgentComponent("isTrace");
                    Mon_animator.SetBool("isAttack", false);
                    Mon_animator.SetBool("isIdle", false);
                    Mon_animator.SetBool("isTrace", true);
                }
                break;

            case StateFlow.UPDATE:
                {
                    agent.SetDestination(Player.position);

                    //플레이어와의 거리가 가까울 경우, ATTACK으로 상태전환
                    if (Vector3.Distance(Player.position, this.transform.position) <= 1.6f)
                    {
                        agent.isStopped = true;
                        ChangeState(MonsterState.ATTACK);
                    }

                    //몬스터 시야에서 벗어남 && 피격상태가 아님
                    if (!isSeePlayer && !isHit)
                        Invoke("WaitChangeToWalk", 0.7f);
                }
                break;

            case StateFlow.EXIT:
                { isDieflag = false; }
                break;
        }
    }

    private void ATTACK(StateFlow stateFlow)
    {
        switch (stateFlow)
        {
            case StateFlow.ENTER:
                {
                    SetAnimationName("isAttack");
                    ChangeAgentComponent("isAttack");
                    Mon_animator.SetBool("isTrace", false);
                    Mon_animator.SetBool("isAttack", true);
                }
                break;

            case StateFlow.UPDATE:
                {
                    StartCoroutine(WaitAttackDamageCoroutine());

                    //플레이어와의 거리가 멀어진 경우, TRACE으로 상태전환
                    if (Vector3.Distance(Player.position, this.transform.position) >= 1.7f)
                    {
                        StopAllCoroutines();
                        agent.isStopped = false;
                        ChangeState(MonsterState.TRACE);
                    }
                }
                break;

            case StateFlow.EXIT:
                {
                    StopCoroutine(WaitAttackDamageCoroutine());
                    isDieflag = false;
                    isCheckAttackDamage = false;
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
                    agent.enabled = false;

                    MonsterSpawner.Instance.MonsterRespawnCheck(_name);

                    SetAnimationName("isDie");
                    ChangeAgentComponent("isDie");

                    Mon_animator.SetBool("isIdle", false);
                    Mon_animator.SetBool("isTrace", false);
                    Mon_animator.SetBool("isAttack", false);
                    Mon_animator.SetBool("isWalk", false);
                    Mon_animator.SetBool("isDie", true);

                    if(!isDieflag)
                    {
                        isDieflag = true;

                        MonsterDie.Instance.UpdateDictionary(MonsterDie.Instance.DieMonsterDic, _name, 1);
                        QuestLog.Instance.UpdateSelected();

                        //Debug.Log("name " + _name + "  count " + MonsterDie.Instance.GetDieMonsterCount(_name));
                    }               
                }
                break;

            case StateFlow.UPDATE:
                {
                    this.gameObject.tag = "Untagged";
                    Invoke("WaitDieActiveFalse", 1.5f);
                }
                break;

            case StateFlow.EXIT:
                {
                }
                break;
        }
    }

    #region  Invoker
    private void WaitChangeToWalk()
    {
        ChangeState(MonsterState.WALK);
    }

    private void WaitChangeToTrace()
    {
        ChangeState(MonsterState.TRACE);
    }

    private void WaitDieActiveFalse()
    {
        //this.gameObject.SetActive(false);

        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        this.gameObject.transform.GetChild(2).gameObject.SetActive(false);

        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        this.gameObject.GetComponent<Degree>().enabled = false;
    }
    #endregion



    //현재 AI의 행동에 따라서 agent 값과 애니메이션 스피트를 바꿔주는 함수
    private void ChangeAgentComponent(string name)
    {
        switch (name)
        {
            case "isIdle":
                {
                    //Idle의 ai agent 세팅
                    agent.speed = 3.5f;
                    agent.angularSpeed = 480.0f;
                    agent.acceleration = 60.0f;
                    agent.stoppingDistance = 0.0f;

                    break;
                }

            case "isWalk":
                {
                    //걸을 때의 ai agent 세팅
                    agent.speed = 1.5f;
                    agent.angularSpeed = 480.0f;
                    agent.acceleration = 60.0f;
                    agent.stoppingDistance = 0.0f;

                    break;
                }            

            case "isTrace":
                {
                    //쫓을 때의 ai agent 세팅
                    agent.speed = 3.0f;
                    agent.angularSpeed = 480.0f;
                    agent.acceleration = 60.0f;
                    agent.stoppingDistance = 0.0f;

                    break;
                }

            case "isAttack":
                {
                    //attack 시의 ai agent 세팅
                    agent.speed = 3.0f;
                    agent.angularSpeed = 480.0f;
                    agent.acceleration = 60.0f;
                    agent.stoppingDistance = 0.0f;

                    break;
                }
        }
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "spear" || other.gameObject.tag == "spear2"|| other.gameObject.tag == "spear3"|| other.gameObject.tag == "spear4"|| other.gameObject.tag == "spear5")
        {
            if (!audiosource.isPlaying)
                audiosource.PlayOneShot(audioClip, 1.0f);

            isHit = true;
            SetDamage(Arrow.Instance._damage);
            HpFill.fillAmount = (float)currentHp/hp;
            healthBarBackGround.SetActive(true);

            StopAllCoroutines();
            StartCoroutine(WaitCoroutine());
        }
    }

    IEnumerator WaitCoroutine()
    {
        yield return new WaitForSeconds(4f);
        healthBarBackGround.SetActive(false);
        isHit = false;
    }

    IEnumerator WaitAttackDamageCoroutine()
    {
        yield return new WaitForSeconds(1.0f);

        if (!isCheckAttackDamage)
        {
            if (!once)
            {
                audiosource2.PlayOneShot(audioClip2, 0.4f);
                once = true;
            }

            DmgAttack();
            isCheckAttackDamage = true;

            yield return new WaitForSeconds(1.25f);

            isCheckAttackDamage = false;
            once = false;
        }
    }


    //버섯몬스터를 초기상태로 초기화함.
    protected void InitMonster_MushRoom()
    {
        if(this.gameObject.name == "Mon01_mushroom(Clone)")
        {
            agent.enabled = false;

            healthBarBackGround.SetActive(false);

            //체력, 애니메이션상태
            currentHp = 100;
            HpFill.fillAmount = (float)currentHp / hp;

            SetAnimationName("isIdle");
            ChangeAgentComponent("isIdle");

            Mon_animator.SetBool("isIdle", true);
            Mon_animator.SetBool("isTrace", false);
            Mon_animator.SetBool("isAttack", false);
            Mon_animator.SetBool("isWalk", false);
            Mon_animator.SetBool("isDie", false);

            isSeePlayer = false;
            isHit = false;
            isDieflag = false;

            this.gameObject.tag = _name;

            this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            this.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            this.gameObject.transform.GetChild(2).gameObject.SetActive(true);

            this.gameObject.GetComponent<BoxCollider>().enabled = true;
            this.gameObject.GetComponent<Degree>().enabled = true;

            ChangeState(MonsterState.IDLE);
        }
        
    }

    protected void InitMonster_Radish()
    {
        if (this.gameObject.name == "Mon02_radish(Clone)")
        {
            agent.enabled = false;

            healthBarBackGround.SetActive(false);

            //체력, 애니메이션상태
            currentHp = 100;
            HpFill.fillAmount = (float)currentHp / hp;

            SetAnimationName("isIdle");
            ChangeAgentComponent("isIdle");

            Mon_animator.SetBool("isIdle", true);
            Mon_animator.SetBool("isTrace", false);
            Mon_animator.SetBool("isAttack", false);
            Mon_animator.SetBool("isWalk", false);
            Mon_animator.SetBool("isDie", false);

            isSeePlayer = false;
            isHit = false;
            isDieflag = false;

            this.gameObject.tag = _name;

            this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            this.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            this.gameObject.transform.GetChild(2).gameObject.SetActive(true);

            this.gameObject.GetComponent<BoxCollider>().enabled = true;
            this.gameObject.GetComponent<Degree>().enabled = true;

            ChangeState(MonsterState.IDLE);
        }

    }

    protected void InitMonster_Crystal()
    {
        if (this.gameObject.name == "Mon03_cystal(Clone)")
        {
            agent.enabled = false;

            healthBarBackGround.SetActive(false);

            //체력, 애니메이션상태
            currentHp = 100;
            HpFill.fillAmount = (float)currentHp / hp;

            SetAnimationName("isIdle");
            ChangeAgentComponent("isIdle");

            Mon_animator.SetBool("isIdle", true);
            Mon_animator.SetBool("isTrace", false);
            Mon_animator.SetBool("isAttack", false);
            Mon_animator.SetBool("isWalk", false);
            Mon_animator.SetBool("isDie", false);

            isSeePlayer = false;
            isHit = false;
            isDieflag = false;

            this.gameObject.tag = _name;

            this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            this.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            this.gameObject.transform.GetChild(2).gameObject.SetActive(true);

            this.gameObject.GetComponent<BoxCollider>().enabled = true;
            this.gameObject.GetComponent<Degree>().enabled = true;

            ChangeState(MonsterState.IDLE);
        }

    }
}    

