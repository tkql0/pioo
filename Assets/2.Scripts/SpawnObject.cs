using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public int maxSize;
    public int maxPlayers;

    public int enemyMaxSize;

    private int _playerSpawnConut;
    private int _mapSpawnConut;
    private long _enemySpawnConut;
    private long _fishSpawnConut;

    private const int Position_X_Min = -10;
    private const int Position_X_Max = 11;

    private const int Position_Y_Min = -3;
    private const int Position_Y_Max = -20;

    private const int Left_MapSpawn = -1;
    private const int Right_MapSpawn = 2;

    private const string Prefab_Player = "Prefabs/Player";
    private const string Prefab_Map = "Prefabs/Map";
    private const string Prefab_Enemy = "Prefabs/Enemy";
    private const string Prefab_Fish = "Prefabs/Fish";

    private ObjectController _objectController;
    private MapController _mapController;

    private GameObject _spawnGroupObject;


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
        enemyMaxSize = 5;

        _fishSpawnConut = 0;
        _enemySpawnConut = 0;
        _mapSpawnConut = 0;
        _playerSpawnConut = 0;

        _spawnGroupObject = GameTree.GAME.spawnController.spawnGroupObject;
        _objectController = GameTree.GAME.objectController;
        _mapController = GameTree.MAP.mapController;

        // 시작시 오브젝트 생성
        SpawnMyPlayer();

        for (int i = 0; i < _objectController.playerList.Count; i++)
        {
            Vector3 targetPosition = _objectController.playerList[i].transform.position;

            //SpawnMonster(targetPosition);
            //SpawnExpFish(targetPosition);
            SpawnMap(targetPosition);
        }

        for (int i = 0; i < _objectController.mapList.Count; i++)
        {
            //Vector3 targetPosition = _objectController.mapList[i].transform.position;

            SpawnMonsterPool();
            SpawnExpFishPool();


            Spawn(_objectController.mapList[i].gameObject);
        }
    }

    private void SpawnMyPlayer()
    {
        maxSize = maxPlayers;

        for (int i = 0; i < maxSize; i++)
        {
            GameObject playersObject = Resources.Load<GameObject>(Prefab_Player);
            GameObject playersObjects =  Instantiate(playersObject, _spawnGroupObject.transform);
            //playersObjects.GetComponent<MyCharater>().key = _playerSpawnConut;

            _objectController.playerList.Add(_playerSpawnConut, playersObjects.GetComponent<MyCharater>());
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
            _objectController.mapList.Add(_mapSpawnConut, MapsObjects.GetComponent<Map>());
            _mapSpawnConut++;
        }
    }

    public void SpawnMonsterPool()
    {
        //maxSize = Random.Range(1, 5);

        for (int i = 0; i < enemyMaxSize; i++)
        {
            //int randomPositionX = Random.Range(Position_X_Min, Position_X_Max);

            GameObject EnemysObject = Resources.Load<GameObject>(Prefab_Enemy);
            GameObject EnemysObjects = Instantiate(EnemysObject, _spawnGroupObject.transform);

            //_objectController.enemyList.Add(_enemySpawnConut, EnemysObjects.GetComponent<EnemyCharater>());
            _objectController.enemyList.Add(_enemySpawnConut, EnemysObjects);
            _enemySpawnConut++;
            EnemysObjects.SetActive(false);
        }
    }

    public void SpawnExpFishPool()
    {
        //maxSize = Random.Range(1, 15);
        maxSize = 15;

        for (int i = 0; i < maxSize; i++)
        {
            //int randomPositionX = Random.Range(Position_X_Min, Position_X_Max);
            //int randomPositionY = Random.Range(Position_Y_Max, Position_Y_Min);

            GameObject FishsObject = Resources.Load<GameObject>(Prefab_Fish);
            GameObject FishsObjects = Instantiate(FishsObject, _spawnGroupObject.transform);

            //_objectController.fishList.Add(_fishSpawnConut, FishsObjects.GetComponent<FishCharacter>());
            _objectController.fishList.Add(_fishSpawnConut, FishsObjects);
            _fishSpawnConut++;
            FishsObjects.SetActive(false);
        }
    }

    public void Spawn(GameObject spawnCenter)
    {
        int enemyCount = Random.Range(1, 6);
        int fishCount = Random.Range(1, 15);

        //_mapController.enemyMaxSize = enemyCount;

        for (int i = 0; i < enemyCount; i++)
        {
            SpawnEnemy(spawnCenter);
        }

        for (int i = 0; i < fishCount; i++)
        {
            SpawnFish(spawnCenter);
        }
    }

    GameObject SpawnEnemy(GameObject spawnObject)
    {
        int randomPositionX = Random.Range(Position_X_Min, Position_X_Max);

        for (int i = 0; i < _objectController.enemyList.Count; i++)
        {
            if (!_objectController.enemyList[i].activeSelf)
            {
                _objectController.enemyList[i].SetActive(true);

                _objectController.enemyList[i].transform.position
                    = new Vector3(randomPositionX + spawnObject.transform.position.x, 0, 0);

                return _objectController.enemyList[i];
            }
        }
        return null;
    }

    GameObject SpawnFish(GameObject spawnObject)
    {
        int randomPositionX = Random.Range(Position_X_Min, Position_X_Max);
        int randomPositionY = Random.Range(Position_Y_Min, Position_Y_Max);

        for (int i = 0; i < _objectController.fishList.Count; i++)
        {
            if (!_objectController.fishList[i].activeSelf)
            {
                _objectController.fishList[i].SetActive(true);

                _objectController.fishList[i].transform.position
                    = new Vector3(randomPositionX + spawnObject.transform.position.x, randomPositionY, 0);

                return _objectController.fishList[i];
            }
        }
        return null;
    }

    IEnumerator ReSpawn(float Time)
    {
        
        int randomPositionX = Random.Range(Position_X_Min, Position_X_Max);

        var wfs = new WaitForSeconds(Time);


        yield return wfs;

    }
}