using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectController
{
    public Dictionary<int, MyCharater> playerList = new Dictionary<int, MyCharater>();
    public Dictionary<int, Map> mapList = new Dictionary<int, Map>();
    public Dictionary<long, EnemyCharater> enemyList = new Dictionary<long, EnemyCharater>();
    public Dictionary<long, FishCharacter> fishList = new Dictionary<long, FishCharacter>();

    public void OnEnable()
    {
        Init();


    }

    public void OnDisable()
    {


    }

    //public Action<long> OnDead_Event = null;
    public Action<long> OnMove_Event = null;

    public void Init()
    {
        for (long i = 0; i < fishList.Count; i++)
        {
            FishMoveCommand(i);
        }

        for (long i = 0; i < enemyList.Count; i++)
        {
            EnemyMoveCommand(i);
        }
    }

    public void EnemyMoveCommand(long InCharacterld)
    {
        if (enemyList.TryGetValue(InCharacterld, out var outCharacter) == false)
            return;

        outCharacter.Move();

        OnMove_Event?.Invoke(InCharacterld);
    }

    public void FishMoveCommand(long InCharacterld)
    {
        if (fishList.TryGetValue(InCharacterld, out var outCharacter) == false)
            return;

        outCharacter.Move();

        OnMove_Event?.Invoke(InCharacterld);
    }

    //void DistanceFromPlayer()
    //{
    //    Vector3 playerPos = myCharacter.transform.position;
    //    Vector3 myPos = transform.position;

    //    float DirX = playerPos.x - myPos.x;
    //    float diffX = Mathf.Abs(DirX);

    //    if (diffX > 70.0f)
    //        gameObject.SetActive(false);
    //}
}