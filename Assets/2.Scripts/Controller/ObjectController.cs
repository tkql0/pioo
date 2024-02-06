using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectType
{
    NULL,
    Player,
    Enemy,
    Fish,
    EnemyWeapon,
    PlayerWeapon,
    Map,
}

public class ObjectController
{
    //public Dictionary<long, AttackableCharacter> characterList
    //    = new Dictionary<long, AttackableCharacter>();

    public Player player;
    public Dictionary<long, EnemyCharacter> enemyDataList = new Dictionary<long, EnemyCharacter>();
    public Dictionary<long, FishCharacter> fishDataList = new Dictionary<long, FishCharacter>();
    public Dictionary<long, Map> mapDataList = new Dictionary<long, Map>();
    public Dictionary<long, Weapon> weaponDataList = new Dictionary<long, Weapon>();

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

    private void MapSpawn(long InIndex)
    {
        if (mapDataList.TryGetValue(InIndex, out var outCharacter) == false)
            return;

        if (outCharacter.gameObject.activeSelf)
            outCharacter.MapMonsterSpawn(outCharacter.enemyMaxSize, outCharacter.fishMaxSize);
    }

    public void SetActive(long InIndex, ObjectType InObjectType, bool InIsActive)
    {
        switch (InObjectType)
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

    public bool GetisActive(long InIndex, ObjectType InObjectType)
    {
        bool isActive = false;

        switch (InObjectType)
        {
            case ObjectType.Enemy:
                if (enemyDataList.TryGetValue(InIndex, out var outEnemyData) == false)
                    return isActive;
                isActive = outEnemyData.enemy.activeSelf;
                break;
            case ObjectType.Fish:
                if (fishDataList.TryGetValue(InIndex, out var outFishData) == false)
                    return isActive;
                isActive = outFishData.fish.activeSelf;
                break;
            case ObjectType.Map:
                if (mapDataList.TryGetValue(InIndex, out var outMapData) == false)
                    return isActive;
                isActive = outMapData.map.activeSelf;
                break;
            case ObjectType.EnemyWeapon:
                isActive = weaponDataList[InIndex].weapon.activeSelf;
                break;
            case ObjectType.PlayerWeapon:
                isActive = weaponDataList[InIndex].weapon.activeSelf;
                break;
        }

        return isActive;
    }
    // activeSelf를 변수로 만들어서 OnEnable()랑 OnDisable()로
    // 값을.. 그게 그건가 생각 보류

    public void SetSpawnPosition(long InIndex, ObjectType InObjectType, Vector2 InSpawnPosition)
    {
        int randomPositionX = Random.Range(Position_X_Min, Position_X_Max);
        int randomPositionY = Random.Range(Position_Y_Min, Position_Y_Max);

        switch (InObjectType)
        {
            case ObjectType.Enemy:
                if (enemyDataList.TryGetValue(InIndex, out var outEnemyData) == false)
                    return;
                outEnemyData.transform.position =
                    new Vector2(randomPositionX + InSpawnPosition.x, 0);
                break;
            case ObjectType.Fish:
                if (fishDataList.TryGetValue(InIndex, out var outFishData) == false)
                    return;
                outFishData.transform.position =
                    new Vector2(randomPositionX + InSpawnPosition.x, randomPositionY);
                break;
        }
    }
}