using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Type
{
    Player,
    Enemy,
    Fish,
    Weapon,
}

public class ObjectController
{
    public Dictionary<Type, AttackableCharacter> characterList = new Dictionary<Type, AttackableCharacter>();

    public Player player;
    public Dictionary<long, EnemyCharater> enemyDataList = new Dictionary<long, EnemyCharater>();
    public Dictionary<long, FishCharacter> fishDataList = new Dictionary<long, FishCharacter>();
    public Dictionary<long, Weapon> playerWaponDataList = new Dictionary<long, Weapon>();
    public Dictionary<long, Weapon> enemyWaponDataList = new Dictionary<long, Weapon>();
    public Dictionary<int, Map> mapDataList = new Dictionary<int, Map>();

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

    public void SetActiveCharacter(int InIndex, bool InIsActive)
    {
        if (enemyDataList.TryGetValue(InIndex, out var outData) == false)
            return;

        outData.SetActiveObject(InIsActive);
    }
}