using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class MonsterAIController : Monster
{
    NavMeshAgent agent;
    Animator Mon_animator;
    

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
    }

    private void CommonUpdate()
    { 
        //체력이 0보다 작거나 같아 질 경우 DIE 상태로 전환
        if(currentHp <= 0)
            ChangeState(MonsterState.DIE);
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
                    //몬스터 시야에 발각 || 몬스터가 피격당함
                    if (isSeePlayer || isHit)
                    {
                        //원래있던위치를 저장함.
                        oriPos = this.gameObject.transform.position;
                        Invoke("WaitChangeToTrace", 0.4f);
                    }
                }
                break;

            case StateFlow.EXIT:
                {}
                break;
        }
    }
    
    private void WALK(StateFlow stateFlow)
    {
        switch (stateFlow)
        {
            case StateFlow.ENTER:
                { }
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
                {}
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
                    if (Vector3.Distance(Player.position, this.transform.position) <= 1.4f)
                    {
                        ChangeState(MonsterState.ATTACK);
                    }

                    //몬스터 시야에서 벗어남 && 피격상태가 아님
                    if (!isSeePlayer && !isHit)
                        Invoke("WaitChangeToWalk", 0.7f);
                }
                break;

            case StateFlow.EXIT:
                {}
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
                    //플레이어와의 거리가 멀어진 경우, TRACE으로 상태전환
                    if (Vector3.Distance(Player.position, this.transform.position) >= 1.5f)
                    {
                        ChangeState(MonsterState.TRACE);
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
                    SetAnimationName("isDie");
                    ChangeAgentComponent("isDie");

                    Mon_animator.SetBool("isIdle", false);
                    Mon_animator.SetBool("isTrace", false);
                    Mon_animator.SetBool("isAttack", false);
                    Mon_animator.SetBool("isWalk", false);
                    Mon_animator.SetBool("isDie", true);
                }
                break;

            case StateFlow.UPDATE:
                {
                    this.gameObject.tag = "Untagged";
                    Destroy(this.gameObject, 3.0f);
                }
                break;

            case StateFlow.EXIT:
                {
                }
                break;
        }
    }

    private void WaitChangeToWalk()
    {
        ChangeState(MonsterState.WALK);
    }
    
    private void WaitChangeToTrace()
    {
        ChangeState(MonsterState.TRACE);
    }


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
            isHit = true;
            SetDamage(_arrow.damage);
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
}
