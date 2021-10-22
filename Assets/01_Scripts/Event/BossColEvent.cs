using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossColEvent : MonoBehaviour
{
    public UnityEvent _cameraSubOn;
    public UnityEvent _cameraSubOff;
    public UnityEvent _autoMoveOn;

    public BoxCollider _bossBoxCol;
    public Canvas _bossCanvas;

    public void BossEventOn()
    {
        GameManager.Instance.PlayerStateChange("STOP");
        PlayerAnimationController.Instance.ChangeAnimationState("IDLE", false);

        //콜라이더 비활성화
        _bossBoxCol.enabled = false;
        _bossCanvas.enabled = true;


        //panel fade out과 in 함수 실행
        FadeController.Instance.FadeIn(2.0f, null);
        Invoke("WaitFadeOut", 3.0f);
    }

    private void WaitFadeOut()
    {
        FadeController.Instance.FadeOut(2.0f, null);
        
        //서브카메라 활성화
        CameraSubOn();

        //자동이동 
        AutoMoveOn();
    }


    //이벤트 함수
    public void CameraSubOn()
    {
        _cameraSubOn.Invoke();
    }

    public void CameraSubOff()
    {
        _cameraSubOff.Invoke();
    }

    public void AutoMoveOn()
    {
        _autoMoveOn.Invoke();
    }
}
