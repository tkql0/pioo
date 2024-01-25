using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    public ObjectController objectController;
    public SpawnController spawnController;
    public MapController mapController;

    public void Init()
    {
        objectController = new ObjectController();
        spawnController = new SpawnController();
        mapController = new MapController();

        spawnController.OnEnable();
        objectController.OnEnable();
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