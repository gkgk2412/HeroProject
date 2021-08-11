using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    private bool isFlag = false;

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
    }
}
