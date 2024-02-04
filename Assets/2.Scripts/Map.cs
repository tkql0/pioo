using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public GameObject map;

    public int key;

    private const float reSpawnTime =  20f;

    public int enemyMaxSize;
    public int fishMaxSize;

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

        Vector3 targetPosition = _objectController.player.transform.position;
        Vector3 myPosition = transform.position;

        float DistanceX = targetPosition.x - myPosition.x;
        float differenceX = Mathf.Abs(DistanceX);

        DistanceX = DistanceX > 0 ? 1 : -1;

        if (differenceX > 60.0f)
        {
            _spawnController.DeSpawn(targetPosition);

            transform.Translate(Vector3.right * DistanceX * 120);

            enemyMaxSize = Random.Range(1, 12);
            fishMaxSize = Random.Range(1, 30);

            MapMonsterSpawn(enemyMaxSize, fishMaxSize);
        }
    }

    public void MapMonsterSpawn(int enemySize, int fishSize)
    {
        Vector3 myPosition = transform.position;

        SpawnController _spawnController = GameManager.SPAWN;

        _spawnController.Spawn(myPosition, enemySize, fishSize, key);

        StartCoroutine(ReSpawn(reSpawnTime, enemySize, fishSize));
    }

    private IEnumerator ReSpawn(float ReSpawnTime, int enemySize, int fishSize)
    {
        Vector3 myPosition = transform.position;

        SpawnController _spawnController = GameManager.SPAWN;

        yield return new WaitForSeconds(ReSpawnTime);

        _spawnController.Spawn(myPosition, ReSpawnSize(enemySize,
            fishSize).Item1, ReSpawnSize(enemySize, fishSize).Item2, key);

        StartCoroutine(ReSpawn(ReSpawnTime, enemySize, fishSize));
    }

    private (int, int) ReSpawnSize(int enemySize, int fishSize)
    {
        ObjectController _objectController = GameManager.OBJECT;

        int enemyReSpawnSize = 0;
        int fishReSpawnSize = 0;

        for (int i = 0; i < _objectController.enemyDataList.Count; i++)
        {
            if (key == _objectController.enemyDataList[i].key
                && _objectController.GetisActive(i, ObjectType.Enemy))
            {
                enemyReSpawnSize++;
            }
        }

        for (int i = 0; i < _objectController.fishDataList.Count; i++)
        {
            if (key == _objectController.fishDataList[i].key
                && _objectController.GetisActive(i, ObjectType.Fish))
            {
                fishReSpawnSize++;
            }
        }

        int enemyReSpawnMaxSize = enemySize - enemyReSpawnSize;
        int fishReSpawnMaxSize = fishSize - fishReSpawnSize;

        return (enemyReSpawnMaxSize, fishReSpawnMaxSize);
    }
}