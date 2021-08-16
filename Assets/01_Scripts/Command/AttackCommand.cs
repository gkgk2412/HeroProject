using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCommand : CommandKey
{
    ObjectPooler objectPooler;

    public AttackCommand(MonoBehaviour _mono)
    {
        this.mono = _mono;
    }

    private void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }

    public override void Execute()
    {
        Attack();
    }

    void Attack()
    {
        GameManager.Instance.PlayerStateChange("LIVE_ATTACK");
        PlayerAnimationController.Instance.ChangeAnimationState("ATTACK", true);
    }
}
