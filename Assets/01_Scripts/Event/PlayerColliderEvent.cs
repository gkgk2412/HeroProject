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

    public GameObject _gameEnd;

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
        
        if (other.gameObject.tag == "potal")
        {
            StartCoroutine(VolumeDown());
            _gameEnd.SetActive(true);
            FadeController.Instance.FadeIn(3.0f, null);
            GameManager.Instance.PlayerStateChange("STOP");
            PlayerAnimationController.Instance.ChangeAnimationState("IDLE", false);
        }
    }

    private IEnumerator VolumeDown()
    {
        while(true)
        {
            Bgm.Instance.VolumnDown(3);

            yield return null;
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
