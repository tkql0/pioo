using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public int key;

    private float reSpawnTime;

    public int enemyMaxSize;
    public int fishMaxSize;

    private ObjectController _objectController;
    private SpawnController _spawnController;


    private void Update()
    {
        MapRelocation();
    }

    public void Init()
    {
        reSpawnTime = 10f;

        key = 0;

        _objectController = GameTree.GAME.objectController;
        _spawnController = GameTree.GAME.spawnController;

        MapMonsterSpawn(enemyMaxSize, fishMaxSize);
    }

    public void OnEnable()
    {
        enemyMaxSize = Random.Range(1, 6);
        fishMaxSize = Random.Range(1, 15);
    }

    public void OnDisable()
    {

    }

    private void MapMonsterSpawn(int enemySize, int fishSize)
    {
        _spawnController.Spawn(gameObject, enemySize, fishSize, key);

        StartCoroutine(ReSpawn(reSpawnTime, enemySize, fishSize));
    }

    private void MapRelocation()
    {
        Vector3 targetPosition;
        Vector3 myPosition;

        targetPosition = _objectController.playerList[key / 3].transform.position;
        myPosition = transform.position;

        float DistanceX = targetPosition.x - myPosition.x;
        float differenceX = Mathf.Abs(DistanceX);

        DistanceX = DistanceX > 0 ? 1 : -1;

        if (differenceX > 30.0f)
        {
            _spawnController.DeSpawn(targetPosition);

            transform.Translate(Vector3.right * DistanceX * 60);

            enemyMaxSize = Random.Range(1, 6);
            fishMaxSize = Random.Range(1, 15);

            MapMonsterSpawn(enemyMaxSize, fishMaxSize);
        }
    }

    private IEnumerator ReSpawn(float ReSpawnTime, int enemySize, int fishSize)
    {
        yield return new WaitForSeconds(ReSpawnTime);

        _spawnController.Spawn(gameObject, ReSpawnSize(enemySize, fishSize).Item1, ReSpawnSize(enemySize, fishSize).Item2, key);

        StartCoroutine(ReSpawn(ReSpawnTime, enemySize, fishSize));
    }

    (int, int) ReSpawnSize(int enemySize, int fishSize)
    {
        int enemyReSpawnSize = 0;
        int fishReSpawnSize = 0;

        for (int i = 0; i < _objectController.enemyList.Count; i++)
        {
            if (key == _objectController.enemyDataList[i].key
                && _objectController.enemyList[i].activeSelf)
            {
                enemyReSpawnSize++;
            }
        }
        for (int i = 0; i < _objectController.fishList.Count; i++)
        {
            if (key == _objectController.fishDataList[i].key
                && _objectController.fishList[i].activeSelf)
            {
                fishReSpawnSize++;
            }
        }

        int enemyReSpawnMaxSize = enemySize - enemyReSpawnSize;
        int fishReSpawnMaxSize = fishSize - fishReSpawnSize;

        return (enemyReSpawnMaxSize, fishReSpawnMaxSize);
    }
}
