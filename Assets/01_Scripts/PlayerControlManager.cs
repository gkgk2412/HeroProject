using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlManager: MonoBehaviour
{
    // 현재 캐릭터가 가지고있는 캐릭터 컨트롤러 콜라이더.
    private CharacterController controller;
    private Animator _charAnim;

    private float horizontalMove;
    private float verticalMove;

    /*--------------------------------------------캐릭터 속성값--------------------------------------------------*/
    [Header("캐릭터 속성")]
    private Vector3 MoveDir;                                  // 캐릭터의 움직이는 방향.

    [SerializeField] private float speed;                     // 캐릭터에게 실제로 적용되는 스피드
    [SerializeField] private float moveSpeed;                 // 캐릭터 움직임 스피드.
    [SerializeField] private float runSpeed;                  // 캐릭터 달리기 스피드.
    [SerializeField] private float jumpSpeed;                 // 캐릭터 점프 힘.
    [SerializeField] private float gravity;                   // 캐릭터에게 작용하는 중력.
    [SerializeField] private float rotTime;                   // 회전시간
    

    public float curStamina;                                  // 캐릭터의 스태미나
    /*-----------------------------------------------------------------------------------------------------------*/


    enum PLAYERSTATE
    {
        IDLE = 0,
        WALK,
        JUMP,
        RUN,
        ATTACK
    }

    PLAYERSTATE pState;


    private static PlayerControlManager instance = null;

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

    void Start()
    {
        speed = moveSpeed;
        MoveDir = Vector3.zero;
        controller = GetComponent<CharacterController>();
        _charAnim = GetComponent<Animator>();
    }

    void Update()
    {
        //방향키가 아닌 WSAD는 return
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            return;

        // 현재 캐릭터가 땅에 있는가?
        if (controller.isGrounded)
        {
            ChangeAnimationState(PLAYERSTATE.JUMP, false);
            
            Move();
            Jump();
            Run();
        }

        //스태미너를 다 사용했다면
        if(curStamina <= 0)
        {
            //스테미너 올라가기
            if (curStamina <= 100)
                curStamina += 0.5f;

            speed = moveSpeed;
            _charAnim.SetFloat("MoveAniSpeed", 1.0f);
        }

        Rotation();

        // 캐릭터에 중력 적용.
        MoveDir.y -= gravity * Time.deltaTime;

        // 캐릭터 움직임.
        controller.Move(MoveDir * Time.deltaTime);

        //조금이라도 움직임이 있을 경우 애니메이션 재생
        ChangeAnimationState(PLAYERSTATE.WALK, true);
    }

    // 게임 매니저 인스턴스에 접근할 수 있는 프로퍼티, static으로 선언하여 다른 클래스에서 호출 가능함.
    public static PlayerControlManager Instance
    {
        //get 으로 return된 instance (|| null) 를 받아옴
        get
        {
            if (null == instance)
                return null;

            return instance;
        }
    }


    void Move()
    {
        //player 상하좌우키 입력
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");

        // 위, 아래 움직임 셋팅. 대각선 이동 속도맞추기 위해 정규화
        MoveDir = new Vector3(horizontalMove, 0, verticalMove).normalized;

        MoveDir *= speed;
    }

    void Run()
    {
        //왼쪽 shift 키를 누르면 속도 올리기
        if(Input.GetKey(KeyCode.LeftShift))
        {
            speed = runSpeed;
            _charAnim.SetFloat("MoveAniSpeed", 1.3f);

            //스테미너 줄어들기
            if (curStamina >= 0)
                curStamina -= 1;
        }

        else
        {
            speed = moveSpeed;
            _charAnim.SetFloat("MoveAniSpeed", 1.0f);

            //스테미너 올라가기
            if(curStamina <= 100)
                curStamina += 0.5f;
        }
    }

    void Rotation()
    {
        //오른쪽
        if (Input.GetAxisRaw("Horizontal") >= 1.0f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), rotTime * Time.deltaTime);
        }

        //왼쪽
        if (Input.GetAxisRaw("Horizontal") <= -1.0f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), rotTime * Time.deltaTime);
        }

        //위
        if (Input.GetAxisRaw("Vertical") >= 1.0f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), rotTime * Time.deltaTime);
        }

        //아래
        if (Input.GetAxisRaw("Vertical") <= -1.0f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), rotTime * Time.deltaTime);
        }
    }

    void Jump()
    {
        // 캐릭터 점프
        if (Input.GetButton("Jump"))
        {
            MoveDir.y = jumpSpeed;
            ChangeAnimationState(PLAYERSTATE.JUMP, true);
        }
    }

    void ChangeAnimationState(PLAYERSTATE pState, bool isbool)
    {
        switch(pState)
        {
            case PLAYERSTATE.JUMP:
                {
                    _charAnim.SetBool("isJump", isbool);
                    break;
                }
            case PLAYERSTATE.WALK:
                {
                    _charAnim.SetFloat("Speed", MoveDir.magnitude);
                    break;
                }
        }
    }
}