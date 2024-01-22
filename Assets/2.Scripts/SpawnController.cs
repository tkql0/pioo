using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController
{
    public void OnEnable()
    {
        GameTree.GAME.gameStrat = true;

        GameObject testObject = new GameObject("ObjectGroup");
        SpawnObject spawnObject = testObject.AddComponent<SpawnObject>();

        spawnObject.Init();
    }

    public void OnDisable()
    {
        GameTree.GAME.gameStrat = false;
    }
}