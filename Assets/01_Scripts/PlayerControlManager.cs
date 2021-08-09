using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlManager: MonoBehaviour
{
    // 현재 캐릭터가 가지고있는 캐릭터 컨트롤러 콜라이더.
    private CharacterController controller;

    private float horizontalMove;
    private float verticalMove;

    /*--------------------------------------------캐릭터 속성값--------------------------------------------------*/
    public float speed;                     // 캐릭터에게 실제로 적용되는 스피드
    public float moveSpeed;                 // 캐릭터 움직임 스피드.
    public float runSpeed;                  // 캐릭터 달리기 스피드.
    public float gravity;                   // 캐릭터에게 작용하는 중력.
    public float rotTime;                   // 회전시간
    public float curStamina;                // 캐릭터의 스태미나
    public float jumpSpeed;                 // 캐릭터 점프 힘.

    public Vector3 MoveDir;                 // 캐릭터의 움직이는 방향.

    private float health;                   // 캐릭터의 체력
    private float gold;                     // 캐릭터가 가진 골드
    /*-----------------------------------------------------------------------------------------------------------*/

    CommandKey btnJump, btnRun;

    private static PlayerControlManager instance = null;

    // 인스턴스에 접근할 수 있는 프로퍼티, static으로 선언하여 다른 클래스에서 호출 가능함.
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

        SetCommand();
    }

    void Update()
    {
        if (GameManager.Instance.CheckPlayerState() == "LIVE")
        { 
            //방향키가 아닌 WSAD는 return
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
                return;

            // 현재 캐릭터가 땅에 있는가?
            if (controller.isGrounded)
            {
                PlayerAnimationController.Instance.ChangeAnimationState("JUMP", false);

                Move();
                Jump();
                Run();
            }

            //스태미너를 다 사용했다면
            if (curStamina <= 0)
            {
                //스테미너 올라가기
                if (curStamina <= 100)
                    curStamina += 0.5f;

                speed = moveSpeed;
                PlayerAnimationController.Instance.ChangeAnimationState("NOTRUN", true);
            }

            Rotation();

            // 캐릭터에 중력 적용.
            MoveDir.y -= gravity * Time.deltaTime;

            // 캐릭터 움직임.
            controller.Move(MoveDir * Time.deltaTime);

            //조금이라도 움직임이 있을 경우 애니메이션 재생
            PlayerAnimationController.Instance.ChangeAnimationState("WALK", true);

        }
    }

    public void SetCommand()
    {
        btnJump = new JumpCommand(this);
        btnRun = new RunCommand(this);
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
            btnRun.Execute();          
        }

        else
        {
            speed = moveSpeed;
            PlayerAnimationController.Instance.ChangeAnimationState("NOTRUN", true);

            //스테미너 올라가기
            if (curStamina <= 100)
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
        if (Input.GetKey(KeyCode.Space))
        {
            btnJump.Execute();
        }
    }
}