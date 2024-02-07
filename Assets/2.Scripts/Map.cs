using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public void Init()
    {
        
    }

    public void OnEnable()
    {
        map = gameObject;
        enemyMaxSize = Random.Range(1, 6);
        fishMaxSize = Random.Range(1, 15);

        //StartCoroutine(ReSpawn(ReSpawn_Time, enemyMaxSize, fishMaxSize));
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

        if (differenceX > DeSpawn_Distance)
        {
            _spawnController.DeSpawn(targetPosition);

            transform.Translate(Vector2.right * DistanceX * 120);

            enemyMaxSize = Random.Range(1, 6);
            fishMaxSize = Random.Range(1, 15);

            MapMonsterSpawn(enemyMaxSize, fishMaxSize);
        }
    }

    public void MapMonsterSpawn(long enemySize, long fishSize)
    {
        Vector2 myPosition = transform.position;
        SpawnController _spawnController = GameManager.SPAWN;

        _spawnController.Spawn(myPosition, enemySize, fishSize, key);

        StartCoroutine(ReSpawn(ReSpawn_Time, enemySize, fishSize));
    }

    private IEnumerator ReSpawn(float ReSpawnTime, long enemySize, long fishSize)
    {
        Vector2 myPosition = transform.position;
        SpawnController _spawnController = GameManager.SPAWN;

        yield return new WaitForSeconds(ReSpawnTime);

        _spawnController.Spawn(myPosition, ReSpawnSize(enemySize,
            fishSize).Item1, ReSpawnSize(enemySize, fishSize).Item2, key);

        StartCoroutine(ReSpawn(ReSpawnTime, enemySize, fishSize));
    }

    private (long, long) ReSpawnSize(long enemySize, long fishSize)
    {
        ObjectController _objectController = GameManager.OBJECT;

        long enemyReSpawnSize = 0;
        long fishReSpawnSize = 0;

        foreach (KeyValuePair<long, Character> enemyData in _objectController.characterList)
        {
            if (enemyData.Value.key == ObjectType.Enemy
                && _objectController.GetisActive(enemyData.Key, ObjectType.Enemy)
                && enemyData.Value.spawnObjectKey == key)
                enemyReSpawnSize++;
        }

        foreach (KeyValuePair<long, Character> fishData in _objectController.characterList)
        {
            if (fishData.Value.key == ObjectType.Fish
                && _objectController.GetisActive(fishData.Key, ObjectType.Fish)
                && fishData.Value.spawnObjectKey == key)
                fishReSpawnSize++;
        }

        long enemyReSpawnMaxSize = enemySize - enemyReSpawnSize;
        long fishReSpawnMaxSize = fishSize - fishReSpawnSize;

        return (enemyReSpawnMaxSize, fishReSpawnMaxSize);
    }
}