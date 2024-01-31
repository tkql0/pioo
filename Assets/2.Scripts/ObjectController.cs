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

        //for (long i = 0; i < enemyDataList.Count; i++)
        //{
        //    EnemyCommand(i);
        //}
    }

    private void EnemyCommand(long InCharacterld)
    {
        if (enemyDataList.TryGetValue(InCharacterld, out var outCharacter) == false)
            return;

        
    }

    //private void FishCommand(long InCharacterld)
    //{
    //    if (fishList.TryGetValue(InCharacterld, out var outCharacter) == false)
    //        return;
    //}
}