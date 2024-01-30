using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController
{
    public GameObject spawnGroupObject;
    private const int Position_X_Min = -10;
    private const int Position_X_Max = 11;

    private const int Position_Y_Min = -3;
    private const int Position_Y_Max = -20;

    private const float DeSpawn_Distance = 22.5f;

    private const string Object_ObjectGroup = "ObjectGroup";

    public void Init()
    {
        
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

        for (int i = 0; i < GameTree.GAME.objectController.enemyDataList.Count; i++)
        {
            if (!GameTree.GAME.objectController.enemyDataList[i].Enemy.activeSelf)
            {
                GameTree.GAME.objectController.enemyDataList[i].Enemy.SetActive(true);

                GameTree.GAME.objectController.enemyDataList[i].transform.position
                    = new Vector3(randomPositionX + spawnObject.transform.position.x, 0, 0);

                GameTree.GAME.objectController.enemyDataList[i].key = key;

                return GameTree.GAME.objectController.enemyDataList[i].Enemy;
            }
        }
        return null;
    }

    private GameObject SpawnFish(GameObject spawnObject, int key)
    {
        int randomPositionX = Random.Range(Position_X_Min, Position_X_Max);
        int randomPositionY = Random.Range(Position_Y_Min, Position_Y_Max);

        for (int i = 0; i < GameTree.GAME.objectController.fishDataList.Count; i++)
        {
            if (!GameTree.GAME.objectController.fishDataList[i].Fish.activeSelf)
            {
                GameTree.GAME.objectController.fishDataList[i].Fish.SetActive(true);

                GameTree.GAME.objectController.fishDataList[i].transform.position
                    = new Vector3(randomPositionX + spawnObject.transform.position.x, randomPositionY, 0);

                GameTree.GAME.objectController.fishDataList[i].key = key;

                return GameTree.GAME.objectController.fishDataList[i].Fish;
            }
        }
        return null;
    }

    private void DistanceEnemyDeSpawn(Vector3 target)
    {
        Vector3 myPosition;

        for (int i = 0; i < GameTree.GAME.objectController.enemyDataList.Count; i++)
        {
            myPosition = GameTree.GAME.objectController.enemyDataList[i].Enemy.transform.position;

            float DistanceX = target.x - myPosition.x;
            float differenceX = Mathf.Abs(DistanceX);

            if (differenceX > DeSpawn_Distance)
            {
                GameTree.GAME.objectController.enemyDataList[i].key = 99;
                GameTree.GAME.objectController.enemyDataList[i].Enemy.SetActive(false);
            }
        }
    }

    private void DistanceFishDeSpawn(Vector3 target)
    {
        Vector3 myPosition;

        for (int i = 0; i < GameTree.GAME.objectController.fishDataList.Count; i++)
        {
            myPosition = GameTree.GAME.objectController.fishDataList[i].Fish.transform.position;

            float DistanceX = target.x - myPosition.x;
            float differenceX = Mathf.Abs(DistanceX);

            if (differenceX > DeSpawn_Distance)
            {
                GameTree.GAME.objectController.fishDataList[i].key = 99;
                GameTree.GAME.objectController.fishDataList[i].Fish.SetActive(false);
            }
        }
    }

    public GameObject SpawnPlayerWapon(GameObject spawnObject)
    {
        for (int i = 0; i < GameTree.GAME.objectController.playerWaponDataList.Count; i++)
        {
            if (!GameTree.GAME.objectController.playerWaponDataList[i].wapon.activeSelf)
            {
                GameTree.GAME.objectController.playerWaponDataList[i].wapon.SetActive(true);
                GameTree.GAME.objectController.playerWaponDataList[i].key = 1;

                GameTree.GAME.objectController.playerWaponDataList[i].transform.position
                    = new Vector3(spawnObject.transform.position.x, spawnObject.transform.position.y, 0);

                return GameTree.GAME.objectController.playerWaponDataList[i].wapon;
            }
        }
        return null;
    }

    public GameObject SpawnEnemyWapon(GameObject spawnObject)
    {
        for (int i = 0; i < GameTree.GAME.objectController.enemyWaponDataList.Count; i++)
        {
            if (!GameTree.GAME.objectController.enemyWaponDataList[i].wapon.activeSelf)
            {
                GameTree.GAME.objectController.enemyWaponDataList[i].wapon.SetActive(true);
                GameTree.GAME.objectController.playerWaponDataList[i].key = 2;

                GameTree.GAME.objectController.enemyWaponDataList[i].transform.position
                    = new Vector3(spawnObject.transform.position.x, spawnObject.transform.position.y, 0);

                return GameTree.GAME.objectController.enemyWaponDataList[i].wapon;
            }
        }
        return null;
    }
}