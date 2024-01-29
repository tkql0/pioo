using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController
{
    public Camera mainCam = Camera.main;

    private ObjectController _objectController = GameTree.GAME.objectController;

    public void OnEnable()
    {
        int myPlayerNumber = 0;

        mainCam.enabled = false;

        GameObject follewObject = new GameObject("Camera");
        follewObject.transform.SetParent(_objectController.playerDataList[myPlayerNumber].player.transform);

        follewObject.AddComponent<Camera>();
        FollowCamera followCamera = follewObject.AddComponent<FollowCamera>();

        followCamera.Init();
    }
}
