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
                if(outEnemyData.key == ObjectType.Enemy)
                    outEnemyData.SetActiveObject(InIsActive);
                break;
            case ObjectType.Fish:
                if (characterDataList.TryGetValue(InIndex, out var outFishData) == false)
                    return;
                if (outFishData.key == ObjectType.Fish)
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
                if (characterDataList.TryGetValue(InIndex, out var outEnemyData) == false)
                    return isActive;
                if (outEnemyData.key == ObjectType.Enemy)
                    isActive = outEnemyData.characterObject.activeSelf;
                break;
            case ObjectType.Fish:
                if (characterDataList.TryGetValue(InIndex, out var outFishData) == false)
                    return isActive;
                if (outFishData.key == ObjectType.Fish)
                    isActive = outFishData.characterObject.activeSelf;
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

    public void SetSpawnPosition(long InIndex, ObjectType InObjectType, Vector2 InSpawnPosition)
    {
        int randomPositionX = Random.Range(Position_X_Min, Position_X_Max);
        int randomPositionY = Random.Range(Position_Y_Min, Position_Y_Max);

        switch (InObjectType)
        {
            case ObjectType.Enemy:
                if (characterDataList.TryGetValue(InIndex, out var outEnemyData) == false)
                    return;
                if (outEnemyData.key == ObjectType.Enemy)
                    outEnemyData.transform.position = new Vector2(randomPositionX + InSpawnPosition.x, 0);
                break;
            case ObjectType.Fish:
                if (characterDataList.TryGetValue(InIndex, out var outFishData) == false)
                    return;
                if (outFishData.key == ObjectType.Fish)
                    outFishData.transform.position = new Vector2(randomPositionX + InSpawnPosition.x, randomPositionY);
                break;
        }
    }
}