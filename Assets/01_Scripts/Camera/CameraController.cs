using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private bool isBossStageCamera = false;

    public bool MyCameraBoss
    {
        get
        {
            return isBossStageCamera;
        }

        set
        {
            isBossStageCamera = value;
        }
    }

    // 따라다닐 타겟 오브젝트의 Transform
    public Transform target;

    private float xVelocity = 0.0f;
    private float zVelocity = 0.0f;
    private float yVelocity = 0.0f;

    [Range(0, 3)]
    [SerializeField] private float smoothTime = 0.2f;

    [Range(0, 10)]
    [SerializeField] private float xMove, yMove, zMove;

    [Range(0, 360)]
    [SerializeField] private float xRot, yRot, zRot;


    void LateUpdate()
    {
        //일반
        if(!MyCameraBoss)
        {
            yMove = 3.5f;
            zMove = 5.0f;
            xRot = 25.0f;

            //position 이동
            float TargetWidth = Mathf.SmoothDamp(transform.position.x, target.position.x + xMove, ref xVelocity, smoothTime);
            float TargetHeight = Mathf.SmoothDamp(transform.position.y, target.position.y + yMove, ref yVelocity, smoothTime);
            float TargetDepth = Mathf.SmoothDamp(transform.position.z, target.position.z - zMove, ref zVelocity, smoothTime);

            transform.position = new Vector3(TargetWidth, TargetHeight, TargetDepth);


            //rotation 이동
            float TargetXRot = transform.rotation.x + xRot;
            float TargetYRot = transform.rotation.y + yRot;
            float TargetZRot = transform.rotation.z + zRot;

            transform.rotation = Quaternion.Euler(TargetXRot, TargetYRot, TargetZRot);
        }

        //보스방일때
        else
        {
            yMove = 10.0f;
            zMove = 6.3f;
            xRot = 55.0f;

            //position 이동
            float TargetWidth = Mathf.SmoothDamp(transform.position.x, target.position.x + xMove, ref xVelocity, smoothTime);
            float TargetHeight = Mathf.SmoothDamp(transform.position.y, target.position.y + yMove, ref yVelocity, smoothTime);
            float TargetDepth = Mathf.SmoothDamp(transform.position.z, target.position.z - zMove, ref zVelocity, smoothTime);

            transform.position = new Vector3(TargetWidth, TargetHeight, TargetDepth);


            //rotation 이동
            float TargetXRot = transform.rotation.x + xRot;
            float TargetYRot = transform.rotation.y + yRot;
            float TargetZRot = transform.rotation.z + zRot;

            transform.rotation = Quaternion.Euler(TargetXRot, TargetYRot, TargetZRot);
        }
    }
}
