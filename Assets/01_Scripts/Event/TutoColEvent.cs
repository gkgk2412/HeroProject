using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoColEvent : MonoBehaviour
{
    DialogueManager theDM;

    private Animator _knightAnim;
    private Animator _knightAnim2;
    public GameObject MainCamera;
    public GameObject _knight;
    public GameObject _knight2;

    public AudioSource audiosource;
    public AudioClip audioClip;

    public BoxCollider _tutoBoxCol;

    private void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();
        _knightAnim = _knight.GetComponent<Animator>();
        _knightAnim2 = _knight2.GetComponent<Animator>();
    }

    public void tutoColEventFunc()
    {
        //메인 퀘스트를 받지 않았을 경우
        if(!GameManager.Instance.GetQuestCheck())
        {
            if (!audiosource.isPlaying)
                audiosource.PlayOneShot(audioClip, 1.0f);

            string message = "메인 퀘스트를 먼저 받아주세요!";

            FloatingTextManager.instance.CreateFloatingText(this.transform.position, message);

            MainCamera.GetComponent<CamShake>().InCameraShake(0.2f, 0.15f);

            //플레이어 멈추고, IDLE로 애니메이션 변경
            GameManager.Instance.PlayerStateChange("STOP");
            PlayerAnimationController.Instance.ChangeAnimationState("IDLE", false);

            //경비병 애니메이션 변경
            _knightAnim.SetBool("isAttackCol", true);
            _knightAnim2.SetBool("isAttackCol", true);

            //경비병의 칼 휘두르는 애니메이션 종료되면 idle로 다시 변경...
            Invoke("WaitAnim", 0.3f);

            GameManager.Instance.PlayerStateChange("LIVE");
        }

        //메인 퀘스트를 받았을 경우
        if (GameManager.Instance.GetQuestCheck())
        {
            //가로막는 콜라이더를 해제
            _tutoBoxCol.enabled = false;
        }
    }

    public void WaitAnim()
    {        
        _knightAnim.SetBool("isAttackCol", false);
        _knightAnim2.SetBool("isAttackCol", false);
    }
}
