using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController
{
    public SpawnObject spawnObject = null;

    public void OnEnable()
    {
        var load = Resources.Load<SpawnObject>(_objectPoolPrefab);

        if (load == null)
            return;

        spawnObject = GameObject.Instantiate(load);

        spawnObject.ObjectSpawnPool();
    }

    public void GameStartSpawnPosition()
    {
        int mapSpawnCount = Left_MapSpawn;

        foreach (KeyValuePair<long, Map> outMapData in GameManager.OBJECT.mapDataList)
        {
            outMapData.Value.transform.position =
                new Vector2(GameManager.OBJECT.player.characterPosition.x + (mapSpawnCount * Map_Distance), 0);

            if (mapSpawnCount == Right_MapSpawn)
                mapSpawnCount = Left_MapSpawn;

            mapSpawnCount++;

            outMapData.Value.MapMonsterSpawn(outMapData.Value.enemyMaxSize, outMapData.Value.fishMaxSize);
        }
    }

    public void TestGameStartSpawnPosition()
    {
        int mapSpawnCount = Left_MapSpawn;

        foreach (KeyValuePair<long, SpawnPositionObject> outMapData in GameManager.OBJECT.testSpawmPositionDataList)
        {
            outMapData.Value.transform.position =
                new Vector2(GameManager.OBJECT.player.characterPosition.x + (mapSpawnCount * Map_Distance), 0);

            if (mapSpawnCount == Right_MapSpawn)
                mapSpawnCount = Left_MapSpawn;

            mapSpawnCount++;
        }
    }

    public void Spawn(Vector2 InSpawnPosition, long InEnemyCount, long InFishCount, long InKey)
    {
        for (long i = 0; i < InEnemyCount; i++)
        {
            GetObjectSpawn(InSpawnPosition, InKey, ObjectType.Enemy);
        }

        for (long i = 0; i < InFishCount; i++)
        {
            GetObjectSpawn(InSpawnPosition, InKey, ObjectType.Fish);
        }
    }

    public void DeSpawn(Vector2 InTargetPosition, float InDeSpawnDistance)
    {
        SetObjectDeSpawn(InTargetPosition, ObjectType.Enemy, InDeSpawnDistance);
        SetObjectDeSpawn(InTargetPosition, ObjectType.Fish, InDeSpawnDistance);
    }


    public GameObject GetObjectSpawn(Vector2 InSpawnPosition, long InKey, ObjectType InObjectType)
    {
        bool _character =  InKey >= 0 ? true : false;

        if (_character)
        {
            foreach (KeyValuePair<long, Character> outChatacterData in GameManager.OBJECT.characterDataList)
            {
                if (!GameManager.OBJECT.GetisActive(outChatacterData.Key, InObjectType))
                {
                    GameManager.OBJECT.SetActive(outChatacterData.Key, InObjectType, true);
                    GameManager.OBJECT.SetSpawnPosition(outChatacterData.Key, InObjectType, InSpawnPosition);

                    outChatacterData.Value.targetSpawnNumber = InKey;

                    return outChatacterData.Value.characterObject;
                }
            }
        }
        else
        {
            foreach (KeyValuePair<long, Weapon> outWeaponData in GameManager.OBJECT.weaponDataList)
            {
                if (!GameManager.OBJECT.GetisActive(outWeaponData.Key, InObjectType))
                {
                    GameManager.OBJECT.SetActive(outWeaponData.Key, InObjectType, true);
                    GameManager.OBJECT.SetSpawnPosition(outWeaponData.Key, InObjectType, InSpawnPosition);

                    return outWeaponData.Value.weaponObject;
                }
            }
        }
        return null;
    }

    public void SetObjectDeSpawn(Vector2 InTargetPosition, ObjectType InObjectType, float InDeSpawnDistance)
    {
        Vector2 myPosition;

        float distanceX = 0;
        float differenceX = 0;

        foreach (KeyValuePair<long, Character> outChatacterData in GameManager.OBJECT.characterDataList)
        {
            myPosition = outChatacterData.Value.characterPosition;

            distanceX = InTargetPosition.x - myPosition.x;
            differenceX = Mathf.Abs(distanceX);

            if (differenceX > InDeSpawnDistance)
            {
                GameManager.OBJECT.SetActive(outChatacterData.Key, InObjectType, false);
                outChatacterData.Value.targetSpawnNumber = 99;
            }
        }
    }

    private const int Left_MapSpawn = -1;
    private const int Right_MapSpawn = 1;

    private const int Map_Distance = 40;

    private const string _objectPoolPrefab = "Prefabs/ObjectPool";
}