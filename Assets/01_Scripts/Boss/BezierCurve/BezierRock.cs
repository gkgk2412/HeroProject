using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierRock : MonoBehaviour
{
    [Range(0, 1)]
    public float speed;

    public float speedPlus = 1.2f;

    private bool isBezier = true;

    internal static class YieldInstructionCache
    {
        public static readonly WaitForFixedUpdate WaitForFixedUpdate = new WaitForFixedUpdate();
    }

    public void Start()
    {
        StartCoroutine(BezierCoroutine());
    }

    private IEnumerator BezierCoroutine()
    {
        while(true)
        {
            if (speed <= 1)
            {
                speed += Time.deltaTime * speedPlus;
            }

            if (isBezier)
            {
                //생성된 돌을 베지에 곡선으로 이동시킴
                this.transform.position = BezierCurve.Instance.BezierCurveFunc(BezierCurve.Instance.MyVec1, BezierCurve.Instance.MyVec2, BezierCurve.Instance.MyVec3, BezierCurve.Instance.MyVec4, speed);
            }

            else
            {
                Destroy(this.gameObject, 3.0f);
            }

            yield return YieldInstructionCache.WaitForFixedUpdate;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //캐릭터나 바닥에 닿으면 베지어 끝내기
        if(other.gameObject.tag == "Player")
        {
            isBezier = false;
        }

        if (other.gameObject.tag == "Bottom")
        {
            isBezier = false;
            this.transform.GetChild(1).GetComponent<BoxCollider>().enabled = false;
        }
    }
}
