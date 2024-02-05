using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public GameObject map;

    public int key;

    private const float reSpawnTime =  20f;

    public long enemyMaxSize;
    public long fishMaxSize;

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

        if (differenceX > 60.0f)
        {
            _spawnController.DeSpawn(targetPosition);

            transform.Translate(Vector2.right * DistanceX * 120);

            enemyMaxSize = Random.Range(1, 12);
            fishMaxSize = Random.Range(1, 30);

            MapMonsterSpawn(enemyMaxSize, fishMaxSize);
        }
    }

    public void MapMonsterSpawn(long enemySize, long fishSize)
    {
        Vector2 myPosition = transform.position;
        SpawnController _spawnController = GameManager.SPAWN;

        _spawnController.Spawn(myPosition, enemySize, fishSize, key);

        StartCoroutine(ReSpawn(reSpawnTime, enemySize, fishSize));
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

        foreach (KeyValuePair<long, EnemyCharacter> enemyData 
            in _objectController.enemyDataList)
        {
            if (key == enemyData.Value.key
                && _objectController.GetisActive(enemyData.Key, ObjectType.Enemy))
                enemyReSpawnSize++;
        }

        foreach (KeyValuePair<long, FishCharacter> fishData 
            in _objectController.fishDataList)
        {
            if (key == fishData.Value.key
                && _objectController.GetisActive(fishData.Key, ObjectType.Fish))
                fishReSpawnSize++;
        }

        long enemyReSpawnMaxSize = enemySize - enemyReSpawnSize;
        long fishReSpawnMaxSize = fishSize - fishReSpawnSize;

        return (enemyReSpawnMaxSize, fishReSpawnMaxSize);
    }
}