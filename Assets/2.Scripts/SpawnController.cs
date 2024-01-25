using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController
{
    public GameObject spawnGroupObject;
    ObjectController _objectController;

    private const int Position_X_Min = -10;
    private const int Position_X_Max = 11;

    private const int Position_Y_Min = -3;
    private const int Position_Y_Max = -20;

    void Init()
    {
        _objectController = GameTree.GAME.objectController;
    }

    public void OnEnable()
    {
        Init();

        spawnGroupObject = new GameObject("ObjectGroup");
        SpawnObject spawnObject = spawnGroupObject.AddComponent<SpawnObject>();

        spawnObject.Init();
    }

    public void OnDisable()
    {

    }
    public void Spawn(GameObject spawnCenter)
    {
        int enemyCount = Random.Range(1, 6);
        int fishCount = Random.Range(1, 15);

        //_mapController.enemyMaxSize = enemyCount;

        for (int i = 0; i < enemyCount; i++)
        {
            SpawnEnemy(spawnCenter);
        }

        for (int i = 0; i < fishCount; i++)
        {
            SpawnFish(spawnCenter);
        }
    }

    public void DeSpawn()
    {

    }

    GameObject SpawnEnemy(GameObject spawnObject)
    {
        int randomPositionX = Random.Range(Position_X_Min, Position_X_Max);

        for (int i = 0; i < _objectController.enemyList.Count; i++)
        {
            if (!_objectController.enemyList[i].activeSelf)
            {
                _objectController.enemyList[i].SetActive(true);

                _objectController.enemyList[i].transform.position
                    = new Vector3(randomPositionX + spawnObject.transform.position.x, 0, 0);

                return _objectController.enemyList[i];
            }
        }
        return null;
    }

    GameObject SpawnFish(GameObject spawnObject)
    {
        int randomPositionX = Random.Range(Position_X_Min, Position_X_Max);
        int randomPositionY = Random.Range(Position_Y_Min, Position_Y_Max);

        for (int i = 0; i < _objectController.fishList.Count; i++)
        {
            if (!_objectController.fishList[i].activeSelf)
            {
                _objectController.fishList[i].SetActive(true);

                _objectController.fishList[i].transform.position
                    = new Vector3(randomPositionX + spawnObject.transform.position.x, randomPositionY, 0);

                return _objectController.fishList[i];
            }
        }
        return null;
    }
}