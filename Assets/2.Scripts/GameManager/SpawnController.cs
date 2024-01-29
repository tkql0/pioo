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

        for (int i = 0; i < _objectController.enemyDataList.Count; i++)
        {
            if (!_objectController.enemyDataList[i].Enemy.activeSelf)
            {
                _objectController.enemyDataList[i].Enemy.SetActive(true);

                _objectController.enemyDataList[i].transform.position
                    = new Vector3(randomPositionX + spawnObject.transform.position.x, 0, 0);

                _objectController.enemyDataList[i].key = key;

                return _objectController.enemyDataList[i].Enemy;
            }
        }
        return null;
    }

    private GameObject SpawnFish(GameObject spawnObject, int key)
    {
        int randomPositionX = Random.Range(Position_X_Min, Position_X_Max);
        int randomPositionY = Random.Range(Position_Y_Min, Position_Y_Max);

        for (int i = 0; i < _objectController.fishDataList.Count; i++)
        {
            if (!_objectController.fishDataList[i].Fish.activeSelf)
            {
                _objectController.fishDataList[i].Fish.SetActive(true);

                _objectController.fishDataList[i].transform.position
                    = new Vector3(randomPositionX + spawnObject.transform.position.x, randomPositionY, 0);

                _objectController.fishDataList[i].key = key;

                return _objectController.fishDataList[i].Fish;
            }
        }
        return null;
    }

    private void DistanceEnemyDeSpawn(Vector3 target)
    {
        Vector3 myPosition;

        for (int i = 0; i < _objectController.enemyDataList.Count; i++)
        {
            myPosition = _objectController.enemyDataList[i].Enemy.transform.position;

            float DistanceX = target.x - myPosition.x;
            float differenceX = Mathf.Abs(DistanceX);

            if (differenceX > DeSpawn_Distance)
            {
                _objectController.enemyDataList[i].key = 99;
                _objectController.enemyDataList[i].Enemy.SetActive(false);
            }
        }
    }

    private void DistanceFishDeSpawn(Vector3 target)
    {
        Vector3 myPosition;

        for (int i = 0; i < _objectController.fishDataList.Count; i++)
        {
            myPosition = _objectController.fishDataList[i].Fish.transform.position;

            float DistanceX = target.x - myPosition.x;
            float differenceX = Mathf.Abs(DistanceX);

            if (differenceX > DeSpawn_Distance)
            {
                _objectController.fishDataList[i].key = 99;
                _objectController.fishDataList[i].Fish.SetActive(false);
            }
        }
    }

    public GameObject SpawnPlayerWapon(GameObject spawnObject)
    {
        for (int i = 0; i < _objectController.playerWaponDataList.Count; i++)
        {
            if (!_objectController.playerWaponDataList[i].wapon.activeSelf)
            {
                _objectController.playerWaponDataList[i].wapon.SetActive(true);
                _objectController.playerWaponDataList[i].key = 1;

                _objectController.playerWaponDataList[i].transform.position
                    = new Vector3(spawnObject.transform.position.x, spawnObject.transform.position.y, 0);

                return _objectController.playerWaponDataList[i].wapon;
            }
        }
        return null;
    }

    public GameObject SpawnEnemyWapon(GameObject spawnObject)
    {
        for (int i = 0; i < _objectController.enemyWaponDataList.Count; i++)
        {
            if (!_objectController.enemyWaponDataList[i].wapon.activeSelf)
            {
                _objectController.enemyWaponDataList[i].wapon.SetActive(true);
                _objectController.playerWaponDataList[i].key = 2;

                _objectController.enemyWaponDataList[i].transform.position
                    = new Vector3(spawnObject.transform.position.x, spawnObject.transform.position.y, 0);

                return _objectController.enemyWaponDataList[i].wapon;
            }
        }
        return null;
    }
}