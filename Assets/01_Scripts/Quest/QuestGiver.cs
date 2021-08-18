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

    [SerializeField] private QuestAlarm _questAlarm;

    //Main Quest를 받은 상태이면 
    public void MainQuestEvent()
    {        
        _questAlarm.GetQuestAlarm(quests[0]);
        tmpLog.AcceptQuest(quests[0]);
    }

    //버섯돌이 Quest
    public void SubQuest01Event()
    {
        _questAlarm.GetQuestAlarm(quests[1]);
        tmpLog.AcceptQuest(quests[1]);
    }

    //무 몬스터 quest
    public void SubQuest02Event()
    {
        _questAlarm.GetQuestAlarm(quests[2]);
        tmpLog.AcceptQuest(quests[2]);
    }

    //수정골렘 quest
    public void SubQuest03Event()
    {
        _questAlarm.GetQuestAlarm(quests[3]);
        tmpLog.AcceptQuest(quests[3]);
    }
}
