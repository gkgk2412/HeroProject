using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    private bool isGetQuest = false;         //왕에게서 받는 메인 퀘스트 받았는지 여부를 체크함
    private bool isUiWindowOpen = false;     //UI 윈도우가 띄워져있는지 (한 윈도우가 띄워져있으면 다른 윈도우는 나오지 않도록 한다.)
    private bool[] subQuestArray = { false, false, false }; 

    // 인스턴스에 접근할 수 있는 프로퍼티, static으로 선언하여 다른 클래스에서 호출 가능함.
    public static GameManager Instance
    {
        //get 으로 return된 instance (|| null) 를 받아옴
        get
        {
            if (null == instance)
                return null;

            return instance;
        }
    }


    void Awake()
    {
        if (null == instance)
        {
            //이 클래스의 인스턴스가 탄생했을 때 전역변수 instace에 게임매니저 인스턴스가 담겨있지 않다면, 자신을 넣어준다.
            instance = this;

            //Scene 이동 시 파괴되지 않게함
            //this.gameObject 의 의미 -> 이 스크립트가 컴포넌트로서 붙어있는 Hierarchy상의 게임오브젝트를 의미
            DontDestroyOnLoad(this.gameObject);
        }

        else
        {
            // Scene 이동 시, 전역변수인 instance에 새 인스턴스가 존재한다면 자신(새로운 씬의 GameControlManager)을 삭제해준다.
            Destroy(this.gameObject);
        }
    }

    enum PLAYERSTATE
    {
        LIVE = 0,
        LIVE_ATTACK,
        STOP,
        DIE
    }

    [Header("플레이어 상태")]
    [SerializeField]
    private PLAYERSTATE playerState;


    //플레이어 상태리턴(현재의 플레이어상태를 STRING형으로 참조함)
    public string CheckPlayerState()
    {
        string playerStateName;

        switch (playerState)
        {
            case PLAYERSTATE.LIVE:
                {
                    playerStateName = "LIVE";
                    return playerStateName;
                }

            case PLAYERSTATE.LIVE_ATTACK:
                {
                    playerStateName = "LIVE_ATTACK";
                    return playerStateName;
                }

            case PLAYERSTATE.STOP:
                {
                    playerStateName = "STOP";
                    return playerStateName;
                }

            case PLAYERSTATE.DIE:
                {
                    playerStateName = "DIE";
                    return playerStateName;
                }

            default:
                {
                    playerStateName = "ERROR";
                    return playerStateName;
                }
        }
    }

    //플레이어 상태 변화
    public void PlayerStateChange(string playerStateName)
    {
        switch (playerStateName)
        {
            case "LIVE":
                {
                    playerState = PLAYERSTATE.LIVE;
                    break;
                } 
            
            case "LIVE_ATTACK":
                {
                    playerState = PLAYERSTATE.LIVE_ATTACK;
                    break;
                }

            case "DIE":
                {
                    playerState = PLAYERSTATE.DIE;
                    break;
                }

            case "STOP":
                {
                    playerState = PLAYERSTATE.STOP;
                    break;
                }

            default:
                {
                    Debug.LogError("GameManagerError - enum으로 지정된 변수명을 입력해주세요.");
                    break;
                }
        }
    }

    public bool GetQuestCheck()
    {
        return isGetQuest;
    }

    public void SetQusetCheck(bool isbool)
    {
        isGetQuest = isbool;
    }
    
    public bool GetUiWindowCheck()
    {
        return isUiWindowOpen;
    }

    public void SetUiWindowCheck(bool isbool)
    {
        isUiWindowOpen = isbool;
    }

    public bool GetsubQuestArray(int index)
    {
        return subQuestArray[index];
    }

    public void SetsubQuestArray(int index, bool isbool)
    {
        subQuestArray[index] = isbool;
    }
}
