using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectController
{
    public Dictionary<int, MyCharater> playerList = new Dictionary<int, MyCharater>();
    public Dictionary<int, Map> mapList = new Dictionary<int, Map>();
    // public Dictionary<long, EnemyCharater> enemyList = new Dictionary<long, EnemyCharater>();
    public Dictionary<long, GameObject> enemyList = new Dictionary<long, GameObject>();
    //public Dictionary<long, FishCharacter> fishList = new Dictionary<long, FishCharacter>();
    public Dictionary<long, GameObject> fishList = new Dictionary<long, GameObject>();

    public void OnEnable()
    {
        //playerList = new Dictionary<int, MyCharater>();
        //enemyList = new Dictionary<long, EnemyCharater>();
        //fishList = new Dictionary<long, FishCharacter>();

        Init();
    }

    public void OnDisable()
    {


    }

    public void Init()
    {
        //playerList = new Dictionary<int, MyCharater>();
        //enemyList = new Dictionary<long, EnemyCharater>();
        //fishList = new Dictionary<long, FishCharacter>();

        //for (long i = 0; i < fishList.Count; i++)
        //{
        //    FishCommand(i);
        //}

        //for (long i = 0; i < enemyList.Count; i++)
        //{
        //    EnemyCommand(i);
        //}
    }

    //public void EnemyCommand(long InCharacterld)
    //{
    //    if (enemyList.TryGetValue(InCharacterld, out var outCharacter) == false)
    //        return;

    //    outCharacter.Move();
    //}

    //public void FishCommand(long InCharacterld)
    //{
    //    if (fishList.TryGetValue(InCharacterld, out var outCharacter) == false)
    //        return;

    //    outCharacter.Move();
    //}

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