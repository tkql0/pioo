using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    private CameraController cameraController;

    //public int player;

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
