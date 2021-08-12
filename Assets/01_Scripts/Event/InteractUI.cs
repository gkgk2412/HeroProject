using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractUI : MonoBehaviour
{
    DialogueManager theDM;

    public int objType = 0;

    public Transform Player;
    public GameObject text;
    public GameObject _board;
    public GameObject _king;

    public float InteractiveLength;
    public float minLength;

    private bool isEventOn = false;
    private bool isRange = false;

    public UnityEvent _mainQEvent;

    internal static class YieldInstructionCache
    {
        public static readonly WaitForFixedUpdate WaitForFixedUpdate = new WaitForFixedUpdate();
    }

    // Start is called before the first frame update
    void Start()
    {
        if(objType == 1)
        {
            _king = this.gameObject;
        }

        theDM = FindObjectOfType<DialogueManager>();
        StartCoroutine(InteractiveUI());
    }

    private IEnumerator InteractiveUI()
    {
        //플레이어와 물체사이의 거리를 저장할 float 형 변수
        float objLength = 0;

        while (true)
        {
            //플레이어와 이 스크립트가 들어있는 물체사이의 거리 측정
            objLength = (this.transform.position - Player.position).sqrMagnitude;

            //상호작용 거리 내에서만 이벤트 발생, + 이벤트가 켜지지 않았을 경우에
            if (objLength < InteractiveLength && !isEventOn)
            {
                //그 사이의 거리가 minLength보다 작다면, UI text 생성
                if (objLength < minLength)
                {
                    isRange = true;
                    text.SetActive(true);
                }

                else
                {
                    isRange = false;
                    text.SetActive(false);
                }
            }

            yield return YieldInstructionCache.WaitForFixedUpdate;
        }
    }

    public void OnEventBoard()
    {
        //UI창이 켜짐으로 바꿔줌.
        GameManager.Instance.SetUiWindowCheck(true);

        _board.SetActive(true);
        text.SetActive(false);
        isEventOn = true;
    }

    public void OffEventBoard()
    {       
        //UI창이 꺼짐으로 바꿔줌.
        GameManager.Instance.SetUiWindowCheck(false);

        _board.SetActive(false);
        isEventOn = false;
    }

    public void OnEventKing()
    {
        //UI창이 켜짐으로 바꿔줌.
        GameManager.Instance.SetUiWindowCheck(true);

        EventMain();
        GameManager.Instance.SetQusetCheck(true);
        theDM.showDialogue(_king.transform.GetComponent<InteractiveDialogueEvent>().GetDialogue());
        text.SetActive(false);
        isEventOn = true;
    }

    public void OffEventKing()
    {
        //UI창이 꺼짐으로 바꿔줌.
        GameManager.Instance.SetUiWindowCheck(false);
    }

    public bool RangeCheck()
    {
        if (GameManager.Instance.GetQuestCheck() && objType == 1)
        {
            return false;
        }
        else
            return isRange;
    }

    public void EventMain()
    {
        _mainQEvent.Invoke();
    }
}
