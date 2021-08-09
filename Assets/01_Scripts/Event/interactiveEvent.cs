using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class interactiveEvent : MonoBehaviour
{
    public InteractUI _interactUIScr;
    public InteractUI _interactUIScr2;

    private bool isInteractKeyDown = false;

    public UnityEvent _boardEventON;
    public UnityEvent _boardEventOFF;

    public UnityEvent _kingEventON;
    public UnityEvent _kingEventOFF;

    private void Update()
    {
        //상호작용 범위 안에 있을 경우
        if(_interactUIScr.RangeCheck())
        {
            if (Input.GetKeyUp(KeyCode.F))
            {
                ExecuteBoard();
            }
        }
        
        if(_interactUIScr2.RangeCheck() && !GameManager.Instance.GetQuestCheck())
        {
            if (Input.GetKeyUp(KeyCode.F))
            {
                ExecuteKing();
            }
        }
    }

    public void ExecuteBoard()
    {
        /*------게시판 이벤트------*/
        if (isInteractKeyDown)
        {
            InteractionBoardOFF();
            GameManager.Instance.PlayerStateChange("LIVE");
            isInteractKeyDown = false;
        }
        else
        {
            InteractionBoardOn();
            GameManager.Instance.PlayerStateChange("STOP");
            isInteractKeyDown = true;
        }
    }

    public void ExecuteKing()
    {
        /*------왕 이벤트------*/
        if (isInteractKeyDown)
        {
            InteractionKingOFF();
            GameManager.Instance.PlayerStateChange("LIVE");
            isInteractKeyDown = false;
        }
        else
        {
            InteractionKingOn();
            GameManager.Instance.PlayerStateChange("STOP");
            PlayerAnimationController.Instance.ChangeAnimationState("IDLE", false);
            isInteractKeyDown = true;
        }
    }

    /*-------------------이벤트 연결 함수-------------------*/
    public void InteractionBoardOn()
    {
        _boardEventON.Invoke();
    }

    public void InteractionBoardOFF()
    {
        _boardEventOFF.Invoke();
    }

    public void InteractionKingOn()
    {
        _kingEventON.Invoke();
    }

    public void InteractionKingOFF()
    {
        _kingEventOFF.Invoke();
    }
}
