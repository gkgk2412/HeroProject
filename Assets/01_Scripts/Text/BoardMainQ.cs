using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardMainQ : MonoBehaviour
{
    void Update()
    {
        //Main Quest를 받은 상태이면 
        if(GameManager.Instance.GetQuestCheck())
        {
            string message = "히어로 가문의 명예를 걸고 끔찍한 괴물 곰을 물리치세요!";

            this.GetComponent<Text>().text = message;
        }

        //Main Quest를 받지 않은 상태이면
        if (!GameManager.Instance.GetQuestCheck())
        {
            string message = "메인 퀘스트를 받지 않았습니다.";

            this.GetComponent<Text>().text = message;
        }
    }
}
