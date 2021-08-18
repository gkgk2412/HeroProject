using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestAlarm : MonoBehaviour
{
    [SerializeField] private GameObject _AlarmPrefab;
    [SerializeField] private Transform _AlarmParent;

    public void GetQuestAlarm(Quest quest)
    {
        GameObject Alarm = Instantiate(_AlarmPrefab, _AlarmParent);

        string QuestAlr = "퀘스트 " + quest.MyTitle + " 수락!";

        Alarm.transform.GetChild(0).GetComponent<Text>().text = QuestAlr;
    }
}