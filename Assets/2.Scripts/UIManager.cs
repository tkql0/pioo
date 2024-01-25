using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    public CameraController cameraController;

    public void Init()
    {
        cameraController = new CameraController();

        cameraController.OnEnable();
    }

    public void OnEnable()
    {
        Init();
    }
    public void OnDisable()
    {


    }
}
