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

    private const int Left_MapSpawn = -1;
    private const int Right_MapSpawn = 2;

    private const string Prefab_Player = "Prefabs/Player";
    private const string Prefab_Map = "Prefabs/Map";
    private const string Prefab_Enemy = "Prefabs/Enemy";
    private const string Prefab_Fish = "Prefabs/Fish";

    private ObjectController _objectController;
    private MapController _mapController;
    private SpawnController _spawnController;

    public void Init()
    {
        maxSize = 0;
        maxPlayers = 1;

        _fishSpawnConut = 0;
        _enemySpawnConut = 0;
        _mapSpawnConut = 0;
        _playerSpawnConut = 0;

        _spawnController = GameTree.GAME.spawnController;
        _objectController = GameTree.GAME.objectController;
        _mapController = GameTree.GAME.mapController;

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

        for (int i = 0; i < _objectController.playerList.Count; i++)
        {
            Vector3 targetPosition = _objectController.playerList[i].transform.position;

            SpawnMap(targetPosition);
        }

        for (int i = 0; i < _mapController.mapList.Count; i++)
        {
            SpawnMonsterPool();
            SpawnFishPool();

            _mapController.mapList[i].key = i;
        }
    }

    private void SpawnMyPlayer()
    {
        maxSize = maxPlayers;

        for (int i = 0; i < maxSize; i++)
        {
            GameObject playersObject = Resources.Load<GameObject>(Prefab_Player);
            GameObject playersObjects =  Instantiate(playersObject, _spawnController.spawnGroupObject.transform);

            _objectController.playerDataList.Add(_playerSpawnConut, playersObjects.GetComponent<Player>());
            _objectController.playerList.Add(_playerSpawnConut, playersObjects);
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
            _mapController.mapList.Add(_mapSpawnConut, MapsObjects.GetComponent<Map>());
            //_mapController.mapList.Add(_mapSpawnConut, MapsObjects);
            _mapSpawnConut++;
        }
    }

    private void SpawnMonsterPool()
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

    private void SpawnFishPool()
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

public class WaponData : MonoBehaviour
{
    //Rigidbody2D rigid;

    //void Awake()
    //{
    //    rigid = GetComponent<Rigidbody2D>();
    //}

    //void Update()
    //{
    //    //if (!photonView.IsMine)
    //    //    return;

    //    transform.right = GetComponent<Rigidbody2D>().velocity;
    //    lance_gravity();
    //}

    //void lance_gravity()
    //{
    //    Vector3 myPos = transform.position;
    //    Vector3 SeaLevelPos = GameManager.Instance.SeaLevel.transform.position;

    //    float DirY = myPos.y - SeaLevelPos.y;
    //    rigid.drag = DirY <= 0 ? 3 : 1;
    //    // 몬스터의 공격이 해수면보다 아래인가? 맞다면 가속 1로 변경
    //}
}