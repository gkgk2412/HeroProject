using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunCommand : CommandKey
{
    public RunCommand(MonoBehaviour _mono)
    {
        this.mono = _mono;
    }

    public override void Execute()
    {
        Run();
    }

    void Run()
    {
        PlayerControlManager.Instance.speed = PlayerControlManager.Instance.runSpeed;
        PlayerAnimationController.Instance.ChangeAnimationState("RUN", true);

        //스테미너 줄어들기
        if (PlayerControlManager.Instance.curStamina >= 0)
            PlayerControlManager.Instance.curStamina -= 1;
    }
}
