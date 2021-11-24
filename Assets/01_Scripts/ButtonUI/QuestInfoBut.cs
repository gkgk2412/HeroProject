using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestInfoBut : MonoBehaviour
{
    public GameObject QuestInfo = null;
    public AudioSource audiosource;
    public AudioClip audioClip;

    public void ClickQuestInfo()
    {
        if(!GameManager.Instance.GetUiWindowCheck())
        {
            if (!audiosource.isPlaying)
                audiosource.PlayOneShot(audioClip, 1.0f);

            QuestInfo.SetActive(true);
            GameManager.Instance.PlayerStateChange("STOP");
            PlayerAnimationController.Instance.ChangeAnimationState("IDLE", false);
        }

        //UI창이 켜짐으로 바꿔줌.
        GameManager.Instance.SetUiWindowCheck(true);
    }

    public void OutQuestInfo()
    {
        if (!audiosource.isPlaying)
            audiosource.PlayOneShot(audioClip, 1.0f);

        //UI창이 꺼짐으로 바꿔줌.
        GameManager.Instance.SetUiWindowCheck(false);
        QuestInfo.SetActive(false);
        GameManager.Instance.PlayerStateChange("LIVE");
    }
}
