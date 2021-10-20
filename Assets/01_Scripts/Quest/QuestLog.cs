using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class QuestLog : MonoBehaviour
{
    public int QuestGold;

    public UnityEvent QuestClearEvnet;
    public UnityEvent QuestNClearEvnet;

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

    public void UpdateSelected()
    {
        ShowDescription(selected);
    }

    public Quest GetSelectedQuest()
    {
        return selected;
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
            if(obj.MyType == quest.wantedName)
            {
                obj.MyCurrentAmount = MonsterDie.Instance.GetDieMonsterCount(quest.wantedName);

                //지정된 수만큼 몬스터 처치 시
                if(obj.MyCurrentAmount >= obj.MyAmount)
                {
                    obj.MyCurrentAmount = obj.MyAmount;
                    QuestGold = quest.MyTakeGold;
                    EventFunc_Clear();
                }
                else
                    EventFunc_N_Clear();
            }


            objectives += obj.MyType + " : " + obj.MyCurrentAmount + "/" + obj.MyAmount + "\n";
        }

        questDescription.text = string.Format("<b>{0}</b>\n\n<size=40>{1}</size>\n\n진행사항\n<size=40>{2}</size>", title, quest.MyDescription, objectives);
    }

    public void EventFunc_Clear()
    {
        QuestClearEvnet.Invoke();
    }

    public void EventFunc_N_Clear()
    {
        QuestNClearEvnet.Invoke();
    }
}
