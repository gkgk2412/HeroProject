using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boss : MonoBehaviour
{
    public CameraController _CameraController;
    public Transform Player;

    public float b_hp;
    public float b_CurrentHp;
    public float moveSpeed;
    public float rotSpeed;

    public enum BossState
    {
        IDLE,
        SKILL01_ROCK_THROW,
        SKILL02_BODY_BlOW,
        SKILL03_BODY_BlOW,
        DIE
    }

    protected BossState startState = BossState.IDLE;
    protected BossState currentState;

    protected float SetDamage(float damage)
    {
        b_CurrentHp -= damage;
        return b_CurrentHp;
    }

    public void DmgAttack(float damage)
    {
        if (PlayerControlManager.Instance.MyCurHP > 0)
        {
            PlayerControlManager.Instance.MyCurHP = PlayerControlManager.Instance.MyCurHP - damage;

            string message = damage.ToString();

            FloatingTextManager.instance.CreateFloatingPlayerDamageText(new Vector3(Player.position.x, Player.position.y + 1.0f, Player.position.z), message);
        }

        if (PlayerControlManager.Instance.MyCurHP <= 0)
        {
            PlayerControlManager.Instance.MyCurHP = 0;
        }
    }
}
