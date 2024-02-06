using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    private long _mapSpawnConut;
    private long _enemySpawnConut;
    private long _fishSpawnConut;
    //private long _playerWeaponSpawnConut;
    //private long _enemyWeaponSpawnConut;
    private long _weaponSpawnConut;

    private const int Left_MapSpawn = -1;
    private const int Right_MapSpawn = 2;

    private const string Prefab_Player = "Prefabs/Player";
    private const string Prefab_Map = "Prefabs/Map";
    private const string Prefab_Enemy = "Prefabs/Enemy";
    private const string Prefab_Fish = "Prefabs/Fish";
    private const string Prefab_EnemyWapon = "Prefabs/EnemyAttack";
    private const string Prefab_PlayerWapon = "Prefabs/PlayerAttack";

    public void Init()
    {
        _fishSpawnConut = 0;
        _enemySpawnConut = 0;
        _mapSpawnConut = 0;
       _weaponSpawnConut = 0;
    }

    public void OnEnable()
    {
        
    }

    public void OnDisable()
    {

    }

    public void StartSpawn()
    {
        ObjectController _objectController = GameManager.OBJECT;

        SpawnPool(ObjectType.Player);

        Vector2 targetPosition = _objectController.player.transform.position;

        SpawnMap(targetPosition);
        SpawnPool(ObjectType.EnemyWeapon);
        SpawnPool(ObjectType.PlayerWeapon);

        foreach (KeyValuePair<long, Map> mapNumber in _objectController.mapDataList)
        {
            SpawnPool(ObjectType.Enemy);
            SpawnPool(ObjectType.Fish);

            mapNumber.Value.key = mapNumber.Key;
        }
    }

    private void SpawnPool(ObjectType character)
    {
        int maxSize = 0;

        ObjectController _objectController = GameManager.OBJECT;

        switch (character)
        {
            case ObjectType.Player:
                GameObject playerObject = Resources.Load<GameObject>(Prefab_Player);
                GameObject playersObjects = Instantiate(playerObject);

                _objectController.player = playersObjects.GetComponent<Player>();
                break;
            case ObjectType.Enemy:
                maxSize = 20;

                for (int i = 0; i < maxSize; i++)
                {
                    GameObject EnemysObject = Resources.Load<GameObject>(Prefab_Enemy);
                    GameObject EnemysObjects = Instantiate(EnemysObject, transform);

                    _objectController.enemyDataList.Add(_enemySpawnConut,
                        EnemysObjects.GetComponent<EnemyCharacter>());
                    _enemySpawnConut++;
                    EnemysObjects.SetActive(false);
                }
                break;
            case ObjectType.Fish:
                maxSize = 50;

                for (int i = 0; i < maxSize; i++)
                {
                    GameObject FishsObject = Resources.Load<GameObject>(Prefab_Fish);
                    GameObject FishsObjects = Instantiate(FishsObject, transform);

                    _objectController.fishDataList.Add(_fishSpawnConut,
                        FishsObjects.GetComponent<FishCharacter>());
                    _fishSpawnConut++;
                    FishsObjects.SetActive(false);
                }
                break;
            case ObjectType.Map:
                break;
            case ObjectType.EnemyWeapon:
                maxSize = 20;

                for (int i = 0; i < maxSize; i++)
                {
                    GameObject EnemyAttackObject = Resources.Load<GameObject>(Prefab_EnemyWapon);
                    GameObject EnemysAttackObject = Instantiate(EnemyAttackObject, transform);

                    _objectController.weaponDataList.Add(_weaponSpawnConut,
                        EnemysAttackObject.GetComponent<Weapon>());
                    _objectController.weaponDataList[_weaponSpawnConut].key = ObjectType.EnemyWeapon;
                    _weaponSpawnConut++;
                    EnemysAttackObject.SetActive(false);
                }
                break;
            case ObjectType.PlayerWeapon:
                maxSize = 20;

                for (int i = 0; i < maxSize; i++)
                {
                    GameObject PlayerAttackObject = Resources.Load<GameObject>(Prefab_PlayerWapon);
                    GameObject PlayersAttackObject = Instantiate(PlayerAttackObject, transform);

                    _objectController.weaponDataList.Add(_weaponSpawnConut,
                        PlayersAttackObject.GetComponent<Weapon>());
                    _objectController.weaponDataList[_weaponSpawnConut].key = ObjectType.PlayerWeapon;
                    _weaponSpawnConut++;
                    PlayersAttackObject.SetActive(false);
                }
                break;
        }
    }

    private void SpawnMap(Vector2 spawnCenter)
    {
        ObjectController _objectController = GameManager.OBJECT;

        for (int i = Left_MapSpawn; i < Right_MapSpawn; i++)
        {
            GameObject MapsObject = Resources.Load<GameObject>(Prefab_Map);
            GameObject MapsObjects = Instantiate(MapsObject,
                new Vector2(spawnCenter.x + (i * 40), 0), Quaternion.identity);
            _objectController.mapDataList.Add(_mapSpawnConut, MapsObjects.GetComponent<Map>());
            _mapSpawnConut++;
        }
    }
}