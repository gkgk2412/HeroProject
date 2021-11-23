using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerColliderEvent : MonoBehaviour
{
    public UnityEvent _tutoEvnet;
    public UnityEvent _bossEvnet;
    public UnityEvent _skillThree;
    public UnityEvent _skillThreeOut;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "TutoEnterCol")
        {
            tutoColEvent();
        }
        
        if (other.gameObject.name == "BossEventCol")
        {
            BossColEvent();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //보스 3번째 스킬 범위
        if (other.gameObject.tag == "Range")
        {
            SkillThreeEvent();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //보스 3번째 스킬 범위
        if (other.gameObject.tag == "Range")
        {
            SkillThreeEventOut();
        }
    }

    public void tutoColEvent()
    {
        _tutoEvnet.Invoke();
    }

    public void BossColEvent()
    {
        _bossEvnet.Invoke();
    }

    public void SkillThreeEvent()
    {
        _skillThree.Invoke();
    }

    public void SkillThreeEventOut()
    {
        _skillThreeOut.Invoke();
    }
}
