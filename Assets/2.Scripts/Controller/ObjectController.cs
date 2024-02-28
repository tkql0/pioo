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

public enum ItemType
{
    NULL,
    Item_Fish,
}

public class ObjectController
{
    public Player player;
    public Dictionary<long, Character> characterDataList = new Dictionary<long, Character>();
    public Dictionary<long, Map> mapDataList = new Dictionary<long, Map>();
    public Dictionary<long, SpawnMap> spawnMapDataList = new Dictionary<long, SpawnMap>();
    // 위에 있는 mapDataList와는 다른 테스트용 Dictionary
    public Dictionary<long, Weapon> weaponDataList = new Dictionary<long, Weapon>();

    public Dictionary<long, Item> itemDataList = new Dictionary<long, Item>();

    public void OnEnable()
    {

    }

    public void SetActive(long InIndex, ObjectType InObjectType, bool InIsActive)
    {
        switch (InObjectType)
        {
            case ObjectType.Enemy:
            case ObjectType.Fish:
                if (characterDataList.TryGetValue(InIndex, out var outCharacterData) == false)
                    return;

                outCharacterData.SetActiveObject(InIsActive);
                break;
            case ObjectType.EnemyWeapon:
            case ObjectType.PlayerWeapon:
                if (weaponDataList.TryGetValue(InIndex, out var outWeaponData) == false)
                    return;

                outWeaponData.SetActiveObject(InIsActive);
                break;
        }
    }

    public bool GetisActive(long InIndex, ObjectType InObjectType)
    {
        switch (InObjectType)
        {
            case ObjectType.Enemy:
            case ObjectType.Fish:
                {
                    if (characterDataList.TryGetValue(InIndex, out var outCharacterData) == false)
                        return true;

                    if (outCharacterData.key == InObjectType)
                        return outCharacterData.isActive;
                }
                break;
            case ObjectType.EnemyWeapon:
            case ObjectType.PlayerWeapon:
                {
                    if (weaponDataList.TryGetValue(InIndex, out var outWeaponData) == false)
                        return true;

                    if (outWeaponData.key == InObjectType)
                        return outWeaponData.isActive;
                }
                break;
            case ObjectType.Map:
                {
                    if (mapDataList.TryGetValue(InIndex, out var outMapData) == false)
                        return outMapData.isActive;

                    //if (spawnMapDataList.TryGetValue(InIndex, out var outMapData) == false)
                    //    return outMapData.isActive;
                }
                break;
        }
        return true;
    }

    public void SetSpawnPosition(long InIndex, ObjectType InObjectType, Vector2 InSpawnPosition)
    {
        int randomPositionX = Random.Range(Spawn_Position_X_Min, Spawn_Position_X_Max);
        int randomPositionY = Random.Range(Spawn_Position_Y_Min, Spawn_Position_Y_Max);

        switch (InObjectType)
        {
            case ObjectType.Enemy:
                if (characterDataList.TryGetValue(InIndex, out var outEnemyData) == false)
                    return;
                    outEnemyData.transform.position = new Vector2(randomPositionX + InSpawnPosition.x, 0);
                break;
            case ObjectType.Fish:
                if (characterDataList.TryGetValue(InIndex, out var outFishData) == false)
                    return;
                    outFishData.transform.position = new Vector2(randomPositionX + InSpawnPosition.x, randomPositionY);
                break;
            case ObjectType.EnemyWeapon:
            case ObjectType.PlayerWeapon:
                if (weaponDataList.TryGetValue(InIndex, out var outWawponData) == false)
                    return;
                outWawponData.transform.position = InSpawnPosition;
                break;
        }
    }

    public bool SetCharacterInfo(Character InCharacter, ObjectType Inkey, long InSpawnNumber)
    {
        if (InCharacter == null)
            return false;

        characterDataList.Add(InSpawnNumber, InCharacter);
        InCharacter.SetKey(Inkey);
        InCharacter.SetSpawnNumber(InSpawnNumber);

        return true;
    }

    public bool SetWeaponInfo(Weapon InWeapon, ObjectType Inkey, long InSpawnNumber)
    {
        if (InWeapon == null)
            return false;

        weaponDataList.Add(InSpawnNumber, InWeapon);
        InWeapon.SetKey(Inkey);
        InWeapon.SetSpawnNumber(InSpawnNumber);

        return true;
    }

    private const int Spawn_Position_X_Min = -20;
    private const int Spawn_Position_X_Max = 21;

    private const int Spawn_Position_Y_Min = -3;
    private const int Spawn_Position_Y_Max = -40;
}