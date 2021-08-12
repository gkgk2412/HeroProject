using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestInfoBut : MonoBehaviour
{
    public GameObject QuestInfo = null;


    public void ClickQuestInfo()
    {
        if(!GameManager.Instance.GetUiWindowCheck())
        {
            QuestInfo.SetActive(true);
            GameManager.Instance.PlayerStateChange("STOP");
            PlayerAnimationController.Instance.ChangeAnimationState("IDLE", false);
        }

        //UI창이 켜짐으로 바꿔줌.
        GameManager.Instance.SetUiWindowCheck(true);
    }

    public void OutQuestInfo()
    {       
        //UI창이 꺼짐으로 바꿔줌.
        GameManager.Instance.SetUiWindowCheck(false);
        QuestInfo.SetActive(false);
        GameManager.Instance.PlayerStateChange("LIVE");
    }
}
