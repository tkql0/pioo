using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    private ObjectController _objectController;
    private SpawnController _spawnController;

    public int enemyMaxSize;
    public int fishMaxSize;

    public int key;

    private void Update()
    {
        //if (!Input.anyKey)
        //    return;
        mapRelocation();
    }

    public void Init()
    {
        enemyMaxSize = 0;
        fishMaxSize = 0;
        key = 0;

        _objectController = GameTree.GAME.objectController;
        _spawnController = GameTree.GAME.spawnController;

        MapMonsterSpawn();
    }

    public void OnEnable()
    {
        //MapMonsterSpawn();
    }

    public void OnDisable()
    {


    }

    void MapMonsterSpawn()
    {
        enemyMaxSize = Random.Range(1, 6);
        fishMaxSize = Random.Range(1, 15);

        _spawnController.Spawn(gameObject, enemyMaxSize, fishMaxSize, key);
    }

    public void mapRelocation()
    {
        Vector3 targetPosition;
        Vector3 myPosition;

        targetPosition = _objectController.playerList[key].transform.position;
        myPosition = transform.position;

        float DistanceX = targetPosition.x - myPosition.x;
        float differenceX = Mathf.Abs(DistanceX);

        DistanceX = DistanceX > 0 ? 1 : -1;

        if (differenceX > 30.0f)
        {
            _spawnController.DeSpawn(key, targetPosition);

            transform.Translate(Vector3.right * DistanceX * 60);
            MapMonsterSpawn();
        }
    }
}
