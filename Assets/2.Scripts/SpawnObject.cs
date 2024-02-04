using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public int maxSize;
    private long _mapSpawnConut;
    private long _enemySpawnConut;
    private long _fishSpawnConut;
    private long _playerWaponSpawnConut;
    private long _enemyWaponSpawnConut;
    private long _waponSpawnConut;

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
        maxSize = 0;
        _fishSpawnConut = 0;
        _enemySpawnConut = 0;
        _mapSpawnConut = 0;
        _playerWaponSpawnConut = 0;
        _enemyWaponSpawnConut = 0;
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

        SpawnMyPlayer();

        Vector3 targetPosition = _objectController.player.transform.position;

        SpawnMap(targetPosition);
        SpawnPlayerAttackPool();
        SpawnEnemyAttackPool();
        //SpawnAttackPool();

        for (int i = 0; i < _objectController.mapDataList.Count; i++)
        {
            SpawnMonsterPool();
            SpawnFishPool();

            _objectController.mapDataList[i].key = i;
        }
    }

    private void SpawnMyPlayer()
    {
        ObjectController _objectController = GameManager.OBJECT;

        GameObject playersObject = Resources.Load<GameObject>(Prefab_Player);
        GameObject playersObjects = Instantiate(playersObject, transform);

        _objectController.player = playersObjects.GetComponent<Player>();
    }

    private void SpawnMap(Vector3 spawnCenter)
    {
        ObjectController _objectController = GameManager.OBJECT;

        for (int i = Left_MapSpawn; i < Right_MapSpawn; i++)
        {
            GameObject MapsObject = Resources.Load<GameObject>(Prefab_Map);
            GameObject MapsObjects = Instantiate(MapsObject,
                new Vector3(spawnCenter.x + (i * 40), 0, 0), Quaternion.identity);
            _objectController.mapDataList.Add(_mapSpawnConut, MapsObjects.GetComponent<Map>());
            _mapSpawnConut++;
        }
    }

    private void SpawnMonsterPool()
    {
        ObjectController _objectController = GameManager.OBJECT;

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
    }

    private void SpawnFishPool()
    {
        ObjectController _objectController = GameManager.OBJECT;

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
    }

    private void SpawnPlayerAttackPool()
    {
        ObjectController _objectController = GameManager.OBJECT;

        maxSize = 10;

        for (int i = 0; i < maxSize; i++)
        {
            GameObject PlayerAttackObject = Resources.Load<GameObject>(Prefab_PlayerWapon);
            GameObject PlayersAttackObject = Instantiate(PlayerAttackObject, transform);

            _objectController.playerWaponDataList.Add(_playerWaponSpawnConut,
                PlayersAttackObject.GetComponent<Weapon>());
            _objectController.playerWaponDataList[i].key = ObjectType.Player;
            _playerWaponSpawnConut++;
            PlayersAttackObject.SetActive(false);
        }
    }

    private void SpawnEnemyAttackPool()
    {
        ObjectController _objectController = GameManager.OBJECT;

        maxSize = 15;

        for (int i = 0; i < maxSize; i++)
        {
            GameObject EnemyAttackObject = Resources.Load<GameObject>(Prefab_EnemyWapon);
            GameObject EnemysAttackObject = Instantiate(EnemyAttackObject, transform);

            _objectController.enemyWaponDataList.Add(_enemyWaponSpawnConut,
                EnemysAttackObject.GetComponent<Weapon>());
            _objectController.enemyWaponDataList[i].key = ObjectType.Enemy;
            _enemyWaponSpawnConut++;
            EnemysAttackObject.SetActive(false);
        }
    }

    //private void SpawnAttackPool()
    //{
    //    ObjectController _objectController = GameManager.OBJECT;

    //    maxSize = 20;

    //    for (int i = 0; i < maxSize; i++)
    //    {
    //        GameObject EnemyAttackObject = Resources.Load<GameObject>(Prefab_EnemyWapon);
    //        GameObject EnemysAttackObject = Instantiate(EnemyAttackObject, transform);

    //        _objectController.WaponDataList.Add(_waponSpawnConut,
    //            EnemysAttackObject.GetComponent<Weapon>());
    //        _objectController.playerWaponDataList[i].key = CharacterType.Enemy;
    //        _waponSpawnConut++;
    //        EnemysAttackObject.SetActive(false);
    //    }

    //    for (int i = 0; i < maxSize; i++)
    //    {
    //        GameObject PlayerAttackObject = Resources.Load<GameObject>(Prefab_PlayerWapon);
    //        GameObject PlayersAttackObject = Instantiate(PlayerAttackObject, transform);

    //        _objectController.WaponDataList.Add(_waponSpawnConut,
    //            PlayersAttackObject.GetComponent<Weapon>());
    //        _objectController.WaponDataList[i].key = CharacterType.Player;
    //        _waponSpawnConut++;
    //        PlayersAttackObject.SetActive(false);
    //    }
    //}
}