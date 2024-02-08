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

        foreach (KeyValuePair<long, Character> chatacterNumber in _objectController.characterDataList)
        {
            if (!_objectController.GetisActive(chatacterNumber.Key, InObjectType) && chatacterNumber.Value.key == InObjectType)
            {
                _objectController.SetActive(chatacterNumber.Key, InObjectType, true);
                _objectController.SetSpawnPosition(chatacterNumber.Key, InObjectType, InSpawnPosition);

                chatacterNumber.Value.spawnObjectKey = InKey;

                return chatacterNumber.Value.characterObject;
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

        foreach (KeyValuePair<long, Character> chatacterNumber in _objectController.characterDataList)
        {
            myPosition = chatacterNumber.Value.transform.position;

            DistanceX = InTargetPosition.x - myPosition.x;
            differenceX = Mathf.Abs(DistanceX);

            if (differenceX > InDeSpawnDistance)
            {
                _objectController.SetActive(chatacterNumber.Key, InObjectType, false);
                chatacterNumber.Value.spawnObjectKey = 99;
            }
        }
    }

    public GameObject SpawnWeapon(Vector2 inSpawnPosition, ObjectType objectType)
    {
        ObjectController _objectController = GameManager.OBJECT;

        foreach (KeyValuePair<long, Weapon> weaponNumber in _objectController.weaponDataList)
        {
            switch (objectType)
            {
                case ObjectType.PlayerWeapon:
                    if (!_objectController.GetisActive(weaponNumber.Key, objectType) && weaponNumber.Value.key == objectType)
                    {
                        weaponNumber.Value.weapon.SetActive(true);

                        weaponNumber.Value.transform.position
                            = new Vector2(inSpawnPosition.x, inSpawnPosition.y);

                        return weaponNumber.Value.weapon;
                    }
                    break;
                case ObjectType.EnemyWeapon:
                    if (!_objectController.GetisActive(weaponNumber.Key, objectType) && weaponNumber.Value.key == objectType)
                    {
                        weaponNumber.Value.weapon.SetActive(true);

                        weaponNumber.Value.transform.position
                            = new Vector2(inSpawnPosition.x, inSpawnPosition.y);

                        return weaponNumber.Value.weapon;
                    }
                    break;
            }
        }
        return null;
    }
}