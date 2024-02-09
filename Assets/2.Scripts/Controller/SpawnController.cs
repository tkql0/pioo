using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController
{
    private const int Left_MapSpawn = -1;
    private const int Right_MapSpawn = 1;

    public void OnEnable()
    {
        
    }

    public void OnDisable()
    {

    }

    public void GameStartSpawnPosition()
    {
        ObjectController _objectController = GameManager.OBJECT;

        int i = Left_MapSpawn;

        foreach (KeyValuePair<long, Map> mapNumber in _objectController.mapDataList)
        {
            mapNumber.Value.transform.position = new Vector2(_objectController.player.transform.position.x + (i * 40), 0);

            if (i == Right_MapSpawn)
                i = Left_MapSpawn;

            i++;

            mapNumber.Value.MapMonsterSpawn(mapNumber.Value.enemyMaxSize, mapNumber.Value.fishMaxSize);
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
                if (!_objectController.GetisActive(outChatacterData.Key, InObjectType) && outChatacterData.Value.key == InObjectType)
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
                if (!_objectController.GetisActive(outWeaponData.Key, InObjectType) && outWeaponData.Value.key == InObjectType)
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

        float DistanceX = 0;
        float differenceX = 0;

        foreach (KeyValuePair<long, Character> OutChatacterData in _objectController.characterDataList)
        {
            myPosition = OutChatacterData.Value.transform.position;

            DistanceX = InTargetPosition.x - myPosition.x;
            differenceX = Mathf.Abs(DistanceX);

            if (differenceX > InDeSpawnDistance)
            {
                _objectController.SetActive(OutChatacterData.Key, InObjectType, false);
                OutChatacterData.Value.spawnObjectKey = 99;
            }
        }
    }
}