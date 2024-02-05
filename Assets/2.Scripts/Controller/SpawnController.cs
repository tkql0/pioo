using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController
{
    private SpawnObject spawnGroupObject;

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

    public void Spawn(Vector2 spawnCenterPosition, long enemyCount, long fishCount, int key)
    {
        for (long i = 0; i < enemyCount; i++)
        {
            SpawnObject(spawnCenterPosition, key, ObjectType.Enemy);
        }

        for (long i = 0; i < fishCount; i++)
        {
            SpawnObject(spawnCenterPosition, key, ObjectType.Fish);
        }
    }

    public void DeSpawn(Vector2 target)
    {
        DistanceObjectDeSpawn(target, ObjectType.Enemy);
        DistanceObjectDeSpawn(target, ObjectType.Fish);
    }

    private GameObject SpawnObject(Vector2 spawnCenterPosition, int key, ObjectType character)
    {
        ObjectController _objectController = GameManager.OBJECT;

        switch (character)
        {
            case ObjectType.Enemy:
                foreach (KeyValuePair<long, EnemyCharacter> enemyNumber in _objectController.enemyDataList)
                {
                    if (!_objectController.GetisActive(enemyNumber.Key, ObjectType.Enemy))
                    {
                        _objectController.SetActive(enemyNumber.Key, ObjectType.Enemy, true);
                        _objectController.SetSpawnPosition(enemyNumber.Key, ObjectType.Enemy, spawnCenterPosition);

                        enemyNumber.Value.key = key;

                        return enemyNumber.Value.enemy;
                    }
                }
                break;
            case ObjectType.Fish:
                foreach (KeyValuePair<long, FishCharacter> fishNumber in _objectController.fishDataList)
                {
                    if (!_objectController.GetisActive(fishNumber.Key, ObjectType.Fish))
                    {
                        _objectController.SetActive(fishNumber.Key, ObjectType.Fish, true);
                        _objectController.SetSpawnPosition(fishNumber.Key, ObjectType.Fish, spawnCenterPosition);

                        fishNumber.Value.key = key;

                        return fishNumber.Value.fish;
                    }
                }
                break;
        }
        return null;
    }

    private void DistanceObjectDeSpawn(Vector2 target, ObjectType character)
    {
        ObjectController _objectController = GameManager.OBJECT;

        Vector2 myPosition;

        switch (character)
        {
            case ObjectType.Enemy:
                foreach (KeyValuePair<long, EnemyCharacter> enemyNumber in _objectController.enemyDataList)
                {
                    myPosition = enemyNumber.Value.transform.position;

                    float DistanceX = target.x - myPosition.x;
                    float differenceX = Mathf.Abs(DistanceX);

                    if (differenceX > DeSpawn_Distance)
                    {
                        _objectController.SetActive(enemyNumber.Key, ObjectType.Enemy, false);
                        enemyNumber.Value.key = 99;
                    }
                }
                break;
            case ObjectType.Fish:
                foreach (KeyValuePair<long, FishCharacter> fishNumber in _objectController.fishDataList)
                {
                    myPosition = fishNumber.Value.transform.position;

                    float DistanceX = target.x - myPosition.x;
                    float differenceX = Mathf.Abs(DistanceX);

                    if (differenceX > DeSpawn_Distance)
                    {
                        _objectController.SetActive(fishNumber.Key, ObjectType.Fish, false);
                        fishNumber.Value.key = 99;
                    }
                }
                break;
        }
    }

    public GameObject SpawnPlayerWapon(Vector2 spawnObject)
    {
        ObjectController _objectController = GameManager.OBJECT;

        for (int i = 0; i < _objectController.playerWaponDataList.Count; i++)
        {
            if (!_objectController.playerWaponDataList[i].weapon.activeSelf)
            {
                _objectController.playerWaponDataList[i].weapon.SetActive(true);
                _objectController.playerWaponDataList[i].key = ObjectType.Player;

                _objectController.playerWaponDataList[i].transform.position
                    = new Vector2(spawnObject.x, spawnObject.y);

                return _objectController.playerWaponDataList[i].weapon;
            }
        }
        return null;
    }

    public GameObject SpawnEnemyWapon(Vector2 spawnObject)
    {
        ObjectController _objectController = GameManager.OBJECT;

        for (int i = 0; i < _objectController.enemyWaponDataList.Count; i++)
        {
            if (!_objectController.enemyWaponDataList[i].weapon.activeSelf)
            {
                _objectController.enemyWaponDataList[i].weapon.SetActive(true);
                _objectController.enemyWaponDataList[i].key = ObjectType.Enemy;

                _objectController.enemyWaponDataList[i].transform.position
                    = new Vector2(spawnObject.x, spawnObject.y);

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
    

    private void MapSpawn(long InIndex)
    {
        ObjectController _objectController = GameManager.OBJECT;

        if (_objectController.mapDataList.TryGetValue(InIndex, out var outCharacter) == false)
            return;

        if (outCharacter.gameObject.activeSelf)
            outCharacter.MapMonsterSpawn(outCharacter.enemyMaxSize, outCharacter.fishMaxSize);
    }
}