using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubQuestText : MonoBehaviour
{    
    [SerializeField]
    private Text subDescriptionText = null;

    [SerializeField]
    private Text subQ1 = null;
    [SerializeField]
    private Text subQ2 = null;
    [SerializeField]
    private Text subQ3 = null;

    public GameObject accBnt = null;
    public Text acceptText = null;

    private int currentClick = 0;

    public void Update()
    {
        if (currentClick == 0)
        {
            accBnt.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            acceptText.color = new Color(0, 0, 0, 0);
        }

        if(currentClick == 1 && !GameManager.Instance.GetsubQuestArray(0))
        {
            acceptText.text = "수락";
            acceptText.transform.parent.GetComponent<Button>().interactable = true;
        }

        if (currentClick == 1 && GameManager.Instance.GetsubQuestArray(0))
        {
            acceptText.text = "수락됨";
            acceptText.transform.parent.GetComponent<Button>().interactable = false;
        }

        if (currentClick == 2 && !GameManager.Instance.GetsubQuestArray(1))
        {
            acceptText.text = "수락";
            acceptText.transform.parent.GetComponent<Button>().interactable = true;
        }

        if (currentClick == 2 && GameManager.Instance.GetsubQuestArray(1))
        {
            acceptText.text = "수락됨";
            acceptText.transform.parent.GetComponent<Button>().interactable = false;
        }

        if (currentClick == 3 && !GameManager.Instance.GetsubQuestArray(2))
        {
            acceptText.text = "수락";
            acceptText.transform.parent.GetComponent<Button>().interactable = true;
        }

        if (currentClick == 3 && GameManager.Instance.GetsubQuestArray(2))
        {
            acceptText.text = "수락됨";
            acceptText.transform.parent.GetComponent<Button>().interactable = false;
        }
    }

    //버섯 text 클릭 시
    public void MushRoomClick()
    {
        subQ1.color = Color.red;
        subQ2.color = Color.white;
        subQ3.color = Color.white;

        accBnt.GetComponent<Image>().color = Color.white;
        acceptText.color = Color.black;

        string message = "거대 토끼 악당이 나타난 뒤로 얌전하던 버섯돌이들이 날뛰고 있어 ! \n<color=#ff0000ff>버섯돌이</color> <color=#0000ffff>3마리</color>를 해치워주면 <color=#0000ffff>3골드</color>를 줄게. \n어때, 해결해주겠어?";

        subDescriptionText.GetComponent<Text>().text = message;

        currentClick = 1;
    }

    //무 text 클릭 시
    public void radishClick()
    {
        subQ1.color = Color.white;
        subQ2.color = Color.red;
        subQ3.color = Color.white;

        accBnt.GetComponent<Image>().color = Color.white;
        acceptText.color = Color.black;

        string message = "오늘 저녁으로 무 조림을 해먹고싶은데, 거대 토끼 악당이 나타난 뒤로 무 몬스터를 잡기가 쉽지 않아졌어.\n누군가 <color=#ff0000ff>무 몬스터</color>를 <color=#0000ffff>3마리</color>만 잡아와준다면 무 값과 수고비까지 해서 넉넉하게 <color=#0000ffff>10골드</color>를 줄게.";

        subDescriptionText.GetComponent<Text>().text = message;

        currentClick = 2;
    }

    //수정골렘 text 클릭 시
    public void crystalClick()
    {
        subQ1.color = Color.white;
        subQ2.color = Color.white;
        subQ3.color = Color.red;

        accBnt.GetComponent<Image>().color = Color.white;
        acceptText.color = Color.black;

        string message = "나는 수정광산에서 일하던 광부야. 광산에서 일을 해야하는데, 거대 토끼 악당이 나타난 뒤로는 수정골렘이 늘어나서 너무 위험해졌어.\n<color=#ff0000ff>작은 수정골렘</color> <color=#0000ffff>5마리</color>만 퇴치해준다면  <color=#0000ffff>8골드</color>를 보상으로 줄게.";

        subDescriptionText.GetComponent<Text>().text = message;

        currentClick = 3;
    }

    public int GetCurrentNum()
    {
        return currentClick;
    }
}
