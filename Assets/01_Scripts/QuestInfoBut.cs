using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestInfoBut : MonoBehaviour
{
    public GameObject QuestInfo = null;


    public void ClickQuestInfo()
    {
        QuestInfo.SetActive(true);
    }

    public void OutQuestInfo()
    {
        QuestInfo.SetActive(false);
    }
}
