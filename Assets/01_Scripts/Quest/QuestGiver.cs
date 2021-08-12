using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    [SerializeField]
    private Quest[] quests;

    //Debugging
    [SerializeField]
    private QuestLog tmpLog;

    //Main Quest를 받은 상태이면 
    public void MainQuestEvent()
    {      
        tmpLog.AcceptQuest(quests[0]);
    }

    //버섯돌이 Quest
    public void SubQuest01Event()
    {
        tmpLog.AcceptQuest(quests[1]);
    }

    //무 몬스터 quest
    public void SubQuest02Event()
    {
        tmpLog.AcceptQuest(quests[2]);
    }

    //수정골렘 quest
    public void SubQuest03Event()
    {
        tmpLog.AcceptQuest(quests[3]);
    }
}
