using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController
{
    public void OnEnable()
    {
        GameManager.SPAWN._objectPool.SpawnMapPool();
    }

    public void OnDisable()
    {

    }
}
