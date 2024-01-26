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

        GameObject FoiiowObject = new GameObject("Camera");
        FoiiowObject.transform.SetParent(objectController.playerList[myPlayerNumber].gameObject.transform);

        FoiiowObject.AddComponent<Camera>();
        FollowCamera followCamera = FoiiowObject.AddComponent<FollowCamera>();
        followCamera.Init();
    }

    public void OnDisable()
    {


    }
}