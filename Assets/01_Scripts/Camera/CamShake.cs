using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShake : MonoBehaviour
{
    Vector3 originPos;
    
    private static CamShake instance;
    public static CamShake Instance => instance;

    public CamShake()
    {
        //자기 자신에 대한 정보를 static 형태의 instacne 변수에 저장하여 외부에서 접근이 쉽도록 함
        instance = this;
    }

    public void InCameraShake(float shakePwr, float shakeDur)
    {
        originPos = transform.position;
        StartCoroutine(Shake(shakePwr, shakeDur));
    }

    public IEnumerator Shake(float _amount, float _duration)
    {
        float timer = 0;

        while (timer <= _duration)
        {
            transform.position = (Vector3)Random.insideUnitCircle * _amount + originPos;

            timer += Time.deltaTime;
            yield return null;
        }

        transform.position = originPos;
    }
}
