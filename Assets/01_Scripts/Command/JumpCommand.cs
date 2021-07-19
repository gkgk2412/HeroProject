using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCommand : CommandKey
{
    public JumpCommand(MonoBehaviour _mono)
    {
        this.mono = _mono;
    }

    public override void Execute()
    {
        Jump();
    }

    void Jump()
    {
        PlayerControlManager.Instance.MoveDir.y = PlayerControlManager.Instance.jumpSpeed;
        PlayerAnimationController.Instance.ChangeAnimationState("JUMP", true);
    }
}
