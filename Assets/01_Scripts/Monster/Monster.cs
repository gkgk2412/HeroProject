﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class Monster : MonoBehaviour
{
    protected Transform Player;
    public GameObject healthBarBackGround;
    public Image HpFill;

    public enum MonsterState
    {
        IDLE,
        WALK,
        TRACE,
        ATTACK,
        DIE
    }

    protected MonsterState startState = MonsterState.IDLE;
    protected MonsterState currentState;

    protected ArrowValue _arrow;

    protected Vector3 oriPos;      //몬스터가 원래 있던 위치 

    public string _name;
    public float hp;
    public float currentHp;
    protected float attackDamage = 5;
    public float speed;
    public string currentAnimationName;

    public bool isSeePlayer;        //몬스터 시야에 들어옴
    protected bool isHit;           //몬스터가 피격당함
    protected bool isDieflag;       //몬스터가 죽음

    //현재 애니메이션 이름 저장하는 함수
    protected void SetAnimationName(string name)
    {
        currentAnimationName = name;
    }

    protected float SetDamage(float damage)
    {
        currentHp -= damage;
        return currentHp;
    }

    protected void DmgAttack()
    {
        //때리는 행동을 했는데 범위 내에 플레이어가 있다면 실제로 데미지 준다. (때리는 행동 한 번 할 때마다)
        PlayerControlManager.Instance.MyCurHP = PlayerControlManager.Instance.MyCurHP - attackDamage;
    }
}