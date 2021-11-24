using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestScript : MonoBehaviour
{
    public AudioSource audiosource;
    public AudioClip audioClip;

    public Quest MyQuest { get; set; }

    public void Select()
    {
        audiosource.PlayOneShot(audioClip, 1.0f);
        GetComponent<Text>().color = Color.red;
        QuestLog.Instance.ShowDescription(MyQuest);
    }

    public void DeSelect()
    {
        GetComponent<Text>().color = Color.black;
    }
}
