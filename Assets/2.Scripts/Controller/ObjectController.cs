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
    public Player player;
    public Dictionary<long, Character> characterDataList = new Dictionary<long, Character>();
    public Dictionary<long, Map> mapDataList = new Dictionary<long, Map>();
    public Dictionary<long, Weapon> weaponDataList = new Dictionary<long, Weapon>();

    private const int Position_X_Min = -20;
    private const int Position_X_Max = 21;

    private const int Position_Y_Min = -3;
    private const int Position_Y_Max = -40;

    public void OnEnable()
    {

    }

    public void OnDisable()
    {

    }

    public void SetActive(long InIndex, ObjectType InObjectType, bool InIsActive)
    {
        switch (InObjectType)
        {
            case ObjectType.Enemy:
                if (characterDataList.TryGetValue(InIndex, out var outEnemyData) == false)
                    return;
                    outEnemyData.SetActiveObject(InIsActive);
                break;
            case ObjectType.Fish:
                if (characterDataList.TryGetValue(InIndex, out var outFishData) == false)
                    return;
                    outFishData.SetActiveObject(InIsActive);
                break;
            case ObjectType.EnemyWeapon:
                if (weaponDataList.TryGetValue(InIndex, out var outEnemyWeaponData) == false)
                    return;
                    outEnemyWeaponData.SetActiveObject(InIsActive);
                break;
            case ObjectType.PlayerWeapon:
                if (weaponDataList.TryGetValue(InIndex, out var outPlayerWeaponData) == false)
                    return;
                    outPlayerWeaponData.SetActiveObject(InIsActive);
                break;
        }
    }

    public bool GetisActive(long InIndex, ObjectType InObjectType)
    {
        bool isActive = true;

        switch (InObjectType)
        {
            case ObjectType.Enemy:
                if (characterDataList.TryGetValue(InIndex, out var outEnemyData) == true)
                    if (outEnemyData.key == InObjectType)
                        isActive = outEnemyData.characterObject.activeSelf;
                break;
            case ObjectType.Fish:
                if (characterDataList.TryGetValue(InIndex, out var outFishData) == true)
                    if (outFishData.key == InObjectType)
                        isActive = outFishData.characterObject.activeSelf;
                break;
            case ObjectType.EnemyWeapon:
                if (weaponDataList.TryGetValue(InIndex, out var outEnemyWeaponData) == true)
                    if (outEnemyWeaponData.key == InObjectType)
                        isActive = outEnemyWeaponData.weapon.activeSelf;
                break;
            case ObjectType.PlayerWeapon:
                if (weaponDataList.TryGetValue(InIndex, out var outPlayerWeaponData) == true)
                    if (outPlayerWeaponData.key == InObjectType)
                        isActive = outPlayerWeaponData.weapon.activeSelf;
                break;
            case ObjectType.Map:
                if (mapDataList.TryGetValue(InIndex, out var outMapData) == true)
                    isActive = outMapData.map.activeSelf;
                break;
        }

        return isActive;
    }

    public void SetSpawnPosition(long InIndex, ObjectType InObjectType, Vector2 InSpawnPosition)
    {
        int randomPositionX = Random.Range(Position_X_Min, Position_X_Max);
        int randomPositionY = Random.Range(Position_Y_Min, Position_Y_Max);

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
                if (weaponDataList.TryGetValue(InIndex, out var outEnemyWeaponData) == false)
                    return;
                    outEnemyWeaponData.transform.position = InSpawnPosition;
                break;
            case ObjectType.PlayerWeapon:
                if (weaponDataList.TryGetValue(InIndex, out var outPlayerWawponData) == false)
                    return;
                    outPlayerWawponData.transform.position = InSpawnPosition;
                break;
        }
    }
}