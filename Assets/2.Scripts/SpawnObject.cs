using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public int maxSize;
    public int maxPlayers;

    //public int enemyMaxSize;

    private int _playerSpawnConut;
    private int _mapSpawnConut;
    private long _enemySpawnConut;
    private long _fishSpawnConut;

    private const int Left_MapSpawn = -1;
    private const int Right_MapSpawn = 2;

    private const string Prefab_Player = "Prefabs/Player";
    private const string Prefab_Map = "Prefabs/Map";
    private const string Prefab_Enemy = "Prefabs/Enemy";
    private const string Prefab_Fish = "Prefabs/Fish";

    private ObjectController _objectController;
    private MapController _mapController;
    SpawnController _spawnController;

    //private GameObject _spawnGroupObject;


    public void OnEnable()
    {
        
    }

    public void OnDisable()
    {

    }

    public void Init()
    {
        maxSize = 0;
        maxPlayers = 1;
        //enemyMaxSize = 5;

        _fishSpawnConut = 0;
        _enemySpawnConut = 0;
        _mapSpawnConut = 0;
        _playerSpawnConut = 0;

        _spawnController = GameTree.GAME.spawnController;
        _objectController = GameTree.GAME.objectController;
        _mapController = GameTree.GAME.mapController;

        SpawnMyPlayer();

        for (int i = 0; i < _objectController.playerList.Count; i++)
        {
            Vector3 targetPosition = _objectController.playerList[i].transform.position;

            SpawnMap(targetPosition);
        }

        for (int i = 0; i < _mapController.mapList.Count; i++)
        {
            SpawnMonsterPool();
            SpawnExpFishPool();

            _mapController.mapList[i].key = i / 3;
        }
    }

    public void SpawnMyPlayer()
    {
        maxSize = maxPlayers;

        for (int i = 0; i < maxSize; i++)
        {
            GameObject playersObject = Resources.Load<GameObject>(Prefab_Player);
            GameObject playersObjects =  Instantiate(playersObject, _spawnController.spawnGroupObject.transform);

            //_objectController.playerList.Add(_playerSpawnConut, playersObjects.GetComponent<MyCharater>());
            _objectController.playerList.Add(_playerSpawnConut, playersObjects);
            _playerSpawnConut++;
        }
    }

    public void SpawnMap(Vector3 spawnCenter)
    {
        for (int i = Left_MapSpawn; i < Right_MapSpawn; i++)
        {
            GameObject MapsObject = Resources.Load<GameObject>(Prefab_Map);
            GameObject MapsObjects = Instantiate(MapsObject,
                new Vector3(spawnCenter.x + (i * 20), 0, 0), Quaternion.identity);
            _mapController.mapList.Add(_mapSpawnConut, MapsObjects.GetComponent<Map>());
            //_mapController.mapList.Add(_mapSpawnConut, MapsObjects);
            _mapSpawnConut++;
        }
    }

    public void SpawnMonsterPool()
    {
        maxSize = 15;

        for (int i = 0; i < maxSize; i++)
        {
            GameObject EnemysObject = Resources.Load<GameObject>(Prefab_Enemy);
            GameObject EnemysObjects = Instantiate(EnemysObject, _spawnController.spawnGroupObject.transform);

            _objectController.enemyDataList.Add(_enemySpawnConut, EnemysObjects.GetComponent<EnemyCharater>());
            _objectController.enemyList.Add(_enemySpawnConut, EnemysObjects);
            _enemySpawnConut++;
            EnemysObjects.SetActive(false);
        }
    }

    public void SpawnExpFishPool()
    {
        maxSize = 45;

        for (int i = 0; i < maxSize; i++)
        {
            GameObject FishsObject = Resources.Load<GameObject>(Prefab_Fish);
            GameObject FishsObjects = Instantiate(FishsObject, _spawnController.spawnGroupObject.transform);

            _objectController.fishDataList.Add(_fishSpawnConut, FishsObjects.GetComponent<FishCharacter>());
            _objectController.fishList.Add(_fishSpawnConut, FishsObjects);
            _fishSpawnConut++;
            FishsObjects.SetActive(false);
        }
    }
}