using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    private bool isFlag = false;
    private bool isFlag2 = false;
    private bool isFlag3 = false;
    private bool isFlag4 = false;

    [SerializeField]
    private Quest[] quests;

    //Debugging
    [SerializeField]
    private QuestLog tmpLog;


    private void Update()
    {
        //Main Quest를 받은 상태이면 
        if (GameManager.Instance.GetQuestCheck() && !isFlag)
        {
            tmpLog.AcceptQuest(quests[0]);
            isFlag = true;
        }

        //버섯돌이 처치 서브퀘스트 받은 상태이면
        if (GameManager.Instance.GetsubQuestArray(0) && !isFlag2)
        {
            tmpLog.AcceptQuest(quests[1]);
            isFlag2 = true;
        }

        //무 몬스터 처치 서브퀘스트 받은 상태이면
        if (GameManager.Instance.GetsubQuestArray(1) && !isFlag3)
        {
            tmpLog.AcceptQuest(quests[2]);
            isFlag3 = true;
        }

        //작은수정골렘 처치 서브퀘스트 받은 상태이면
        if (GameManager.Instance.GetsubQuestArray(2) && !isFlag4)
        {
            tmpLog.AcceptQuest(quests[3]);
            isFlag4 = true;
        }
    }
}
