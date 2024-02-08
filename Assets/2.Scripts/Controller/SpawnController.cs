using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController
{
    public SpawnObject spawnObject;

    private const int Left_MapSpawn = -1;
    private const int Right_MapSpawn = 1;

    public void OnEnable()
    {
        spawnObject = GameObject.FindObjectOfType<SpawnObject>();
        spawnObject.Init();
        spawnObject.ObjectSpawnPool();

        GameStartSpawnPosition();
    }

    public void OnDisable()
    {

    }

    private void GameStartSpawnPosition()
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

    public void Spawn(Vector2 inSpawnPosition, long enemyCount, long fishCount, long key)
    {
        for (long i = 0; i < enemyCount; i++)
        {
            ObjectSpawn(inSpawnPosition, key, ObjectType.Enemy);
        }

        for (long i = 0; i < fishCount; i++)
        {
            ObjectSpawn(inSpawnPosition, key, ObjectType.Fish);
        }
    }

    public void DeSpawn(Vector2 TargetPosition, float InDeSpawnDistance)
    {
        DistanceObjectDeSpawn(TargetPosition, ObjectType.Enemy, InDeSpawnDistance);
        DistanceObjectDeSpawn(TargetPosition, ObjectType.Fish, InDeSpawnDistance);
    }


    public GameObject ObjectSpawn(Vector2 inSpawnPosition, long key, ObjectType objectType)
    {
        ObjectController _objectController = GameManager.OBJECT;

        foreach (KeyValuePair<long, Character> chatacterNumber in _objectController.characterList)
        {
            switch (objectType)
            {
                case ObjectType.Enemy:
                    if (!_objectController.GetisActive(chatacterNumber.Key, objectType) && chatacterNumber.Value.key == objectType)
                    {
                        _objectController.SetActive(chatacterNumber.Key, objectType, true);
                        _objectController.SetSpawnPosition(chatacterNumber.Key, objectType, inSpawnPosition);

                        chatacterNumber.Value.spawnObjectKey = key;

                        return chatacterNumber.Value.characterObject;
                    }
                    break;
                case ObjectType.Fish:
                    if (!_objectController.GetisActive(chatacterNumber.Key, objectType) && chatacterNumber.Value.key == objectType)
                    {
                        _objectController.SetActive(chatacterNumber.Key, objectType, true);
                        _objectController.SetSpawnPosition(chatacterNumber.Key, objectType, inSpawnPosition);

                        chatacterNumber.Value.spawnObjectKey = key;

                        return chatacterNumber.Value.characterObject;
                    }
                    break;
            }
        }
        return null;
    }

    public void DistanceObjectDeSpawn(Vector2 target, ObjectType objectType, float InDeSpawnDistance)
    {
        ObjectController _objectController = GameManager.OBJECT;

        Vector2 myPosition;

        float DistanceX = 0;
        float differenceX = 0;

        foreach (KeyValuePair<long, Character> chatacterNumber in _objectController.characterList)
        {
            myPosition = chatacterNumber.Value.transform.position;

            DistanceX = target.x - myPosition.x;
            differenceX = Mathf.Abs(DistanceX);

            if (differenceX > InDeSpawnDistance)
            {
                _objectController.SetActive(chatacterNumber.Key, objectType, false);
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