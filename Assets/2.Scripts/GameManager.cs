using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    public ObjectController objectController;
    public SpawnController spawnController;

    public void Init()
    {
        
    }

    public void OnEnable()
    {
        objectController = new ObjectController();
        spawnController = new SpawnController();

    }

    public void OnDisable()
    {


    }
}