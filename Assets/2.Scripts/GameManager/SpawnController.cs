using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController
{
    public GameObject spawnGroupObject;

    private ObjectController _objectController;

    private const int Position_X_Min = -10;
    private const int Position_X_Max = 11;

    private const int Position_Y_Min = -3;
    private const int Position_Y_Max = -20;

    private const float DeSpawn_Distance = 22.5f;

    private const string Object_ObjectGroup = "ObjectGroup";

    public void Init()
    {
        _objectController = GameTree.GAME.objectController;
    }

    public void OnEnable()
    {
        Init();

        spawnGroupObject = new GameObject(Object_ObjectGroup);
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
            SpawnFish(spawnCenter, key);
        }
    }

    public void DeSpawn(Vector3 target)
    {
        DistanceEnemyDeSpawn(target);
        DistanceFishDeSpawn(target);
    }

    private GameObject SpawnEnemy(GameObject spawnObject, int key)
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

    private GameObject SpawnFish(GameObject spawnObject, int key)
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

                _objectController.fishDataList[i].key = key;

                return _objectController.fishList[i];
            }
        }
        return null;
    }

    private void DistanceEnemyDeSpawn(Vector3 target)
    {
        Vector3 myPosition;

        for (int i = 0; i < _objectController.enemyList.Count; i++)
        {
            //if (key != _objectController.enemyDataList[i].key)
                //return;

            myPosition = _objectController.enemyList[i].transform.position;

            float DistanceX = target.x - myPosition.x;
            float differenceX = Mathf.Abs(DistanceX);

            if (differenceX > DeSpawn_Distance)
            {
                _objectController.enemyDataList[i].key = 99;
                _objectController.enemyList[i].SetActive(false);
            }
        }
    }

    private void DistanceFishDeSpawn(Vector3 target)
    {
        Vector3 myPosition;

        for (int i = 0; i < _objectController.fishList.Count; i++)
        {
            //if (key != _objectController.fishDataList[i].key)
                //return;

            myPosition = _objectController.fishList[i].transform.position;

            float DistanceX = target.x - myPosition.x;
            float differenceX = Mathf.Abs(DistanceX);

            if (differenceX > DeSpawn_Distance)
            {
                _objectController.fishDataList[i].key = 99;
                _objectController.fishList[i].SetActive(false);
            }
        }
    }
}