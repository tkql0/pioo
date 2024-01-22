using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    public ObjectController objectController;
    public SpawnController spawnController;

    public bool gmaeStrat = false;

    public int players = 0;

    public void Init()
    {
        objectController = new ObjectController();
        spawnController = new SpawnController();

        spawnController.OnEnable();
    }

    public void OnEnable()
    {
        Init();
    }

    public void OnDisable()
    {


    }
}