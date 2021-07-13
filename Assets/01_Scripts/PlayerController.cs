using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 현재 캐릭터가 가지고있는 캐릭터 컨트롤러 콜라이더.
    private CharacterController controller;
    private Animator _charAnim;

    private float horizontalMove;
    private float verticalMove;

    /*-----------------캐릭터 상태값--------------------*/
    public float speed;                     // 캐릭터 움직임 스피드.
    public float jumpSpeed;                 // 캐릭터 점프 힘.
    public float gravity;                   // 캐릭터에게 작용하는 중력.
    public float rotTime;                   // 회전시간
    private Vector3 MoveDir;                // 캐릭터의 움직이는 방향.

    void Start()
    {
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
            _charAnim.SetBool("isJump", false);

            Rotation();
            Move();
            Jump();
        }

        // 캐릭터에 중력 적용.
        MoveDir.y -= gravity * Time.deltaTime;

        // 캐릭터 움직임.
        controller.Move(MoveDir * Time.deltaTime);

        //조금이라도 움직임이 있을 경우 애니메이션 재생
        _charAnim.SetFloat("Speed", MoveDir.magnitude);
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
            _charAnim.SetBool("isJump", true);
        }
    }
}