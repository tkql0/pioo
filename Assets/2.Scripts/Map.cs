using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public GameObject mapObject;

    public ObjectType key;

    public long mySpawnNumber;

    public long enemyMaxSize;
    public long fishMaxSize;

    private const float ReSpawn_Time = 10f;

    private const float DeSpawn_Distance = 60f;

    private void Update()
    {
        MapRelocation();
    }

    public void OnEnable()
    {
        mapObject = gameObject;
        enemyMaxSize = Random.Range(1, 6);
        fishMaxSize = Random.Range(1, 15);
    }

    public void OnDisable()
    {

    }

    private void MapRelocation()
    {
        ObjectController _objectController = GameManager.OBJECT;
        SpawnController _spawnController = GameManager.SPAWN;

        Vector2 targetPosition = _objectController.player.transform.position;
        Vector2 myPosition = transform.position;

        float DistanceX = targetPosition.x - myPosition.x;
        float differenceX = Mathf.Abs(DistanceX);

        DistanceX = DistanceX > 0 ? 1 : -1;

        _spawnController.DeSpawn(targetPosition, DeSpawn_Distance + 15);

        if (differenceX > DeSpawn_Distance)
        {
            _spawnController.DeSpawn(targetPosition, DeSpawn_Distance - 15);

            transform.Translate(Vector2.right * DistanceX * 120);

            enemyMaxSize = Random.Range(1, 6);
            fishMaxSize = Random.Range(1, 15);

            MapMonsterSpawn(enemyMaxSize, fishMaxSize);
        }
    }

    public void MapMonsterSpawn(long InEnemySize, long InFishSize)
    {
        Vector2 myPosition = transform.position;
        SpawnController _spawnController = GameManager.SPAWN;

        _spawnController.Spawn(myPosition, InEnemySize, InFishSize, mySpawnNumber);

        StartCoroutine(ReSpawn(ReSpawn_Time, InEnemySize, InFishSize));
    }

    private IEnumerator ReSpawn(float ReSpawnTime, long InEnemySize, long InFishSize)
    {
        Vector2 myPosition = transform.position;
        SpawnController _spawnController = GameManager.SPAWN;

        yield return new WaitForSeconds(ReSpawnTime);

        _spawnController.Spawn(myPosition, ReSpawnSize().Item1, ReSpawnSize().Item2, mySpawnNumber);

        StartCoroutine(ReSpawn(ReSpawnTime, InEnemySize, InFishSize));
    }

    private (long, long) ReSpawnSize()
    {
        long enemyReSpawnSize = 0;
        long fishReSpawnSize = 0;

        foreach (KeyValuePair<long, Character> outCharacterData in GameManager.OBJECT.characterDataList)
        {
            if (outCharacterData.Value.targetSpawnNumber == mySpawnNumber)
            {
                if (GameManager.OBJECT.GetisActive(outCharacterData.Key, ObjectType.Enemy) == false)
                    enemyReSpawnSize++;
                else if (GameManager.OBJECT.GetisActive(outCharacterData.Key, ObjectType.Fish) == false)
                    fishReSpawnSize++;
            }
        }

        return (enemyReSpawnSize, fishReSpawnSize);
    }

    public bool isActive => mapObject.activeSelf;
}