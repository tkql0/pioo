using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public int maxSize;
    public int maxPlayers;

    private int _playerSpawnConut;
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
        //maxPlayers = GameTree.UI.player;
        maxPlayers = 1;

        _fishSpawnConut = 0;
        _enemySpawnConut = 0;
        _mapSpawnConut = 0;
        _playerSpawnConut = 0;
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

        for (int i = 0; i < GameTree.GAME.objectController.playerDataList.Count; i++)
        {
            Vector3 targetPosition = GameTree.GAME.objectController.
                playerDataList[i].player.transform.position;

            SpawnMap(targetPosition);
            SpawnPlayerAttackPool();
            SpawnEnemyAttackPool();
        }

        for (int i = 0; i < GameTree.GAME.mapController.mapList.Count; i++)
        {
            SpawnMonsterPool();
            SpawnFishPool();

            GameTree.GAME.mapController.mapList[i].key = i;
        }
    }

    private void SpawnMyPlayer()
    {
        maxSize = maxPlayers;

        for (int i = 0; i < maxSize; i++)
        {
            GameObject playersObject = Resources.Load<GameObject>(Prefab_Player);
            GameObject playersObjects =  Instantiate(playersObject,
                GameTree.GAME.spawnController.spawnGroupObject.transform);

            GameTree.GAME.objectController.playerDataList.Add(_playerSpawnConut,
                playersObjects.GetComponent<Player>());
            _playerSpawnConut++;
        }
    }

    private void SpawnMap(Vector3 spawnCenter)
    {
        for (int i = Left_MapSpawn; i < Right_MapSpawn; i++)
        {
            GameObject MapsObject = Resources.Load<GameObject>(Prefab_Map);
            GameObject MapsObjects = Instantiate(MapsObject,
                new Vector3(spawnCenter.x + (i * 20), 0, 0), Quaternion.identity);
            GameTree.GAME.mapController.mapList.Add(_mapSpawnConut, MapsObjects.GetComponent<Map>());
            _mapSpawnConut++;
        }
    }

    private void SpawnMonsterPool()
    {
        maxSize = 15;

        for (int i = 0; i < maxSize; i++)
        {
            GameObject EnemysObject = Resources.Load<GameObject>(Prefab_Enemy);
            GameObject EnemysObjects = Instantiate(EnemysObject,
                GameTree.GAME.spawnController.spawnGroupObject.transform);

            GameTree.GAME.objectController.enemyDataList.Add(_enemySpawnConut,
                EnemysObjects.GetComponent<EnemyCharater>());
            _enemySpawnConut++;
            EnemysObjects.SetActive(false);
        }
    }

    private void SpawnFishPool()
    {
        maxSize = 45;

        for (int i = 0; i < maxSize; i++)
        {
            GameObject FishsObject = Resources.Load<GameObject>(Prefab_Fish);
            GameObject FishsObjects = Instantiate(FishsObject,
                GameTree.GAME.spawnController.spawnGroupObject.transform);

            GameTree.GAME.objectController.fishDataList.Add(_fishSpawnConut,
                FishsObjects.GetComponent<FishCharacter>());
            _fishSpawnConut++;
            FishsObjects.SetActive(false);
        }
    }

    private void SpawnPlayerAttackPool()
    {
        maxSize = 40;

        for (int i = 0; i < maxSize; i++)
        {
            GameObject PlayerAttackObject = Resources.Load<GameObject>(Prefab_PlayerWapon);
            GameObject PlayersAttackObject = Instantiate(PlayerAttackObject,
                GameTree.GAME.spawnController.spawnGroupObject.transform);

            GameTree.GAME.objectController.playerWaponDataList.Add(_playerWaponSpawnConut,
                PlayersAttackObject.GetComponent<Wapon>());
            _playerWaponSpawnConut++;
            PlayersAttackObject.SetActive(false);
        }
    }

    private void SpawnEnemyAttackPool()
    {
        maxSize = 40;

        for (int i = 0; i < maxSize; i++)
        {
            GameObject EnemyAttackObject = Resources.Load<GameObject>(Prefab_EnemyWapon);
            GameObject EnemysAttackObject = Instantiate(EnemyAttackObject,
                GameTree.GAME.spawnController.spawnGroupObject.transform);

            GameTree.GAME.objectController.enemyWaponDataList.Add(_enemyWaponSpawnConut,
                EnemysAttackObject.GetComponent<Wapon>());
            _enemyWaponSpawnConut++;
            EnemysAttackObject.SetActive(false);
        }
    }
}