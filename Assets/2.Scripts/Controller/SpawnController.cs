using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController
{
    private const int Left_MapSpawn = -1;
    private const int Right_MapSpawn = 1;

    private const int Map_Distance = 40;

    public void OnEnable()
    {
        
    }

    public void OnDisable()
    {

    }

    public void GameStartSpawnPosition()
    {
        ObjectController _objectController = GameManager.OBJECT;

        int mapSpawnCount = Left_MapSpawn;

        foreach (KeyValuePair<long, Map> outMapData in _objectController.mapDataList)
        {
            outMapData.Value.transform.position = new Vector2(_objectController.player.transform.position.x + (mapSpawnCount * Map_Distance), 0);

            if (mapSpawnCount == Right_MapSpawn)
                mapSpawnCount = Left_MapSpawn;

            mapSpawnCount++;

            outMapData.Value.MapMonsterSpawn(outMapData.Value.enemyMaxSize, outMapData.Value.fishMaxSize);
        }
    }

    public void Spawn(Vector2 InSpawnPosition, long InEnemyCount, long InFishCount, long InKey)
    {
        for (long i = 0; i < InEnemyCount; i++)
        {
            ObjectSpawn(InSpawnPosition, InKey, ObjectType.Enemy);
        }

        for (long i = 0; i < InFishCount; i++)
        {
            ObjectSpawn(InSpawnPosition, InKey, ObjectType.Fish);
        }
    }

    public void DeSpawn(Vector2 InTargetPosition, float InDeSpawnDistance)
    {
        ObjectDeSpawn(InTargetPosition, ObjectType.Enemy, InDeSpawnDistance);
        ObjectDeSpawn(InTargetPosition, ObjectType.Fish, InDeSpawnDistance);
    }


    public GameObject ObjectSpawn(Vector2 InSpawnPosition, long InKey, ObjectType InObjectType)
    {
        ObjectController _objectController = GameManager.OBJECT;

        bool _character =  InKey >= 0 ? true : false;

        if (_character)
        {
            foreach (KeyValuePair<long, Character> outChatacterData in _objectController.characterDataList)
            {
                if (!_objectController.GetisActive(outChatacterData.Key, InObjectType))
                {
                    _objectController.SetActive(outChatacterData.Key, InObjectType, true);
                    _objectController.SetSpawnPosition(outChatacterData.Key, InObjectType, InSpawnPosition);

                    outChatacterData.Value.spawnObjectKey = InKey;

                    return outChatacterData.Value.characterObject;
                }
            }
        }
        else
        {
            foreach (KeyValuePair<long, Weapon> outWeaponData in _objectController.weaponDataList)
            {
                if (!_objectController.GetisActive(outWeaponData.Key, InObjectType))
                {
                    _objectController.SetActive(outWeaponData.Key, InObjectType, true);
                    _objectController.SetSpawnPosition(outWeaponData.Key, InObjectType, InSpawnPosition);

                    return outWeaponData.Value.weapon;
                }
            }
        }
        return null;
    }

    public void ObjectDeSpawn(Vector2 InTargetPosition, ObjectType InObjectType, float InDeSpawnDistance)
    {
        ObjectController _objectController = GameManager.OBJECT;

        Vector2 myPosition;

        float distanceX = 0;
        float differenceX = 0;

        foreach (KeyValuePair<long, Character> outChatacterData in _objectController.characterDataList)
        {
            myPosition = outChatacterData.Value.transform.position;

            distanceX = InTargetPosition.x - myPosition.x;
            differenceX = Mathf.Abs(distanceX);

            if (differenceX > InDeSpawnDistance)
            {
                _objectController.SetActive(outChatacterData.Key, InObjectType, false);
                outChatacterData.Value.spawnObjectKey = 99;
            }
        }
    }
}