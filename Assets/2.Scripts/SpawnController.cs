using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController
{
    public GameObject spawnGroupObject;
    public void OnEnable()
    {
        GameTree.GAME.gameStrat = true;

        spawnGroupObject = new GameObject("ObjectGroup");
        SpawnObject spawnObject = spawnGroupObject.AddComponent<SpawnObject>();

        spawnObject.Init();
    }

    public void OnDisable()
    {
        GameTree.GAME.gameStrat = false;
    }
}