using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum CharacterType
{
    NULL,
    Player,
    Enemy,
    Fish,
    Weapon,
}

public class ObjectController
{
    //public Dictionary<long, AttackableCharacter> characterList
    //    = new Dictionary<long, AttackableCharacter>();

    public Player player;
    public Dictionary<long, EnemyCharater> enemyDataList = new Dictionary<long, EnemyCharater>();
    public Dictionary<long, FishCharacter> fishDataList = new Dictionary<long, FishCharacter>();
    public Dictionary<long, Weapon> playerWaponDataList = new Dictionary<long, Weapon>();
    public Dictionary<long, Weapon> enemyWaponDataList = new Dictionary<long, Weapon>();
    public Dictionary<int, Map> mapDataList = new Dictionary<int, Map>();
    //public Dictionary<long, Weapon> WaponDataList = new Dictionary<long, Weapon>();

    public void OnEnable()
    {
        //characterList.Add(Type.Player, Player);
        //characterList.Add(Type.Enemy, new EnemyCharater());

        //characterList[Type.Player].OnDamage();
        //characterList[Type.Enemy].OnDamage();

        for (int i = 0; i < mapDataList.Count; i++)
        {
            MapSpawn(i);
        }
    }

    public void OnDisable()
    {

    }

    public void Update()
    {
            
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

    public void SetActiveEnemy(int InIndex, bool InIsActive)
    {
        if (enemyDataList.TryGetValue(InIndex, out var outData) == false)
            return;

        outData.SetActiveObject(InIsActive);
    }

    public void SetActiveFish(int InIndex, bool InIsActive)
    {
        if (fishDataList.TryGetValue(InIndex, out var outData) == false)
            return;

        outData.SetActiveObject(InIsActive);
    }

    public bool GetActive(int InIndex, CharacterType character)
    {
        bool active = false;

        if (character == CharacterType.Enemy)
            active = enemyDataList[InIndex].enemy.activeSelf;
        else if (character == CharacterType.Fish)
            active = fishDataList[InIndex].fish.activeSelf;

        return active;
    }

    public Vector3 SetPosition(int InIndex, CharacterType character)
    {
        Vector3 myPosition = new Vector3(0, 0, 0);

        if (character == CharacterType.Enemy)
            myPosition = enemyDataList[InIndex].transform.position;
        else if (character == CharacterType.Fish)
            myPosition = fishDataList[InIndex].transform.position;
        //enemyDataList[InIndex].transform.position = new Vector3(randomPositionX + spawnObject.x, 0, 0);
        return myPosition;
    }

    public Vector3 SetNewPosition(int InIndex, CharacterType character,Vector3 myPosition, float randomPosition, Vector3 getCenterPosition)
    {
        if (character == CharacterType.Enemy)
            myPosition = enemyDataList[InIndex].transform.position;
        else if (character == CharacterType.Fish)
            myPosition = fishDataList[InIndex].transform.position;

        return myPosition;
    }
}