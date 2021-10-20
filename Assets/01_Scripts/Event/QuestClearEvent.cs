using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestClearEvent : MonoBehaviour
{
    public GameObject _clearTxt;
    public GameObject _GoldBnt;

    private Quest quest;

    private int QuestClear = 0;

    public void Start()
    {
        _clearTxt.SetActive(false);
        _GoldBnt.SetActive(false);
    }

    public void ClearTextOn()
    {
        _clearTxt.SetActive(true);
        _GoldBnt.SetActive(true);

    } 
    
    public void ClearTextOff()
    {
        _clearTxt.SetActive(false);
        _GoldBnt.SetActive(false);
    }

    public void GoldBntClick()
    {            
        //버튼이 눌러지면 현재 선택된 퀘스트가 무엇인지 체크한다.
        quest = QuestLog.Instance.GetSelectedQuest();

        //해당 퀘스트가 깨진 적이 없으면
        if (!quest.MyClear)
        {
            //해당 퀘스트가 가진 골드..
            PlayerControlManager.Instance.MyGold += QuestLog.Instance.QuestGold;

            quest.MyClear = true;
            ++QuestClear;
        }


        //서브퀘스트 3개를 모두 깼으면
        if(QuestClear == 3)
        {
            //길 생성
            Debug.Log("Quest Clear!");
        }
    }
}
