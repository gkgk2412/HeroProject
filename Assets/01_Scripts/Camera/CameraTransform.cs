using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransform : MonoBehaviour
{
    public Camera MainC;
    public Camera SubC;

    // Start is called before the first frame update
    void Start()
    {
        MainCameraOn();
    }

    public void MainCameraOn()
    {
        MainC.enabled = true;
        SubC.enabled = false;
    }

    public void SubCameraOn()
    {
        MainC.enabled = false;
        SubC.enabled = true;
    }
}
