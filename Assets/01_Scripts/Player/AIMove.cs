using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMove : MonoBehaviour
{
    private Vector3 curPosition;    // 현재 위치 
    private int wayPointIndex = 0;  // 이동포인트 인덱스

    private static AIMove instance;
    public static AIMove Instance => instance;

    public AIMove()
    {
        //자기 자신에 대한 정보를 static 형태의 instacne 변수에 저장하여 외부에서 접근이 쉽도록 함
        instance = this;
    }

    //경로대로 이동하는 함수
    public void wayPoint(Vector3[] wayPoints, GameObject name, float speed)
    {
        //이동중인 현재 위치 curPosition 변수에 담기
        curPosition = name.transform.position;

        //이동지점 배열의 인덱스 0부터 배열크기 -1 까지
        if (wayPointIndex < wayPoints.Length)
        {
            float step;

            step = speed * Time.deltaTime;

            //현재 위치를 frame 처리 시간 비율로 계산한 속도만큼 옮겨줌.
            // 즉, 1개의 프레임 단위로 움직임 처리
            name.transform.position = Vector3.MoveTowards(curPosition, wayPoints[wayPointIndex], step);

            //현재위치가 이동지점의 위치라면 배열 인덱스 +1 하여 다음 포인트로 이동하도록 
            if (Vector3.Distance(wayPoints[wayPointIndex], curPosition) <= 0.3f)
            {
                ++wayPointIndex;
            }
        }
    }

    //특정 index 일때 회전하는 함수
    public void RotatePoint(Vector3[] wayPoints, Vector3[] rotatePoints, GameObject name, float smoothTime)
    {
        //이동지점 배열의 인덱스 0부터 배열크기 -1 까지
        if (wayPointIndex < wayPoints.Length)
        {
            //지정된 rotatePoint index에서 회전
            //인스펙터 창에 인덱스와 각도를 입력하면 그 인덱스에 도달했을때 각도만큼 회전하도록...
            name.transform.rotation = Quaternion.Lerp(name.transform.rotation, Quaternion.Euler(rotatePoints[wayPointIndex]), Time.deltaTime * smoothTime);
        }
    }


    //wayPoint 현재 인덱스 리턴
    public int returnIndex()
    {
        return wayPointIndex;
    }
}