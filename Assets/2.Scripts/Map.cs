using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public GameObject map;

    public int key;

    private const float reSpawnTime =  20f;

    public int enemyMaxSize;
    public int fishMaxSize;

    private void Update()
    {
        MapRelocation();
    }

    public void Init()
    {
        
    }

    public void OnEnable()
    {
        map = gameObject;

        enemyMaxSize = Random.Range(1, 6);
        fishMaxSize = Random.Range(1, 15);
    }

    public void OnDisable()
    {

    }

    private void MapRelocation()
    {
        Vector3 targetPosition;
        Vector3 myPosition;

        targetPosition = GameTree.GAME.objectController.player.transform.position;
        myPosition = transform.position;

        float DistanceX = targetPosition.x - myPosition.x;
        float differenceX = Mathf.Abs(DistanceX);

        DistanceX = DistanceX > 0 ? 1 : -1;

        if (differenceX > 30.0f)
        {
            GameTree.GAME.spawnController.DeSpawn(targetPosition);

            transform.Translate(Vector3.right * DistanceX * 60);

            enemyMaxSize = Random.Range(1, 6);
            fishMaxSize = Random.Range(1, 15);

            MapMonsterSpawn(enemyMaxSize, fishMaxSize);
        }
    }

    public void MapMonsterSpawn(int enemySize, int fishSize)
    {
        GameTree.GAME.spawnController.Spawn(gameObject, enemySize, fishSize, key);

        StartCoroutine(ReSpawn(reSpawnTime, enemySize, fishSize));
    }

    private IEnumerator ReSpawn(float ReSpawnTime, int enemySize, int fishSize)
    {
        yield return new WaitForSeconds(ReSpawnTime);

        GameTree.GAME.spawnController.Spawn(gameObject, ReSpawnSize(enemySize,
            fishSize).Item1, ReSpawnSize(enemySize, fishSize).Item2, key);

        StartCoroutine(ReSpawn(ReSpawnTime, enemySize, fishSize));
    }

    private (int, int) ReSpawnSize(int enemySize, int fishSize)
    {
        int enemyReSpawnSize = 0;
        int fishReSpawnSize = 0;

        for (int i = 0; i < GameTree.GAME.objectController.enemyDataList.Count; i++)
        {
            if (key == GameTree.GAME.objectController.enemyDataList[i].key
                && GameTree.GAME.objectController.enemyDataList[i].Enemy.activeSelf)
            {
                enemyReSpawnSize++;
            }
        }
        for (int i = 0; i < GameTree.GAME.objectController.fishDataList.Count; i++)
        {
            if (key == GameTree.GAME.objectController.fishDataList[i].key
                && GameTree.GAME.objectController.fishDataList[i].Fish.activeSelf)
            {
                fishReSpawnSize++;
            }
        }

        int enemyReSpawnMaxSize = enemySize - enemyReSpawnSize;
        int fishReSpawnMaxSize = fishSize - fishReSpawnSize;

        return (enemyReSpawnMaxSize, fishReSpawnMaxSize);
    }
}
