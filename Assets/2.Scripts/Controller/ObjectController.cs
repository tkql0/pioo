using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectType
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
    public Dictionary<long, EnemyCharacter> enemyDataList = new Dictionary<long, EnemyCharacter>();
    public Dictionary<long, FishCharacter> fishDataList = new Dictionary<long, FishCharacter>();
    public Dictionary<long, Weapon> playerWaponDataList = new Dictionary<long, Weapon>();
    public Dictionary<long, Weapon> enemyWaponDataList = new Dictionary<long, Weapon>();
    public Dictionary<long, Map> mapDataList = new Dictionary<long, Map>();
    //public Dictionary<long, Weapon> WaponDataList = new Dictionary<long, Weapon>();

    private const int Position_X_Min = -20;
    private const int Position_X_Max = 21;

    private const int Position_Y_Min = -3;
    private const int Position_Y_Max = -40;

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

    private void MapSpawn(int InIndex)
    {
        if (mapDataList.TryGetValue(InIndex, out var outCharacter) == false)
            return;

        if (outCharacter.gameObject.activeSelf)
        {
            outCharacter.MapMonsterSpawn(outCharacter.enemyMaxSize, outCharacter.fishMaxSize);
        }
    }

    public void SetActive(long InIndex, ObjectType character, bool InIsActive)
    {
        switch (character)
        {
            case ObjectType.Enemy:
                if (enemyDataList.TryGetValue(InIndex, out var outEnemyData) == false)
                    return;
                outEnemyData.SetActiveObject(InIsActive);
                break;
            case ObjectType.Fish:
                if (fishDataList.TryGetValue(InIndex, out var outFishData) == false)
                    return;
                outFishData.SetActiveObject(InIsActive);
                break;
        }
    }

    public bool GetisActive(long InIndex, ObjectType character)
    {
        bool isActive = false;

        if (character == ObjectType.Enemy)
            isActive = enemyDataList[InIndex].enemy.activeSelf;
        else if (character == ObjectType.Fish)
            isActive = fishDataList[InIndex].fish.activeSelf;

        return isActive;
    }

    public void SetSpawnPosition(long InIndex, ObjectType character, Vector2 centerPosition)
    {
        int randomPositionX = Random.Range(Position_X_Min, Position_X_Max);
        int randomPositionY = Random.Range(Position_Y_Min, Position_Y_Max);

        switch (character)
        {
            case ObjectType.Enemy:
                if (enemyDataList.TryGetValue(InIndex, out var outEnemyData) == false)
                    return;
                outEnemyData.transform.position =
                    new Vector2(randomPositionX + centerPosition.x, 0);
                break;
            case ObjectType.Fish:
                if (fishDataList.TryGetValue(InIndex, out var outFishData) == false)
                    return;
                outFishData.transform.position =
                    new Vector2(randomPositionX + centerPosition.x, randomPositionY);
                break;
        }
    }
}