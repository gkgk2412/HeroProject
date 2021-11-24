using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class QuestClearEvent : MonoBehaviour
{
    public AudioSource audiosource;
    public AudioClip audioClip;

    public AudioSource audiosource02;
    public AudioClip audioClip02;

    public UnityEvent cameraEventon;
    public UnityEvent cameraEventoff;
    public UnityEvent Uioff;

    public GameObject _clearTxt;
    public GameObject _GoldBnt;
    public GameObject[] BossRoomGround = new GameObject[7];
    public GameObject BossCol;

    private Quest quest;

    private int QuestClear = 0;

    //코루틴 yield 캐싱
    internal static class YieldInstructionCache
    {
        public static readonly WaitForFixedUpdate WaitForFixedUpdate = new WaitForFixedUpdate();
        public static readonly WaitForSeconds WaitFordotThreeSecond = new WaitForSeconds(0.4f);
        public static readonly WaitForSeconds WaitFordotsevenSecond = new WaitForSeconds(1.3f);
        public static readonly WaitForSeconds WaitFordoteightSecond = new WaitForSeconds(1.8f);
    }

    public void Start()
    {
        _clearTxt.SetActive(false);
        _GoldBnt.SetActive(false);

        //보스방 초기화
        BossRoomGround[0].SetActive(false);
        BossRoomGround[1].SetActive(false);
        BossRoomGround[2].SetActive(false);
        BossRoomGround[3].SetActive(false);
        BossRoomGround[4].SetActive(false);
        BossRoomGround[5].SetActive(false);
        BossRoomGround[6].SetActive(false);
    }

    public void ClearTextOn()
    {
        _clearTxt.SetActive(true);
        _GoldBnt.SetActive(true);
    } 
    
    public void ClearTextOff()
    {
        _clearTxt.SetActive(false);
        _GoldBnt.SetActive(false);
    }

    public void GoldBntClick()
    {            
        //버튼이 눌러지면 현재 선택된 퀘스트가 무엇인지 체크한다.
        quest = QuestLog.Instance.GetSelectedQuest();

        //해당 퀘스트가 깨진 적이 없으면
        if (!quest.MyClear)
        {
            audiosource.PlayOneShot(audioClip, 1.0f);

            //해당 퀘스트가 가진 골드..
            PlayerControlManager.Instance.MyGold += QuestLog.Instance.QuestGold;

            quest.MyClear = true;
            ++QuestClear;
        }

        //서브퀘스트 3개를 모두 깼으면
        if(QuestClear == 3)
        {
            //UI삭제
            UIOff();

            //길 생성
            StartCoroutine(BossRoomShow());
        }
    }

    private IEnumerator BossRoomShow()
    {
        int i = -1;
        CameraEventOn();

        if (!audiosource02.isPlaying)
            audiosource02.PlayOneShot(audioClip02, 1.0f);

        //보스방 막는 콜라이더 삭제
        BossCol.SetActive(false);

        while (i < 6)
        {
            ++i;

            if (i == 6)
            {
                yield return YieldInstructionCache.WaitFordotsevenSecond;
            }

            if (i < 6)
            {
                yield return YieldInstructionCache.WaitFordotThreeSecond;
            }

            BossRoomGround[i].SetActive(true);

            if (i == 6)
            {
                yield return YieldInstructionCache.WaitFordoteightSecond;
                CameraEventOff();
            }
        }
    }

    public void CameraEventOn()
    {
        cameraEventon.Invoke();
    } 
    
    public void CameraEventOff()
    {
        cameraEventoff.Invoke();
    }
    
    public void UIOff()
    {
        Uioff.Invoke();
    }
}
