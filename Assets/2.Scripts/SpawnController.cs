using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController
{
    public void OnEnable()
    {
        GameTree.GAME.gmaeStrat = true;
    }

    public void OnDisable()
    {
        GameTree.GAME.gmaeStrat = false;
    }
}