using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public int maxSize                  = 0;

    private int playerSpawnConut        = 0;
    private int mapSpawnConut           = 0;
    private long enemySpawnConut        = 0;
    private long fishSpawnConut         = 0;

    private int maxPlayers              = 1;

    private const int POSITION_X_MIN    = -10;
    private const int POSITION_X_MAX    = 11;

    private const int POSITION_Y_MIN    = -3;
    private const int POSITION_Y_MAX    = -20;

    private const int LeftMapSpawn      = -1;
    private const int RightMapSpawn     = 2;

    private const string prefabPlayer   = "Prefabs/Player";
    private const string prefabMap      = "Prefabs/Map";
    private const string prefabEnemy    = "Prefabs/Enemy";
    private const string prefabFish     = "Prefabs/Fish";

    private ObjectController objectController = GameTree.GAME.objectController;

    private GameObject spawnGroupObject = GameTree.GAME.spawnController.spawnGroupObject;

    public void OnEnable()
    {
        
    }

    public void OnDisable()
    {

    }

    public void Init()
    {
        //maxPlayers = 1;

        // 시작시 오브젝트 생성
        SpawnMyPlayer();

        for (int i = 0; i < objectController.playerList.Count; i++)
        {
            Vector3 targetPosition = objectController.playerList[i].transform.position;

            SpawnMap(targetPosition);
            SpawnMonster(targetPosition);
            SpawnExpFish(targetPosition);
        }

        for (int i = 0; i < objectController.mapList.Count; i++)
        {
            Vector3 targetPosition = objectController.mapList[i].transform.position;

            SpawnMonster(targetPosition);
            SpawnExpFish(targetPosition);
        }
    }

    private void SpawnMyPlayer()
    {
        maxSize = maxPlayers;

        for (int i = 0; i < maxSize; i++)
        {
            GameObject playersObject = Resources.Load<GameObject>(prefabPlayer);
            GameObject playersObjects =  Instantiate(playersObject, spawnGroupObject.transform);
            playersObjects.GetComponent<MyCharater>().key = playerSpawnConut;

            objectController.playerList.Add(playerSpawnConut, playersObjects.GetComponent<MyCharater>());
            playerSpawnConut++;
        }
    }

    private void SpawnMap(Vector3 spawnCenter)
    {
        for (int i = 0; i < objectController.playerList.Count; i++)
        {
            for (int j = LeftMapSpawn; j < RightMapSpawn; j++)
            {
                GameObject MapsObject = Resources.Load<GameObject>(prefabMap);
                GameObject MapsObjects = Instantiate(MapsObject,
                    new Vector3(spawnCenter.x + (j * 20), 0, 0), Quaternion.identity);
                objectController.mapList.Add(mapSpawnConut, MapsObjects.GetComponent<Map>());
                mapSpawnConut++;
            }
        }
    }

    public void SpawnMonster(Vector3 spawnCenter)
    {
        maxSize = Random.Range(1, 5);

        for (int i = 0; i < maxSize; i++)
        {
            int randomPositionX = Random.Range(POSITION_X_MIN, POSITION_X_MAX);

            GameObject EnemysObject = Resources.Load<GameObject>(prefabEnemy);
            GameObject EnemysObjects = Instantiate(EnemysObject,
                new Vector3(spawnCenter.x + randomPositionX, 0, 0), Quaternion.identity, spawnGroupObject.transform);

            objectController.enemyList.Add(enemySpawnConut, EnemysObjects.GetComponent<EnemyCharater>());
            enemySpawnConut++;
        }
    }

    public void SpawnExpFish(Vector3 spawnCenter)
    {
        maxSize = Random.Range(1, 15);

        for (int i = 0; i < maxSize; i++)
        {
            int randomPositionX = Random.Range(POSITION_X_MIN, POSITION_X_MAX);
            int randomPositionY = Random.Range(POSITION_Y_MAX, POSITION_Y_MIN);

            GameObject FishsObject = Resources.Load<GameObject>(prefabFish);
            GameObject FishsObjects = Instantiate(FishsObject,
                new Vector3(spawnCenter.x + randomPositionX, spawnCenter.y + randomPositionY, 0), Quaternion.identity, spawnGroupObject.transform);

            objectController.fishList.Add(fishSpawnConut, FishsObjects.GetComponent<FishCharacter>());
            fishSpawnConut++;
        }
    }

    void ReSpawn()
    {
        // 오브젝트가 플레이어에 의해 사라졌을 때 작동할 함수
    }

    IEnumerator reSpawnTime(float Time)
    {
        var wfs = new WaitForSeconds(Time);


        yield return wfs;

    }
}