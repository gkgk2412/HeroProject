using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyWalkMove : MonoBehaviour
{
    private GameObject contactPlatform;
    public GameObject Player;

    private Vector3 platformPosition;
    private Vector3 distance;

    private bool isSkybox = false;
    private bool isBoxIn = false;
    private bool isSkyboxEnd = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "skybox")
        {
            //접촉한 순간의 오브젝트 위치를 저장
            contactPlatform = other.gameObject;
            isSkyboxEnd = false;
            isBoxIn = true;
            WaitTrue();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "skybox")
        {
            Player.GetComponent<CharacterController>().enabled = true;
            isSkybox = false;
            isBoxIn = false;
            isSkyboxEnd = true;
        }
    }

    private void Update()
    {
        if(isSkybox && !isSkyboxEnd)
        {
            Player.GetComponent<CharacterController>().enabled = false;
            PlayerAnimationController.Instance.ChangeAnimationState("IDLE", false);
            PlayerAnimationController.Instance.ChangeAnimationState("NOTRUN", true);

            //캐릭터의 위치는 밟고 있는 플랫폼과 distance 만큼 떨어진 위치
            Player.transform.position = contactPlatform.transform.position - distance;
        }

        if(isBoxIn)
        {
            //움직이면 
            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
            {
                Waitfalse();
            }

            if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
            {
                WaitTrue();
            }
        }
    }

    private void WaitTrue()
    {
        isSkybox = true;

        if (isSkybox)
        {
            platformPosition = contactPlatform.transform.position;

            //접촉한 순간의 오브젝트 위치와 캐릭터 위치의 차이를 distance에 저장
            distance = platformPosition - Player.transform.position;
        }
    }

    private void Waitfalse()
    {
        Player.GetComponent<CharacterController>().enabled = true;
        isSkybox = false;
    }
}
