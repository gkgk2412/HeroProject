using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseUpCam : MonoBehaviour
{
    Camera _camera;

    private float m_FieldOfView;    //바꿔줄 변수
    private float ori_FieldOfView;  //원래 FOV

    private static CloseUpCam instance;
    public static CloseUpCam Instance => instance;

    public CloseUpCam()
    {
        //자기 자신에 대한 정보를 static 형태의 instacne 변수에 저장하여 외부에서 접근이 쉽도록 함
        instance = this;
    }


    //코루틴 yield 캐싱
    internal static class YieldInstructionCache
    {
        public static readonly WaitForFixedUpdate WaitForFixedUpdate = new WaitForFixedUpdate();
    }

    private void Start()
    {
        _camera = this.GetComponent<Camera>();

        //현재 카메라의 FOV를 넣어준다.
        m_FieldOfView = _camera.fieldOfView;
        ori_FieldOfView = _camera.fieldOfView;
    }

    public void InCameraCloseUp(float minusNum, float max_FieldOfView)
    {
        StartCoroutine(CameraCloseUp(minusNum, max_FieldOfView));
    }

    private IEnumerator CameraCloseUp(float minusNum, float max_FieldOfView)
    {
        while (true)
        {
            //카메라 클로즈업
            _camera.fieldOfView = m_FieldOfView;

            m_FieldOfView -= minusNum;

            if (m_FieldOfView < max_FieldOfView)
                m_FieldOfView = max_FieldOfView;

            yield return YieldInstructionCache.WaitForFixedUpdate;
        }
    }
}
