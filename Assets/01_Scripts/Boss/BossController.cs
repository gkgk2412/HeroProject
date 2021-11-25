using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossController : Boss
{
    public AudioSource audiosource;
    public AudioSource audiosource02;
    public AudioSource audiosource03;
    public AudioSource audiosource04;
    public AudioClip audioClip;
    public AudioClip audioClip02;
    public AudioClip audioClip03;
    public AudioClip audioClip04;
    public AudioClip audioClip05;
    public AudioClip audioClip06;

    private Animator _bossAnimator;
    private Rigidbody rb;

    public GameObject MainCamera;
    public GameObject EffectDust;

    public GameObject bossUI;
    public GameObject bossEventPanel;
    public GameObject Projector;
    public GameObject Projector02;
    public GameObject RealRange;
    public BoxCollider bossRoomWall;

    private bool isGround = false;
    private bool isSkill2_do = false;
    private bool isSkill3_do = false;
    private bool isBossDie = false;

    private bool isSkill3_Range = false;
    private bool onceDie = false;
    private bool onceRun = false;


    #region Internal-------------------------- # Please close # --------------------------
    private enum StateFlow
    {
        ENTER,
        UPDATE,
        EXIT
    }

    private void SetStartState(BossState startState)
    {
        currentState = startState;

        switch (currentState)
        {
            case BossState.IDLE: IDLE(StateFlow.ENTER); break;
            case BossState.SKILL01_ROCK_THROW: SKILL01_ROCK_THROW(StateFlow.ENTER); break;
            case BossState.SKILL02_BODY_BlOW: SKILL02_BODY_BlOW(StateFlow.ENTER); break;
            case BossState.SKILL03_JUMP: SKILL03_JUMP(StateFlow.ENTER); break;
            case BossState.DIE: DIE(StateFlow.ENTER); break;
        }
    }

    private void CommonUpdate()
    {
        bool once = false;

        if (this.transform.position.y <= 9.8f)
            this.transform.position = new Vector3(this.transform.position.x, 9.8f, this.transform.position.z);

        //보스의 체력이 0이면
        if (b_CurrentHp <= 0)
        {
            _bossAnimator.SetBool("isDie", true);
            ChangeState(BossState.DIE);
        }

        //땅에 닿으면
        if (isGround)
        {
            if (!once)
            {
                Bgm.Instance.ChangeBgm(1);
                once = true;
            }
            //약 1초 후에 보스 체력바 등 UI 띄우기
            Invoke("ShowUI", 1.0f);
        }
    }

    void Update()
    {
        CommonUpdate();

        switch (currentState)
        {
            case BossState.IDLE: IDLE(StateFlow.UPDATE); break;
            case BossState.SKILL01_ROCK_THROW: SKILL01_ROCK_THROW(StateFlow.UPDATE); break;
            case BossState.SKILL02_BODY_BlOW: SKILL02_BODY_BlOW(StateFlow.UPDATE); break;
            case BossState.SKILL03_JUMP: SKILL03_JUMP(StateFlow.UPDATE); break;
            case BossState.DIE: DIE(StateFlow.UPDATE); break;
        }
    }

    private void ChangeState(BossState nextState)
    {
        switch (this.currentState)
        {
            case BossState.IDLE: IDLE(StateFlow.EXIT); break;
            case BossState.SKILL01_ROCK_THROW: SKILL01_ROCK_THROW(StateFlow.EXIT); break;
            case BossState.SKILL02_BODY_BlOW: SKILL02_BODY_BlOW(StateFlow.EXIT); break;
            case BossState.SKILL03_JUMP: SKILL03_JUMP(StateFlow.EXIT); break;
            case BossState.DIE: DIE(StateFlow.EXIT); break;
        }

        this.currentState = nextState;

        switch (this.currentState)
        {
            case BossState.IDLE: IDLE(StateFlow.ENTER); break;
            case BossState.SKILL01_ROCK_THROW: SKILL01_ROCK_THROW(StateFlow.ENTER); break;
            case BossState.SKILL02_BODY_BlOW: SKILL02_BODY_BlOW(StateFlow.ENTER); break;
            case BossState.SKILL03_JUMP: SKILL03_JUMP(StateFlow.ENTER); break;
            case BossState.DIE: DIE(StateFlow.ENTER); break;
        }
    }

    #endregion;

    // Start is called before the first frame update
    void Start()
    {
        _bossAnimator = this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody>();

        bossUI.SetActive(false);

        b_CurrentHp = b_hp;
    }

    private void IDLE(StateFlow stateFlow)
    {
        if (isGround)
        {
            switch (stateFlow)
            {
                case StateFlow.ENTER:
                    {
                        _bossAnimator.SetBool("isIdle", true);
                        _bossAnimator.SetBool("isThrow", false);
                        _bossAnimator.SetBool("isBodyBlow", false);
                        _bossAnimator.SetBool("isJump", false);
                    }
                    break;

                case StateFlow.UPDATE:
                    {
                        rb.constraints = RigidbodyConstraints.None | RigidbodyConstraints.FreezeRotation;

                        isSkill2_do = false;
                        isSkill3_do = false;
                        onceRun = false;

                        if (isGround)
                        {
                            LookPlayer();
                            StartCoroutine(Think());
                        }                        
                    }
                    break;

                case StateFlow.EXIT:
                    {
                    }
                    break;
            }
        }
    }

    private void SKILL01_ROCK_THROW(StateFlow stateFlow)
    {
        switch (stateFlow)
        {
            case StateFlow.ENTER:
                {
                    StopAllCoroutines();

                    _bossAnimator.SetBool("isIdle", false);
                    _bossAnimator.SetBool("isThrow", true);

                    //스킬실행
                    BezierCurve.Instance.SkillRock();

                    if (!audiosource.isPlaying)
                        audiosource.PlayOneShot(audioClip, 1.0f);
                }
                break;

            case StateFlow.UPDATE:
                {
                    LookPlayer();

                    //idle로 복귀
                    ChangeState(BossState.IDLE);
                }
                break;

            case StateFlow.EXIT:
                {
                }
                break;
        }
    }

    private void SKILL02_BODY_BlOW(StateFlow stateFlow)
    {
        switch (stateFlow)
        {
            case StateFlow.ENTER:
                {
                    StopAllCoroutines();
                }
                break;

            case StateFlow.UPDATE:
                {
                    rb.constraints = RigidbodyConstraints.FreezePositionY;

                    //1초 동안 이펙트 등 띄우고 다 띄워지면 아래부분 실행하기
                    //보스가 현재 바라보고 있는 위치와 그 끝.. 직선으로.. 이펙트 표시
                    _bossAnimator.SetBool("isIdle", false);
                    _bossAnimator.SetBool("isBodyBlow", true);

                    if(!isSkill2_do)
                    {
                        //범위를 보여준다.
                        Projector.SetActive(true);

                        Invoke("Skill02", 1.0f);
                    }

                    //직선으로 뛰기
                    if(isSkill2_do)
                    {
                        Vector3 movement = transform.forward * moveSpeed * Time.deltaTime;
                        rb.MovePosition(transform.position + movement);
                        
                        //범위 끈다.
                        Projector.SetActive(false);

                        if (!audiosource.isPlaying && !onceRun)
                        {
                            audiosource.PlayOneShot(audioClip03, 1.0f);
                            onceRun = true;
                        }
                    }                    
                }
                break;

            case StateFlow.EXIT:
                {
                }
                break;
        }
    }

    private void SKILL03_JUMP(StateFlow stateFlow)
    {
        switch (stateFlow)
        {
            case StateFlow.ENTER:
                {
                    StopAllCoroutines();
                }
                break;

            case StateFlow.UPDATE:
                {
                    _bossAnimator.SetBool("isIdle", false);
                    _bossAnimator.SetBool("isJump", true);

                    //실제로 위로 점프
                    if (!isSkill3_do)
                    {
                        Invoke("Skill03", 0.8f);
                    }
                }
                break;

            case StateFlow.EXIT:
                {
                }
                break;
        }
    }

    private void DIE(StateFlow stateFlow)
    {
        switch (stateFlow)
        {
            case StateFlow.ENTER:
                {
                    if (!audiosource04.isPlaying && !onceDie)
                    {
                        audiosource04.PlayOneShot(audioClip06, 2.0f);
                        onceDie = true;
                    }
                    
                    MonsterDie.Instance.UpdateDictionary(MonsterDie.Instance.DieMonsterDic, this.gameObject.name, 1);
                    QuestLog.Instance.UpdateSelected();
                    isBossDie = true;

                    Invoke("SoundChange", 2.0f);
                }
                break;

            case StateFlow.UPDATE:
                {
                    Bgm.Instance.VolumnDown(2);
                    _CameraController.MyCameraBoss = false;
                    bossRoomWall.isTrigger = true;
                    
                    Destroy(this.gameObject, 5.0f);
                    bossEventPanel.SetActive(false);
                    bossUI.SetActive(false);
                }
                break;

            case StateFlow.EXIT:
                {
                }
                break;
        }
    }

    private void ShowUI()
    {
        if(!isBossDie)
            bossUI.SetActive(true);
    }

    //subCamera.cs에서 event를 통해서 호출
    public void setGround()
    {
        isGround = true;
    }

    private void LookPlayer()
    {
        //계속 플레이어 방향 쪽으로 바라보기
        Vector3 dir = (Player.position - this.transform.position);

        var rot = Quaternion.LookRotation(dir);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rot, Time.deltaTime * rotSpeed);
    }

    private void Skill02()
    {
        isSkill2_do = true;
    }   
    
    private void Skill03()
    {
        this.GetComponent<BoxCollider>().enabled = false;
        rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        isSkill3_do = true;

        //범위 보여주기
        Projector02.SetActive(true);

        //실제 범위 옮기기
        RealRange.transform.position = new Vector3(this.transform.position.x, realRange_yPos, this.transform.position.z);

        //2초뒤 아주 빠르게 낙하
        Invoke("BossDown", 2.0f);
    }

    private void BossDown()
    {
        this.GetComponent<BoxCollider>().enabled = true;

        //범위 끄기
        Projector02.SetActive(false);

        rb.AddForce(Vector3.down * jumpPower * 2, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(isSkill2_do && other.gameObject.tag == "StopBoss")
        {
            //idle로 복귀
            ChangeState(BossState.IDLE);
        }

        if (isSkill2_do && other.gameObject.tag == "StopBoss_damage")
        {
            if (!audiosource03.isPlaying)
                audiosource03.PlayOneShot(audioClip05, 3.0f); 

            //idle로 복귀
            ChangeState(BossState.IDLE);

            DmgAttack(20.0f);
        }

        if (other.gameObject.tag == "spear" || other.gameObject.tag == "spear2" || other.gameObject.tag == "spear3" || other.gameObject.tag == "spear4" || other.gameObject.tag == "spear5")
        {
            audiosource02.PlayOneShot(audioClip04, 0.7f);

            SetDamage(1.0f);
            boss_hp.Instance._HPBar.fillAmount = (float)b_CurrentHp / b_hp;
        }

        if (other.gameObject.tag == "Bottom" && isSkill3_do)
        {
            //idle로 복귀
            ChangeState(BossState.IDLE);

            //땅에 닿으면 진동
            MainCamera.GetComponent<CamShake>().InCameraShake(0.5f, 0.3f);

            if (!audiosource.isPlaying)
                audiosource.PlayOneShot(audioClip02, 1.0f);

            EffectDust.transform.position = this.transform.position;
            EffectDust.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
            EffectDust.transform.GetChild(1).GetComponent<ParticleSystem>().Play();

            //플레이어가 범위안에 있으면 데미지 + 튕겨나가기
            if (isSkill3_Range)
            {
                DmgAttack(30.0f);
                isSkill3_Range = false;

                Player.transform.Translate(Vector3.up * Time.deltaTime * 50.0f, Camera.main.transform);
            }
        }
    }

    public void DmgExplo()
    {
        isSkill3_Range = true;
    }
    
    public void DmgExploOut()
    {
        isSkill3_Range = false;
    }

    private IEnumerator Think()
    {
        yield return new WaitForSeconds(3.0f);

        int randAction = Random.Range(0, 5);

        switch(randAction)
        {
            //돌 던지기
            case 0:
            case 1:
                ChangeState(BossState.SKILL01_ROCK_THROW);
                break;

            //몸통박치기
            case 2:
            case 3:
                ChangeState(BossState.SKILL02_BODY_BlOW);
                break;

            //점프 공격
            case 4:
                ChangeState(BossState.SKILL03_JUMP);
                break;
        }
    }

    private void SoundChange()
    {
        Bgm.Instance.ChangeBgm(2);
    }
}
