using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowDestroy : MonoBehaviour
{
    public void DestroyArrow()
    {
        //가용 숫자가 2면 arrow 1을 모두 destroy
        if(PlayerControlManager.Instance.MyArrow == 2)
        {
            //하이라키의 arrow1개짜리를 모두 삭제
            GameObject _obj = GameObject.Find("spear(Clone)");

            Destroy(_obj);
        }
    }
}
