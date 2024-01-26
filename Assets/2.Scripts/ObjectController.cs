using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectController
{
    public Dictionary<int, GameObject> playerList = new Dictionary<int, GameObject>();
    public Dictionary<long, GameObject> enemyList = new Dictionary<long, GameObject>();
    public Dictionary<long, GameObject> fishList = new Dictionary<long, GameObject>();

    public Dictionary<int, MyCharater> playerDataList = new Dictionary<int, MyCharater>();
    public Dictionary<long, EnemyCharater> enemyDataList = new Dictionary<long, EnemyCharater>();
    public Dictionary<long, FishCharacter> fishDataList = new Dictionary<long, FishCharacter>();

    public void OnEnable()
    {
        //Init();
    }

    public void OnDisable()
    {


    }

    public void Init()
    {
        //for (long i = 0; i < fishList.Count; i++)
        //{
        //    FishCommand(i);
        //}

        //for (long i = 0; i < enemyList.Count; i++)
        //{
        //    EnemyCommand(i);
        //}
    }

    public void EnemyCommand(long InCharacterld)
    {
        if (enemyDataList.TryGetValue(InCharacterld, out var outCharacter) == false)
            return;
    }

    public void FishCommand(long InCharacterld)
    {
        if (fishList.TryGetValue(InCharacterld, out var outCharacter) == false)
            return;

        
    }
}