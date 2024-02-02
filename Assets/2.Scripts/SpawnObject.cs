using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public int maxSize;
    private int _mapSpawnConut;
    private long _enemySpawnConut;
    private long _fishSpawnConut;
    private long _playerWaponSpawnConut;
    private long _enemyWaponSpawnConut;

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

        StartSpawn();
    }

    public void OnEnable()
    {
        
    }

    public void OnDisable()
    {

    }

    private void StartSpawn()
    {
        SpawnMyPlayer();

        Vector3 targetPosition = GameManager.OBJECT.player.transform.position;

        SpawnMap(targetPosition);
        SpawnPlayerAttackPool();
        SpawnEnemyAttackPool();

        for (int i = 0; i < GameManager.OBJECT.mapDataList.Count; i++)
        {
            SpawnMonsterPool();
            SpawnFishPool();

            GameManager.OBJECT.mapDataList[i].key = i;
        }
    }

    private void SpawnMyPlayer()
    {
        GameObject playersObject = Resources.Load<GameObject>(Prefab_Player);
        GameObject playersObjects = Instantiate(playersObject,
            GameManager.SPAWN.spawnGroupObject.transform);

        GameManager.OBJECT.player = playersObjects.GetComponent<Player>();
    }

    private void SpawnMap(Vector3 spawnCenter)
    {
        for (int i = Left_MapSpawn; i < Right_MapSpawn; i++)
        {
            GameObject MapsObject = Resources.Load<GameObject>(Prefab_Map);
            GameObject MapsObjects = Instantiate(MapsObject,
                new Vector3(spawnCenter.x + (i * 40), 0, 0), Quaternion.identity);
            GameManager.OBJECT.mapDataList.Add(_mapSpawnConut, MapsObjects.GetComponent<Map>());
            _mapSpawnConut++;
        }
    }

    private void SpawnMonsterPool()
    {
        maxSize = 20;

        for (int i = 0; i < maxSize; i++)
        {
            GameObject EnemysObject = Resources.Load<GameObject>(Prefab_Enemy);
            GameObject EnemysObjects = Instantiate(EnemysObject,
                GameManager.SPAWN.spawnGroupObject.transform);

            GameManager.OBJECT.enemyDataList.Add(_enemySpawnConut,
                EnemysObjects.GetComponent<EnemyCharater>());
            _enemySpawnConut++;
            EnemysObjects.SetActive(false);
        }
    }

    private void SpawnFishPool()
    {
        maxSize = 50;

        for (int i = 0; i < maxSize; i++)
        {
            GameObject FishsObject = Resources.Load<GameObject>(Prefab_Fish);
            GameObject FishsObjects = Instantiate(FishsObject,
                GameManager.SPAWN.spawnGroupObject.transform);

            GameManager.OBJECT.fishDataList.Add(_fishSpawnConut,
                FishsObjects.GetComponent<FishCharacter>());
            _fishSpawnConut++;
            FishsObjects.SetActive(false);
        }
    }

    private void SpawnPlayerAttackPool()
    {
        maxSize = 20;

        for (int i = 0; i < maxSize; i++)
        {
            GameObject PlayerAttackObject = Resources.Load<GameObject>(Prefab_PlayerWapon);
            GameObject PlayersAttackObject = Instantiate(PlayerAttackObject,
                GameManager.SPAWN.spawnGroupObject.transform);

            GameManager.OBJECT.playerWaponDataList.Add(_playerWaponSpawnConut,
                PlayersAttackObject.GetComponent<Weapon>());
            _playerWaponSpawnConut++;
            PlayersAttackObject.SetActive(false);
        }
    }

    private void SpawnEnemyAttackPool()
    {
        maxSize = 20;

        for (int i = 0; i < maxSize; i++)
        {
            GameObject EnemyAttackObject = Resources.Load<GameObject>(Prefab_EnemyWapon);
            GameObject EnemysAttackObject = Instantiate(EnemyAttackObject,
                GameManager.SPAWN.spawnGroupObject.transform);

            GameManager.OBJECT.enemyWaponDataList.Add(_enemyWaponSpawnConut,
                EnemysAttackObject.GetComponent<Weapon>());
            _enemyWaponSpawnConut++;
            EnemysAttackObject.SetActive(false);
        }
    }
}