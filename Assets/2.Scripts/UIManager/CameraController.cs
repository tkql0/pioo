using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController
{
    private Camera maincamera = Camera.main;

    private ObjectController objectController = GameTree.GAME.objectController;

    public void OnEnable()
    {
        int myPlayerNumber = 0;

        maincamera.enabled = false;

        GameObject followObject = new GameObject("Camera");
        followObject.transform.SetParent(objectController.playerList[myPlayerNumber].gameObject.transform);

        followObject.AddComponent<Camera>();
        FollowCamera followCamera = followObject.AddComponent<FollowCamera>();
        followCamera.Init();
    }

    public void OnDisable()
    {


    }
}