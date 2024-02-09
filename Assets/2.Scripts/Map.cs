using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public GameObject map;

    public long key;

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
        map = gameObject;
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

        _spawnController.Spawn(myPosition, InEnemySize, InFishSize, key);

        StartCoroutine(ReSpawn(ReSpawn_Time, InEnemySize, InFishSize));
    }

    private IEnumerator ReSpawn(float ReSpawnTime, long InEnemySize, long InFishSize)
    {
        Vector2 myPosition = transform.position;
        SpawnController _spawnController = GameManager.SPAWN;

        yield return new WaitForSeconds(ReSpawnTime);

        _spawnController.Spawn(myPosition, ReSpawnSize(InEnemySize,
            InFishSize).Item1, ReSpawnSize(InEnemySize, InFishSize).Item2, key);

        StartCoroutine(ReSpawn(ReSpawnTime, InEnemySize, InFishSize));
    }

    private (long, long) ReSpawnSize(long InEnemySize, long InFishSize)
    {
        ObjectController _objectController = GameManager.OBJECT;

        long enemyReSpawnSize = 0;
        long fishReSpawnSize = 0;

        foreach (KeyValuePair<long, Character> enemyData in _objectController.characterDataList)
        {
            if (_objectController.GetisActive(enemyData.Key, ObjectType.Enemy)
                && enemyData.Value.spawnObjectKey == key)
                enemyReSpawnSize++;
        }

        foreach (KeyValuePair<long, Character> fishData in _objectController.characterDataList)
        {
            if (_objectController.GetisActive(fishData.Key, ObjectType.Fish)
                && fishData.Value.spawnObjectKey == key)
                fishReSpawnSize++;
        }

        long enemyReSpawnMaxSize = InEnemySize - enemyReSpawnSize;
        long fishReSpawnMaxSize = InFishSize - fishReSpawnSize;

        return (enemyReSpawnMaxSize, fishReSpawnMaxSize);
    }
}