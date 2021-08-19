using UnityEngine;
using UnityEditor;

/*
 플레이어 검출범위 그림 
*/

[CustomEditor(typeof(Degree))]
public class ViewEditor : Editor
{
    private void OnSceneGUI()
    {
        //degree 클래스를 참조
        Degree cctv = (Degree)target;

        //원주 위의 시작점의 좌표를 계산(주어진 각도의 1/2)
        Vector3 fromAnglePos = cctv.CirclePoint(-cctv.viewAngle * 0.5f);

        ////원의 색상을 흰색으로 지정
        //Handles.color = Color.white;

        Handles.color = new Color(1, 1, 1, 0.1f);

        //외곽선만 표현하는 원반을 그림
        Handles.DrawWireDisc(cctv.transform.position //원점좌표
                            , Vector3.up //노말벡터
                            , cctv.viewRange); //원의 반지름 

        //부채꼴의 색상을 지정
        Handles.DrawSolidArc(cctv.transform.position //원점좌표
                             , Vector3.up //노말벡터
                             , fromAnglePos // 부채꼴의 시작 좌표
                             , cctv.viewAngle // 부채꼴의 각도
                             , cctv.viewRange); // 부채꼴의 반지름

        //시야각의 텍스트를 표시
        Handles.Label(cctv.transform.position + (cctv.transform.forward * 2.0f), cctv.viewAngle.ToString());
    }
}
