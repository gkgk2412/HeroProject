using System.Collections;
using System.Collections.Generic;
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
    public float damage;
    public float speed;
    public float attackSpeed;
    public string currentAnimationName;

    public bool isSeePlayer;    //몬스터 시야에 들어옴


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
}
