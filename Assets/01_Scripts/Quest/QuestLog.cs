using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestLog : MonoBehaviour
{
    [SerializeField]
    private GameObject questPrefab;

    [SerializeField]
    private Transform questParent;

    private Quest selected;

    [SerializeField]
    private Text questDescription;

    [SerializeField]
    private Text subQuestDescription;

    private static QuestLog instance;

    public static QuestLog Instance
    {
        get
        {
            if(instance== null)
            {
                instance = FindObjectOfType<QuestLog>();
            }
            return instance;
        }
    }

    public void AcceptQuest(Quest quest)
    {
        GameObject go = Instantiate(questPrefab, questParent);

        QuestScript qs = go.GetComponent<QuestScript>();
        quest.MyQuestScript = qs;
        qs.MyQuest = quest;

        go.GetComponent<Text>().text = quest.MyTitle;
    }

    public void ShowDescription(Quest quest)
    {
        if (selected != null)
        {
            selected.MyQuestScript.DeSelect();
        }

        string objectives = string.Empty;

        selected = quest;

        string title = quest.MyTitle;

        foreach (Objective obj in quest.MyCollectObjectives)
        {
            objectives += obj.MyType + " : " + obj.MyCurrentAmount + "/" + obj.MyAmount + "\n";
        }

        //questDescription.text = string.Format("<b>{0}</b>\n\n<size=40>{1}</size>", title, quest.MyDescription);
        questDescription.text = string.Format("<b>{0}</b>\n\n<size=40>{1}</size>\n\n진행사항\n<size=40>{2}</size>", title, quest.MyDescription, objectives);
    }
}
