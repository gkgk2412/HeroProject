using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class interactiveEvent : MonoBehaviour
{
    public AudioSource audiosource;
    public AudioSource audiosource02;
    public AudioClip audioClip;
    public AudioClip audioClip02;

    public InteractUI _interactUIScr;
    public InteractUI _interactUIScr2;
    public InteractUI _interactUIScr3;

    private bool isInteractKeyDown = false;

    public UnityEvent _boardEventON;
    public UnityEvent _boardEventOFF;

    public UnityEvent _kingEventON;
    public UnityEvent _kingEventOFF;
    
    public UnityEvent _storeEventON;
    public UnityEvent _storeEventOFF;

    private void Update()
    {
        //상호작용 범위 안에 있을 경우
        if(_interactUIScr.RangeCheck())
        {
            if (Input.GetKeyUp(KeyCode.F))
            {
                if (!audiosource.isPlaying)
                    audiosource.PlayOneShot(audioClip, 1.0f);

                ExecuteBoard();
            }
        }
        
        if(_interactUIScr2.RangeCheck() && !GameManager.Instance.GetQuestCheck())
        {
            if (Input.GetKeyUp(KeyCode.F))
            {
                if (!audiosource.isPlaying)
                    audiosource.PlayOneShot(audioClip, 1.0f);

                if (!audiosource02.isPlaying)
                    audiosource02.PlayOneShot(audioClip02, 1.0f);

                ExecuteKing();
            }
        }
        
        if(_interactUIScr3.RangeCheck())
        {
            if (Input.GetKeyUp(KeyCode.F))
            {
                if (!audiosource.isPlaying)
                    audiosource.PlayOneShot(audioClip, 1.0f);

                ExecuteStore();
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
            // 다른 UI창이 띄워지지 않았을 경우
            if (!GameManager.Instance.GetUiWindowCheck())
            {
                InteractionBoardOn();
                GameManager.Instance.PlayerStateChange("STOP");
                isInteractKeyDown = true;
            }
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
            // 다른 UI창이 띄워지지 않았을 경우
            if (!GameManager.Instance.GetUiWindowCheck())
            {
                InteractionKingOn();
                GameManager.Instance.PlayerStateChange("STOP");
                PlayerAnimationController.Instance.ChangeAnimationState("IDLE", false);
                isInteractKeyDown = true;
            }
        }
    }

    public void ExecuteStore()
    {
        /*------상점 이벤트------*/
        if (isInteractKeyDown)
        {
            InteractionStoreOFF();
            GameManager.Instance.PlayerStateChange("LIVE");
            isInteractKeyDown = false;
        }
        else
        {
            // 다른 UI창이 띄워지지 않았을 경우
            if (!GameManager.Instance.GetUiWindowCheck())
            {
                InteractionStoreOn();
                GameManager.Instance.PlayerStateChange("STOP");
                PlayerAnimationController.Instance.ChangeAnimationState("IDLE", false);
                isInteractKeyDown = true;
            }
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

    public void InteractionStoreOn()
    {
        _storeEventON.Invoke();
    }

    public void InteractionStoreOFF()
    {
        _storeEventOFF.Invoke();
    }
}
