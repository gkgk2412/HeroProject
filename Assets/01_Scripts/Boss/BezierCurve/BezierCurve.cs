using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BezierCurve : MonoBehaviour
{
    private static BezierCurve instance;
    public static BezierCurve Instance => instance;

    public BezierCurve()
    {
        //자기 자신에 대한 정보를 static 형태의 instacne 변수에 저장하여 외부에서 접근이 쉽도록 함
        instance = this;
    }

    //4개의 point 
    public Transform b_Pos, p_Pos;

    [HideInInspector]
    private Vector3 P1, P2, P3, P4;

    #region public Vector
    public Vector3 MyVec1
    {
        get
        {
            return P1;
        }

        set
        {
            P1 = value;
        }
    }

    public Vector3 MyVec2
    {
        get
        {
            return P2;
        }

        set
        {
            P2 = value;
        }
    }

    public Vector3 MyVec3
    {
        get
        {
            return P3;
        }

        set
        {
            P3 = value;
        }
    }

    public Vector3 MyVec4
    {
        get
        {
            return P4;
        }

        set
        {
            P4 = value;
        }
    }
    #endregion

    public float height;

    public GameObject rockPrefab;

    public void Update()
    {
        P1 = new Vector3(b_Pos.position.x, b_Pos.position.y + 2.0f, b_Pos.position.z);
        P2 = new Vector3(b_Pos.position.x, b_Pos.position.y + height, b_Pos.position.z);

        //조건은 스킬 발동
        if (Input.GetKeyDown(KeyCode.Space))
        {
            P3 = new Vector3(p_Pos.position.x, p_Pos.position.y + height, p_Pos.position.z);
            P4 = new Vector3(p_Pos.position.x, p_Pos.position.y + 0.5f, p_Pos.position.z);

            GameObject obj = Instantiate(rockPrefab);
        }
    }

    public Vector3 BezierCurveFunc(Vector3 p_1, Vector3 p_2, Vector3 p_3, Vector3 p_4, float _speed)
    {
        Vector3 A = Vector3.Lerp(p_1, p_2, _speed);
        Vector3 B = Vector3.Lerp(p_2, p_3, _speed);
        Vector3 C = Vector3.Lerp(p_3, p_4, _speed);

        Vector3 D = Vector3.Lerp(A, B, _speed);
        Vector3 E = Vector3.Lerp(B, C, _speed);

        Vector3 F = Vector3.Lerp(D, E, _speed);

        return F;
    }

    [CanEditMultipleObjects]
    [CustomEditor(typeof(BezierCurve))]
    public class BezierCurve_Editor : Editor
    {
        private void OnSceneGUI()
        {
            BezierCurve Generator = (BezierCurve)target;

            #region//////Scene View에서 Handle 움직일 수 있도록 함.//////
            Generator.P1 =  Handles.PositionHandle(Generator.P1, Quaternion.identity);
            Generator.P2 =  Handles.PositionHandle(Generator.P2, Quaternion.identity);
            Generator.P3 =  Handles.PositionHandle(Generator.P3, Quaternion.identity);
            Generator.P4 =  Handles.PositionHandle(Generator.P4, Quaternion.identity);
            #endregion


            Handles.PositionHandle(Generator.P1, Quaternion.identity);
            Handles.PositionHandle(Generator.P2, Quaternion.identity);
            Handles.PositionHandle(Generator.P3, Quaternion.identity);
            Handles.PositionHandle(Generator.P4, Quaternion.identity);

            Handles.DrawLine(Generator.P1, Generator.P2);
            Handles.DrawLine(Generator.P3, Generator.P4);

            for(float i = 0; i < 10; ++i)
            {
                float value_Before = i / 10;
                Vector3 Before = Generator.BezierCurveFunc(Generator.P1, Generator.P2, Generator.P3, Generator.P4, value_Before);

                float value_After = (i + 1) / 10;
                Vector3 After = Generator.BezierCurveFunc(Generator.P1, Generator.P2, Generator.P3, Generator.P4, value_After);

                Handles.DrawLine(Before, After);
            }
        }
    }
}
