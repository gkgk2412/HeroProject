using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    private bool isClear;

    [SerializeField]
    private string title;

    public string wantedName;

    [SerializeField]
    private int takeGold;

    [SerializeField]
    private string description;

    [SerializeField]
    private CollectObjective[] collectObjectives;

    public QuestScript MyQuestScript { get; set; }


    private static Quest instance = null;

    // 인스턴스에 접근할 수 있는 프로퍼티, static으로 선언하여 다른 클래스에서 호출 가능함.
    public static Quest Instance
    {
        //get 으로 return된 instance (|| null) 를 받아옴
        get
        {
            if (null == instance)
                return null;

            return instance;
        }
    }


    public string MyTitle
    {
        get
        {
            return title;
        }

        set
        {
            title = value;
        }
    }
    
    public bool MyClear
    {
        get
        {
            return isClear;
        }

        set
        {
            isClear = value;
        }
    }
    
    public int MyTakeGold
    {
        get
        {
            return takeGold;
        }

        set
        {
            takeGold = value;
        }
    }

    public string MyDescription
    {
        get
        {
            return description;
        }

        set
        {
            description = value;
        }
    }

    public CollectObjective[] MyCollectObjectives
    {
        get
        {
            return collectObjectives;
        }
    }
}

[System.Serializable]
public abstract class Objective
{
    [SerializeField]
    private int amount;

    private int currentAmount;

    [SerializeField]
    private string type;

    public int MyAmount
    {
        get
        {
            return amount;
        }
    }

    public int MyCurrentAmount
    {
        get
        {
            return currentAmount;
        }

        set
        {
            currentAmount = value;
        }
    }

    public string MyType
    {
        get
        {
            return type;
        }

        set
        {
            type = value;
        }
    }
}

[System.Serializable]
public class CollectObjective : Objective
{
    private static CollectObjective instance;
    public static CollectObjective Instance => instance;

    public CollectObjective()
    {
        //자기 자신에 대한 정보를 static 형태의 instacne 변수에 저장하여 외부에서 접근이 쉽도록 함
        instance = this;
    }
}