using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    private ObjectController _objectController;
    private SpawnController _spawnController;

    public int enemyMaxSize;
    public int fishMaxSize;

    public int enemyReSpawnSize;
    public int fishReSpawnSize;

    public int enemyReSpawnMaxSize;
    public int fishReSpawnMaxSize;

    public int key;

    private void Update()
    {
        //if (!Input.anyKey)
        //    return;
        MapRelocation();
    }

    public void Init()
    {
        enemyMaxSize = 0;
        fishMaxSize = 0;

        enemyReSpawnSize = 0;
        fishReSpawnSize = 0;

        enemyReSpawnMaxSize = 0;
        fishReSpawnMaxSize = 0;

        key = 0;

        _objectController = GameTree.GAME.objectController;
        _spawnController = GameTree.GAME.spawnController;

        MapMonsterSpawn();

        StartCoroutine(ReSpawn(5f));
    }

    public void OnEnable()
    {
        //MapMonsterSpawn();
    }

    public void OnDisable()
    {


    }

    void MapMonsterSpawn()
    {
        enemyMaxSize = Random.Range(1, 6);
        fishMaxSize = Random.Range(1, 15);

        _spawnController.Spawn(gameObject, enemyMaxSize, fishMaxSize, key);
    }

    public void MapRelocation()
    {
        Vector3 targetPosition;
        Vector3 myPosition;

        targetPosition = _objectController.playerList[key].transform.position;
        myPosition = transform.position;

        float DistanceX = targetPosition.x - myPosition.x;
        float differenceX = Mathf.Abs(DistanceX);

        DistanceX = DistanceX > 0 ? 1 : -1;

        if (differenceX > 30.0f)
        {
            _spawnController.DeSpawn(key, targetPosition);

            transform.Translate(Vector3.right * DistanceX * 60);
            fishReSpawnSize = 0;
            enemyReSpawnSize = 0;
            enemyReSpawnMaxSize = 0;
            fishReSpawnMaxSize = 0;
            MapMonsterSpawn();
        }
    }

    IEnumerator ReSpawn(float ReSpawnTime)
    {
        yield return new WaitForSeconds(ReSpawnTime);

        fishReSpawnSize = 0;
        enemyReSpawnSize = 0;
        enemyReSpawnMaxSize = 0;
        fishReSpawnMaxSize = 0;

        for (int i = 0; i < enemyMaxSize; i++)
        {
            if (key == _objectController.enemyDataList[i].key && _objectController.enemyList[i].activeSelf)
            {
                enemyReSpawnSize++;
            }
        }
        for (int i = 0; i < fishMaxSize; i++)
        {
            if (key == _objectController.fishDataList[i].key && _objectController.fishList[i].activeSelf)
            {
                fishReSpawnSize++;
            }
        }

        enemyReSpawnMaxSize = enemyMaxSize - enemyReSpawnSize;
        fishReSpawnMaxSize = fishMaxSize - fishReSpawnSize;

        _spawnController.Spawn(gameObject, enemyReSpawnMaxSize, fishReSpawnMaxSize, key);

        StartCoroutine(ReSpawn(ReSpawnTime));
    }
}
