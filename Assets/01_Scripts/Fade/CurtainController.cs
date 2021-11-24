using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurtainController : MonoBehaviour
{
    public GameObject[] curtainPanel = new GameObject[2];
    private Transform[] oriTrans = new Transform[2];

    private int onValue = 50;
    private int offValue = 200;

    internal static class YieldInstructionCache
    {
        public static readonly WaitForFixedUpdate WaitForFixedUpdate = new WaitForFixedUpdate();
    }

    public void CurtainOn()
    {
        StartCoroutine(CoCurtainOn());
    }

    public void CurtainOff()
    {
        StopAllCoroutines();
        StartCoroutine(CoCurtainOff());
    }

    IEnumerator CoCurtainOn()
    {
        while (true)
        {
            Bgm.Instance.VolumnDown(1);

            if (curtainPanel[0].transform.position.y > Screen.height + onValue)
            {
                curtainPanel[0].transform.position = new Vector3(curtainPanel[0].transform.position.x, curtainPanel[0].transform.position.y - 1, curtainPanel[0].transform.position.z);
            }

            if (curtainPanel[1].transform.position.y < -onValue)
            {
                curtainPanel[1].transform.position = new Vector3(curtainPanel[1].transform.position.x, curtainPanel[1].transform.position.y + 1, curtainPanel[1].transform.position.z);
            }

            yield return YieldInstructionCache.WaitForFixedUpdate;
        }
    }

    IEnumerator CoCurtainOff()
    {
        while (true)
        {
            if (curtainPanel[0].transform.position.y < Screen.height + offValue)
            {
                curtainPanel[0].transform.position = new Vector3(curtainPanel[0].transform.position.x, curtainPanel[0].transform.position.y + 1, curtainPanel[0].transform.position.z);
            }

            if (curtainPanel[1].transform.position.y > -offValue)
            {
                curtainPanel[1].transform.position = new Vector3(curtainPanel[1].transform.position.x, curtainPanel[1].transform.position.y - 1, curtainPanel[1].transform.position.z);
            }

            yield return YieldInstructionCache.WaitForFixedUpdate;
        }
    }
}
