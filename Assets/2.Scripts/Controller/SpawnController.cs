using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController
{
    public SpawnObject spawnObject;

    public void OnEnable()
    {
        spawnObject = GameObject.FindObjectOfType<SpawnObject>();
        spawnObject.Init();
        spawnObject.StartSpawn();

    }

    public void OnDisable()
    {

    }

    public void Spawn(Vector2 inSpawnPosition, long enemyCount, long fishCount, long key)
    {
        for (long i = 0; i < enemyCount; i++)
        {
            spawnObject.Spawn(inSpawnPosition, key, ObjectType.Enemy);
        }

        for (long i = 0; i < fishCount; i++)
        {
            spawnObject.Spawn(inSpawnPosition, key, ObjectType.Fish);
        }
    }

    public void DeSpawn(Vector2 TargetPosition)
    {
        spawnObject.DistanceObjectDeSpawn(TargetPosition, ObjectType.Enemy);
        spawnObject.DistanceObjectDeSpawn(TargetPosition, ObjectType.Fish);
    }
}

// 어떻게 고쳐야 할지 이해 완료
// 스폰오브젝트 class는 스폰컨트롤러 class를 참조해야 하는데
// 오히려 참조는 spawnObject 변수로 반대로 하고있네