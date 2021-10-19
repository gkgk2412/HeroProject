using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
//바닥범위 안에 있을때, raycast 맞고 있으면 충돌
//바닥범위 안에 있어도 raycast 맞지 않으면 장애물
//바닥범위 안에 있지 않으면 아무것도 체크하지 않음
 */

public class Degree : MonoBehaviour
{
    //몬스터의 추적 사정 거리의 범위
    public float viewRange = 8.5f;

    //몬스터 시야각
    public float viewAngle = 360.0f;

    //ray의 높이
    public float rayHeight = .2f;

    private GameObject playerTr;
    private int playerLayer;

    private GameObject monsterTr;

    private Coroutine runningCoroutine = null;

    MonsterAIController _scrMon;

    bool isTrace = false;

    //코루틴 yield 캐싱
    internal static class YieldInstructionCache
    {
        public static readonly WaitForFixedUpdate WaitForFixedUpdate = new WaitForFixedUpdate();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerTr = GameObject.Find("Player");
        monsterTr = this.gameObject;
        _scrMon = this.gameObject.GetComponent<MonsterAIController>();

        runningCoroutine = StartCoroutine(CctvRay());

        //레이어 마스크 값 계산
        playerLayer = LayerMask.NameToLayer("PLAYER");
    }


    private IEnumerator CctvRay()
    {
        while (true)
        {
            isViewPlayer(monsterTr, playerTr);

            //바닥범위안에 있는가
            if (isTracePlayer(viewAngle, viewRange))
            {
                //ray를 맞고 있는가
                if (isViewPlayer(monsterTr, playerTr))
                {
                    //몬스터에게 발각
                    _scrMon.isSeePlayer = true;
                }
            }

            else
            {
                _scrMon.isSeePlayer = false;
            }

            yield return YieldInstructionCache.WaitForFixedUpdate;
        }
    }

    //주어진 각도에 의해 원주 위의 점의 좌표값을 계산하는 함수
    public Vector3 CirclePoint(float angle)
    {
        //로컬 좌표계 기준으로 설정하기 위해 cctv의 Y 회전값을 더함
        angle += transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad),
                           0,
                           Mathf.Cos(angle * Mathf.Deg2Rad));
    }


    //반경범위내에 플레이어있는지 판단
    public bool isTracePlayer(float _viewAngle, float _viewRange)
    {
        //시야반경범위 안에서 주인공 캐릭터를 검출 
        //몬스터를 중심으로 하여 viewRange 만큼의 반경에서 검사)
        //플레이어만 검출하기 위해서 layer 사용
        Collider[] colls = Physics.OverlapSphere(transform.position
                                                 , _viewRange
                                                 , 1 << playerLayer);

        //배열의 개수가 1일때 주인공이 범위안에 있다고 판단
        if (colls.Length == 1)
        {
            //몬스터와 주인공 사이의 방향벡터를 계산
            Vector3 dir = (playerTr.transform.position - transform.position).normalized;

            //몬스터의 시야각에 들어왔는지를 판단
            if (Vector3.Angle(transform.forward, dir) < _viewAngle * 0.5f)
            {
                isTrace = true;
            }
        }

        else if(colls.Length == 0)
        {
            isTrace = false;
        }

        return isTrace;
    }

    //장애물 여부 판단
    public bool isViewPlayer(GameObject RayObj, GameObject hitObject)
    {
        bool isView = false;

        float maxDistance = 100;
        RaycastHit hit;

        //몬스터와 플레이어 사이의 방향 벡터를 계산
        Vector3 dir = (hitObject.transform.position - RayObj.transform.position).normalized;

        bool isHit = Physics.Raycast(new Vector3(RayObj.transform.position.x, RayObj.transform.position.y + rayHeight, RayObj.transform.position.z)
            , dir, out hit, maxDistance);

        if (isHit)
            Debug.DrawRay(new Vector3(RayObj.transform.position.x, RayObj.transform.position.y + rayHeight, RayObj.transform.position.z)
                , dir * hit.distance, Color.red);
        else
            Debug.DrawRay(new Vector3(RayObj.transform.position.x, RayObj.transform.position.y + rayHeight, RayObj.transform.position.z)
                , dir * maxDistance, Color.red);


        //Raycast를 투사하여 장애물이 있는지를 판단
        if (Physics.Raycast(new Vector3(RayObj.transform.position.x, RayObj.transform.position.y + rayHeight, RayObj.transform.position.z)
            , dir, out hit, maxDistance))
        {
            isView = hit.collider.CompareTag("Player");
        }

        return isView;
    }
}