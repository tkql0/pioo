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
        spawnGroupObject.StartSpawn();
    }

    public void OnDisable()
    {

    }

    public void Spawn(Vector3 spawnCenter, int enemyCount, int fishCount, int key)
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

    private GameObject SpawnEnemy(Vector3 spawnObject, int key)
    {
        ObjectController _objectController = GameManager.OBJECT;

        int randomPositionX = Random.Range(Position_X_Min, Position_X_Max);

        for (int i = 0; i < _objectController.enemyDataList.Count; i++)
        {
            if (!_objectController.GetActive(i, CharacterType.Enemy))
            {
                _objectController.SetActiveEnemy(i, true);
                
                _objectController.enemyDataList[i].transform.position
                    = new Vector3(randomPositionX + spawnObject.x, 0, 0);

                _objectController.enemyDataList[i].key = key;

                return _objectController.enemyDataList[i].enemy;
            }
        }
        return null;
    }

    private GameObject SpawnFish(Vector3 spawnObject, int key)
    {
        ObjectController _objectController = GameManager.OBJECT;

        int randomPositionX = Random.Range(Position_X_Min, Position_X_Max);
        int randomPositionY = Random.Range(Position_Y_Min, Position_Y_Max);

        for (int i = 0; i < _objectController.fishDataList.Count; i++)
        {
            if (!_objectController.GetActive(i, CharacterType.Fish))
            {
                _objectController.SetActiveFish(i, true);

                _objectController.fishDataList[i].transform.position
                    = new Vector3(randomPositionX + spawnObject.x, randomPositionY, 0);

                _objectController.fishDataList[i].key = key;

                return _objectController.fishDataList[i].fish;
            }
        }
        return null;
    }

    private void DistanceEnemyDeSpawn(Vector3 target)
    {
        ObjectController _objectController = GameManager.OBJECT;

        Vector3 myPosition;

        for (int i = 0; i < _objectController.enemyDataList.Count; i++)
        {
            myPosition = _objectController.enemyDataList[i].transform.position;

            float DistanceX = target.x - myPosition.x;
            float differenceX = Mathf.Abs(DistanceX);

            if (differenceX > DeSpawn_Distance)
            {
                _objectController.SetActiveEnemy(i, false);
                _objectController.enemyDataList[i].key = 99;
            }
        }
    }

    private void DistanceFishDeSpawn(Vector3 target)
    {
        ObjectController _objectController = GameManager.OBJECT;

        Vector3 myPosition;

        for (int i = 0; i < _objectController.fishDataList.Count; i++)
        {
            myPosition = _objectController.fishDataList[i].transform.position;

            float DistanceX = target.x - myPosition.x;
            float differenceX = Mathf.Abs(DistanceX);

            if (differenceX > DeSpawn_Distance)
            {
                _objectController.SetActiveFish(i, false);
                _objectController.fishDataList[i].key = 99;
            }
        }
    }

    public GameObject SpawnPlayerWapon(Vector3 spawnObject)
    {
        ObjectController _objectController = GameManager.OBJECT;

        for (int i = 0; i < _objectController.playerWaponDataList.Count; i++)
        {
            if (!_objectController.playerWaponDataList[i].weapon.activeSelf)
            {
                _objectController.playerWaponDataList[i].weapon.SetActive(true);
                _objectController.playerWaponDataList[i].key = CharacterType.Player;

                _objectController.playerWaponDataList[i].transform.position
                    = new Vector3(spawnObject.x, spawnObject.y, 0);

                return _objectController.playerWaponDataList[i].weapon;
            }
        }
        return null;
    }

    public GameObject SpawnEnemyWapon(Vector3 spawnObject)
    {
        ObjectController _objectController = GameManager.OBJECT;

        for (int i = 0; i < _objectController.enemyWaponDataList.Count; i++)
        {
            if (!_objectController.enemyWaponDataList[i].weapon.activeSelf)
            {
                _objectController.enemyWaponDataList[i].weapon.SetActive(true);
                _objectController.enemyWaponDataList[i].key = CharacterType.Enemy;

                _objectController.enemyWaponDataList[i].transform.position
                    = new Vector3(spawnObject.x, spawnObject.y, 0);

                return _objectController.enemyWaponDataList[i].weapon;
            }
        }
        return null;
    }

    //public GameObject SpawnWapon(GameObject spawnObject)
    //{
    //    ObjectController _objectController = GameManager.OBJECT;

    //    for (int i = 0; i < _objectController.WaponDataList.Count; i++)
    //    {
    //        if (!_objectController.WaponDataList[i].weapon.activeSelf)
    //        {
    //            _objectController.WaponDataList[i].weapon.SetActive(true);
    //            _objectController.WaponDataList[i].key = CharacterType.Enemy;

    //            _objectController.WaponDataList[i].transform.position
    //                = new Vector3(spawnObject.transform.position.x, spawnObject.transform.position.y, 0);

    //            return _objectController.WaponDataList[i].weapon;
    //        }
    //    }
    //    return null;
    //}
}