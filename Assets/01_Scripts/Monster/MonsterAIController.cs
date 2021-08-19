using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterAIController : Monster
{
    NavMeshAgent agent;
    Animator Mon_animator;

    public enum MonsterState
    {
        IDLE,
        WALK,
        TRACE,
        ATTACK,
        DIE
    }

    public MonsterState startState = MonsterState.IDLE;
    [SerializeField] private MonsterState currentState;

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
        Player = GameObject.Find("Player").transform;
        agent = this.GetComponent<NavMeshAgent>();
        Mon_animator = this.GetComponent<Animator>();
    }

    private void CommonUpdate()
    { }

    private void IDLE(StateFlow stateFlow)
    {
        switch (stateFlow)
        {
            case StateFlow.ENTER:
                {
                    Mon_animator.SetBool("isWalk", false);
                    Mon_animator.SetBool("isIdle", true);
                }
                break;

            case StateFlow.UPDATE:
                {
                    //몬스터 시야에 발각
                    if (isSeePlayer)
                    {
                        //원래있던위치를 저장함.
                        oriPos = this.gameObject.transform.position;
                        ChangeState(MonsterState.TRACE);
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

                    //몬스터 시야에서 벗어남
                    if (!isSeePlayer)
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
                }
                break;

            case StateFlow.UPDATE:
                {
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
    }
}
