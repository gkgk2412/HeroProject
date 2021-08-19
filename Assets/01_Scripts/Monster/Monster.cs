using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Monster : MonoBehaviour
{
    protected Transform Player;

    public string _name;

    public float hp;

    public float damage;

    public float speed;

    public float attackSpeed;

    public bool isSeePlayer;    //몬스터 시야에 들어옴

    protected Vector3 oriPos;      //몬스터가 원래 있던 위치 
}
