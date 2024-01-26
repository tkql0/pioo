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
    public void Spawn(GameObject spawnCenter, int enemyCount, int fishCount, int key)
    {
        for (int i = 0; i < enemyCount; i++)
        {
            SpawnEnemy(spawnCenter, key);
        }

        for (int i = 0; i < fishCount; i++)
        {
            SpawnFish(spawnCenter);
        }
    }

    public void DeSpawn(int key, Vector3 target)
    {
        DistanceEnemyDeSpawn(key, target);
        DistanceFishDeSpawn(key, target);
    }

    GameObject SpawnEnemy(GameObject spawnObject, int key)
    {
        int randomPositionX = Random.Range(Position_X_Min, Position_X_Max);

        for (int i = 0; i < _objectController.enemyList.Count; i++)
        {
            if (!_objectController.enemyList[i].activeSelf)
            {
                _objectController.enemyList[i].SetActive(true);

                _objectController.enemyList[i].transform.position
                    = new Vector3(randomPositionX + spawnObject.transform.position.x, 0, 0);

                _objectController.enemyDataList[i].key = key;

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

    void DistanceEnemyDeSpawn(int key, Vector3 target)
    {
        Vector3 myPosition;

        for (int i = 0; i < _objectController.enemyList.Count; i++)
        {
            if (key != _objectController.enemyDataList[i].key)
                return;

            myPosition = _objectController.enemyList[i].transform.position;

            float DistanceX = target.x - myPosition.x;
            float differenceX = Mathf.Abs(DistanceX);

            if (differenceX > 22.5f)
                _objectController.enemyList[i].SetActive(false);
        }
    }

    void DistanceFishDeSpawn(int key, Vector3 target)
    {
        Vector3 myPosition;

        for (int i = 0; i < _objectController.fishList.Count; i++)
        {
            if (key != _objectController.fishDataList[i].key)
                return;

            myPosition = _objectController.fishList[i].transform.position;

            float DistanceX = target.x - myPosition.x;
            float differenceX = Mathf.Abs(DistanceX);

            if (differenceX > 22.5f)
                _objectController.fishList[i].SetActive(false);
        }
    }
}