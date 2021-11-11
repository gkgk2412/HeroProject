using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class PlayerMoveAuto : MonoBehaviour
{
    public UnityEvent _bossEvnet02;
    public BoxCollider bossRoomWall;

    [HideInInspector]
    public Vector3[] wayPoints;         // 이동포인트 배열
    [HideInInspector]
    public Vector3[] rotatePoints;     // 회전포인트 배열

    Animator pAnimator;

    internal static class YieldInstructionCache
    {
        public static readonly WaitForFixedUpdate WaitForFixedUpdate = new WaitForFixedUpdate();
    }

    private void Start()
    {      
        pAnimator = this.GetComponent<Animator>();
    }

    public void AutoMoveStart()
    {
        StartCoroutine(MoveRotCoroutine());
    }

    private IEnumerator MoveRotCoroutine()
    {
        this.GetComponent<CharacterController>().enabled = false;

        while (true)
        {
            /*경로 이동 및 회전*/
            AIMove.Instance.wayPoint(wayPoints, this.gameObject, 1.0f);
            AIMove.Instance.RotatePoint(wayPoints, rotatePoints, this.gameObject, 8.0f);
            pAnimator.SetBool("isAutoMove", true);

            //마지막 경로에 도착
            if (AIMove.Instance.returnIndex() == 2)
            {
                this.GetComponent<CharacterController>().enabled = true;
                FadeController.Instance.FadeIn(2.0f, null);
                pAnimator.SetBool("isAutoMove", false);

                bossRoomWall.isTrigger = false;
                Invoke("WaitFadeOut", 3.0f);
                yield break;
            }

            yield return YieldInstructionCache.WaitForFixedUpdate;
        }
    }

    private void WaitFadeOut()
    {
        FadeController.Instance.FadeOut(2.0f, null);
        BossColEvent();
    }

    public void BossColEvent()
    {
        _bossEvnet02.Invoke();
    }


    #region Internal-------------------------- #  ABOUT GIZMO  # --------------------------

    //위치 정보 기즈모에 표시하는 함수
    private void OnDrawGizmosSelected()
    {
        if (wayPoints == null)
            return;

        for (int i = 0; i < wayPoints.Length; ++i)
        {
            Gizmos.color = Color.green;

            //처음 시작이면 오브젝트 현재위치에서 0번째 wayPoint 위치까지 선긋기
            if (i == 0)
            {
                Gizmos.DrawLine(transform.position, wayPoints[i]);
            }

            //그 외에는 이전 wayPoint에서 현재 wayPoint까지 선긋기
            else
            {
                //Debug.Log(wayPoints[i - 1].ToString() + "  / to" + wayPoints[i].ToString());
                Gizmos.DrawLine(wayPoints[i - 1], wayPoints[i]);
            }

            //포인트마다 Gizmo 아이콘 그려줌
            Gizmos.DrawIcon(wayPoints[i], "wayPointGizmoIcon.png", true);
        }
    }

    #endregion;
}
