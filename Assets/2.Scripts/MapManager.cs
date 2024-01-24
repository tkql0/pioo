using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager
{
    public MapController mapController;
    public CameraController cameraController;

    public void Init()
    {
        mapController = new MapController();
        cameraController = new CameraController();

        cameraController.OnEnable();
        mapController.OnEnable();
    }

    public void OnEnable()
    {
        Init();
    }
    public void OnDisable()
    {


    }
}
