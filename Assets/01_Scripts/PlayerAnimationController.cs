using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator _charAnim;

    private static PlayerAnimationController instance;
    public static PlayerAnimationController Instance => instance;

    public PlayerAnimationController()
    {
        //자기 자신에 대한 정보를 static 형태의 instacne 변수에 저장하여 외부에서 접근이 쉽도록 함
        instance = this;
    }

    public void Start()
    {
        _charAnim = GetComponent<Animator>();
    }

    public void ChangeAnimationState(string state, bool isbool)
    {
        switch (state)
        {
            case "IDLE":
                {
                    _charAnim.SetFloat("Speed", 0.0f);
                    _charAnim.SetBool("isJump", isbool);
                    break;
                }

            case "JUMP":
                {
                    _charAnim.SetBool("isJump", isbool);
                    break;
                }
            case "WALK":
                {
                    _charAnim.SetFloat("Speed", PlayerControlManager.Instance.MoveDir.magnitude);
                    break;
                }

            case "RUN":
                {
                    _charAnim.SetFloat("MoveAniSpeed", 1.3f);
                    break;
                }

            case "NOTRUN":
                {
                    _charAnim.SetFloat("MoveAniSpeed", 1.0f);
                    break;
                }
        }
    }
}
