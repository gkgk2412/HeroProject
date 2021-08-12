using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubQuestBnt : MonoBehaviour
{
    public SubQuestText _subScr = null;
    public Text acceptText = null;


    public void AcceptSubQuest()
    {
        //서브퀘스트 수락함
        if(_subScr.GetCurrentNum() == 1)
        {
            GameManager.Instance.SetsubQuestArray(0, true);
            acceptText.text = "수락됨";
        }

        if (_subScr.GetCurrentNum() == 2)
        {
            GameManager.Instance.SetsubQuestArray(1, true);
            acceptText.text = "수락됨";
        }

        if (_subScr.GetCurrentNum() == 3)
        {
            GameManager.Instance.SetsubQuestArray(2, true);
            acceptText.text = "수락됨";
        }
    }
}
