using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SubQuestBnt : MonoBehaviour
{
    public SubQuestText _subScr = null;
    public Text acceptText = null;

    public UnityEvent _subQuest01Event;
    public UnityEvent _subQuest02Event;
    public UnityEvent _subQuest03Event;

    public AudioSource audiosource;
    public AudioClip audioClip;

    public void AcceptSubQuest()
    {
        //서브퀘스트 수락함
        if(_subScr.GetCurrentNum() == 1)
        {
            EventSub01();
            GameManager.Instance.SetsubQuestArray(0, true);

            audiosource.PlayOneShot(audioClip, 1.0f);
        }

        if (_subScr.GetCurrentNum() == 2)
        {
            EventSub02();
            GameManager.Instance.SetsubQuestArray(1, true);

            audiosource.PlayOneShot(audioClip, 1.0f);
        }

        if (_subScr.GetCurrentNum() == 3)
        {
            EventSub03();
            GameManager.Instance.SetsubQuestArray(2, true);

            audiosource.PlayOneShot(audioClip, 1.0f);
        }
    }

    public void EventSub01()
    {
        _subQuest01Event.Invoke();
    }

    public void EventSub02()
    {
        _subQuest02Event.Invoke();
    }

    public void EventSub03()
    {
        _subQuest03Event.Invoke();
    }
}
