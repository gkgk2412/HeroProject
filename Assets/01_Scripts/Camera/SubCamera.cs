using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SubCamera : MonoBehaviour
{
    public CameraController _CameraController;

    public GameObject MainCamera;
    public GameObject boss;

    public Transform target;
    public Transform target2;
    public Transform target3;

    public UnityEvent _curtainOff;
    public UnityEvent _SubCameraOff;

    bool isTriggerBoss01 = false;
    bool isTriggerBoss02 = false;
    bool isTriggerFirstEvent = false;
    bool isOnce = false;

    Camera _camera;
    Animator boss_ani;

    float deltaTime = 0.0f;
    float deltaTime02 = 0.0f;

    private void Start()
    {
        _camera = MainCamera.GetComponent<Camera>();
        boss_ani = boss.GetComponent<Animator>();
    }

    void Update()
    {
        if(!_camera.enabled)
        {
            //보스방 이벤트 1
            if(isTriggerBoss01)
            {
                this.transform.position = target2.transform.position;
                this.transform.rotation = target2.transform.rotation;
            }

            //보스방 이벤트 1
            if (isTriggerBoss02)
            {
                isTriggerBoss01 = false;
                this.transform.position = target3.transform.position;
                this.transform.rotation = target3.transform.rotation;

                //sub Camera가 보스를 쳐다봄
                Invoke("WaitBoss", 2.0f);
            }

            //퀘스트 3개깨면 이벤트
            if(!isTriggerBoss01 && !isTriggerBoss01 && !isTriggerFirstEvent)
            {
                deltaTime02 += Time.deltaTime;

                if(deltaTime02 > 4.0f)
                {
                    isTriggerFirstEvent = true;
                }

                CamShake.Instance.InCameraShake(0.25f, 3.8f);

                this.transform.position = target.transform.position;
                this.transform.rotation = target.transform.rotation;
            }
        }
    }

    public void GetBoss01()
    {
        isTriggerBoss01 = true;
    }
    
    public void GetBoss02()
    {
        isTriggerBoss02 = true;
    }

    public void WaitBoss()
    {
        //보스 등장
        boss.GetComponent<Rigidbody>().isKinematic = false;

        Invoke("WaitCamShake", 1.3f);
        Invoke("WaitRot", 2.0f);
    }

    public void WaitCamShake()
    {
        deltaTime += Time.deltaTime;

        if(deltaTime < 0.5f)        
            CamShake.Instance.InCameraShake(0.05f, 0.5f);
    }

    public void WaitRot()
    {
        //등장 후에 카메라가 보스 비추기
        Vector3 dir = boss.transform.position - target3.transform.position;
        target3.transform.rotation = Quaternion.Lerp(target3.transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 1.0f);

        Invoke("WaitBossAni", 3.0f);
    }

    public void WaitBossAni()
    {
        if(!isOnce)
        {
            boss_ani.SetBool("isStartEvent", true);
            isOnce = true;
        }
        Invoke("EventCurtainOff", 2.5f);
    }

    public void EventCurtainOff()
    {
        _curtainOff.Invoke();
        boss_ani.SetBool("isStartEvent", false);
        isTriggerBoss02 = false;
        Invoke("SubCamOff", 2.0f);
    }

    public void SubCamOff()
    {
        _SubCameraOff.Invoke();

        //움직일 수 있도록...
        GameManager.Instance.PlayerStateChange("LIVE");

        _CameraController.MyCameraBoss = true;
    }
}
