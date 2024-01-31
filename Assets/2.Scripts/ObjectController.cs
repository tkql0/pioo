using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectController
{
    public Player player;
    public Dictionary<long, EnemyCharater> enemyDataList = new Dictionary<long, EnemyCharater>();
    public Dictionary<long, FishCharacter> fishDataList = new Dictionary<long, FishCharacter>();
    public Dictionary<long, Wapon> playerWaponDataList = new Dictionary<long, Wapon>();
    public Dictionary<long, Wapon> enemyWaponDataList = new Dictionary<long, Wapon>();
    public Dictionary<int, Map> mapDataList = new Dictionary<int, Map>();

    public void OnEnable()
    {
        for (int i = 0; i < mapDataList.Count; i++)
        {
            MapSpawn(i);
        }
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

        //for (long i = 0; i < enemyDataList.Count; i++)
        //{
        //    EnemyCommand(i);
        //}
    }

    private void MapSpawn(int InCharacterld)
    {
        if (mapDataList.TryGetValue(InCharacterld, out var outCharacter) == false)
            return;

        if (outCharacter.gameObject.activeSelf)
        {
            outCharacter.MapMonsterSpawn(outCharacter.enemyMaxSize, outCharacter.fishMaxSize);
        }
    }

    //private void EnemyCommand(long InCharacterld)
    //{
    //    if (enemyDataList.TryGetValue(InCharacterld, out var outCharacter) == false)
    //        return;


    //}

    //private void FishCommand(long InCharacterld)
    //{
    //    if (fishList.TryGetValue(InCharacterld, out var outCharacter) == false)
    //        return;
    //}
}