using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController
{
    public SpawnObject spawnGroupObject;
    private const int Position_X_Min = -20;
    private const int Position_X_Max = 21;

    private const int Position_Y_Min = -3;
    private const int Position_Y_Max = -40;

    private const float DeSpawn_Distance = 45f;

    public void Init()
    {
        
    }

    public void OnEnable()
    {
        spawnGroupObject = GameObject.FindObjectOfType<SpawnObject>();
        spawnGroupObject.Init();
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

        for (int i = 0; i < GameManager.OBJECT.enemyDataList.Count; i++)
        {
            if (!GameManager.OBJECT.enemyDataList[i].enemy.activeSelf)
            {
                GameManager.OBJECT.SetActiveCharacter(i, true);

                GameManager.OBJECT.enemyDataList[i].transform.position
                    = new Vector3(randomPositionX + spawnObject.transform.position.x, 0, 0);

                GameManager.OBJECT.enemyDataList[i].key = key;

                return GameManager.OBJECT.enemyDataList[i].enemy;
            }
        }
        return null;
    }

    private GameObject SpawnFish(GameObject spawnObject, int key)
    {
        int randomPositionX = Random.Range(Position_X_Min, Position_X_Max);
        int randomPositionY = Random.Range(Position_Y_Min, Position_Y_Max);

        for (int i = 0; i < GameManager.OBJECT.fishDataList.Count; i++)
        {
            if (!GameManager.OBJECT.fishDataList[i].Fish.activeSelf)
            {
                GameManager.OBJECT.fishDataList[i].Fish.SetActive(true);

                GameManager.OBJECT.fishDataList[i].transform.position
                    = new Vector3(randomPositionX + spawnObject.transform.position.x, randomPositionY, 0);

                GameManager.OBJECT.fishDataList[i].key = key;

                return GameManager.OBJECT.fishDataList[i].Fish;
            }
        }
        return null;
    }

    private void DistanceEnemyDeSpawn(Vector3 target)
    {
        Vector3 myPosition;

        for (int i = 0; i < GameManager.OBJECT.enemyDataList.Count; i++)
        {
            myPosition = GameManager.OBJECT.enemyDataList[i].enemy.transform.position;

            float DistanceX = target.x - myPosition.x;
            float differenceX = Mathf.Abs(DistanceX);

            if (differenceX > DeSpawn_Distance)
            {
                GameManager.OBJECT.SetActiveCharacter(i, false);
            }
        }
    }

    private void DistanceFishDeSpawn(Vector3 target)
    {
        Vector3 myPosition;

        for (int i = 0; i < GameManager.OBJECT.fishDataList.Count; i++)
        {
            myPosition = GameManager.OBJECT.fishDataList[i].Fish.transform.position;

            float DistanceX = target.x - myPosition.x;
            float differenceX = Mathf.Abs(DistanceX);

            if (differenceX > DeSpawn_Distance)
            {
                GameManager.OBJECT.fishDataList[i].Fish.SetActive(false);
            }
        }
    }

    public GameObject SpawnPlayerWapon(GameObject spawnObject)
    {
        for (int i = 0; i < GameManager.OBJECT.playerWaponDataList.Count; i++)
        {
            if (!GameManager.OBJECT.playerWaponDataList[i].weapon.activeSelf)
            {
                GameManager.OBJECT.playerWaponDataList[i].weapon.SetActive(true);
                GameManager.OBJECT.playerWaponDataList[i].key = 1;

                GameManager.OBJECT.playerWaponDataList[i].transform.position
                    = new Vector3(spawnObject.transform.position.x, spawnObject.transform.position.y, 0);

                return GameManager.OBJECT.playerWaponDataList[i].weapon;
            }
        }
        return null;
    }

    public GameObject SpawnEnemyWapon(GameObject spawnObject)
    {
        for (int i = 0; i < GameManager.OBJECT.enemyWaponDataList.Count; i++)
        {
            if (!GameManager.OBJECT.enemyWaponDataList[i].weapon.activeSelf)
            {
                GameManager.OBJECT.enemyWaponDataList[i].weapon.SetActive(true);
                GameManager.OBJECT.enemyWaponDataList[i].key = 2;

                GameManager.OBJECT.enemyWaponDataList[i].transform.position
                    = new Vector3(spawnObject.transform.position.x, spawnObject.transform.position.y, 0);

                return GameManager.OBJECT.enemyWaponDataList[i].weapon;
            }
        }
        return null;
    }
}