using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactiveCommand : CommandKey
{
    public int objType = 0;
    public Transform Player;
    public GameObject text;
    public GameObject _board;
    public float InteractiveLength;
    public float minLength;

    private bool isInteractNow = false;
    private bool isF_Btn_Clicked = false;


    internal static class YieldInstructionCache
    {
        public static readonly WaitForFixedUpdate WaitForFixedUpdate = new WaitForFixedUpdate();
    }

    // Start is called before the first frame update
    void Start()
    {
        if(objType ==0)
            _board.SetActive(false);

        StartCoroutine(InteractiveUI());
    }

    public interactiveCommand(MonoBehaviour _mono)
    {
        this.mono = _mono;
    }

    public override void Execute()
    {
        Interaction();
    }

    public void Interaction()
    {
        _board.SetActive(true);
    }

    private IEnumerator InteractiveUI()
    {
        //플레이어와 문사이의 거리를 저장할 float 형 변수
        float objLength = 0;

        while (true)
        {
            //플레이어와 이 스크립트가 들어있는 물체사이의 거리 측정
            objLength = (this.transform.position - Player.position).sqrMagnitude;

            //상호작용 거리 내에서만 이벤트 발생
            if (objLength < InteractiveLength)
            {
                //그 사이의 거리가 minLength보다 작다면, UI text 생성
                if (objLength < minLength)
                {
                    text.SetActive(true);
                }

                else
                {
                    text.SetActive(false);
                }
            }        

            yield return YieldInstructionCache.WaitForFixedUpdate;
        }
    }
}
