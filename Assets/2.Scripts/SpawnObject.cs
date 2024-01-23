using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    int maxSize = 0;
    int playerSpawnConut = 0;
    long enemySpawnConut = 0;
    long fishSpawnConut = 0;
    int mapSpawnConut = 0;

    ObjectController objectController = GameTree.GAME.objectController;

    GameObject spawnGroupObject = GameTree.GAME.spawnController.spawnGroupObject;

    public void OnEnable()
    {
        
    }

    public void OnDisable()
    {

    }

    public void Init()
    {
        // 시작시 오브젝트 생성
        SpawnMyPlayer();
        SpawnMonster();
        SpawnExpFish();
        SpawnMap();
    }

    void SpawnMyPlayer()
    {
        maxSize = 1;

        for (int i = 0; i < maxSize; i++)
        {
            GameObject playersObject = Resources.Load<GameObject>("Prefabs/Player");
            GameObject playersObjects =  Instantiate(playersObject, spawnGroupObject.transform);
            playersObjects.GetComponent<MyCharater>().key = playerSpawnConut;

            objectController.playerList.Add(playerSpawnConut, playersObjects);
            playerSpawnConut++;

            //for (int j = -1; j < 2; j++)
            //{
            //    GameObject MapsObject = Resources.Load<GameObject>("Prefabs/Map");
            //    GameObject MapsObjects = Instantiate(MapsObject, new Vector3(playersObjects.transform.position.x + (j * 20), 0, 0), Quaternion.identity);
            //    objectController.mapLish.Add(mapSpawnConut, MapsObjects);
            //    mapSpawnConut++;
            //}
        }
    }

    void SpawnMonster()
    {
        int randomPositionX = Random.Range(-20, 20);

        maxSize = Random.Range(1, 21);

        for (int i = 0; i < maxSize; i++)
        {
            GameObject EnemysObject = Resources.Load<GameObject>("Prefabs/Enemy");
            GameObject EnemysObjects = Instantiate(EnemysObject, spawnGroupObject.transform);

            objectController.enemyList.Add(enemySpawnConut, EnemysObjects);
            enemySpawnConut++;
        }
    }

    void SpawnExpFish()
    {
        int randomPositionX = Random.Range(-69, 70);
        int randomPositionY = Random.Range(-25, -5);

        maxSize = Random.Range(1, 21);

        for (int i = 0; i < maxSize; i++)
        {
            GameObject FishsObject = Resources.Load<GameObject>("Prefabs/Fish");
            GameObject FishsObjects = Instantiate(FishsObject, spawnGroupObject.transform);

            objectController.fishList.Add(fishSpawnConut, FishsObjects);
            fishSpawnConut++;
        }
    }

    void SpawnMap()
    {
        for (int i = 0; i < objectController.playerList.Count; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                GameObject MapsObject = Resources.Load<GameObject>("Prefabs/Map");
                GameObject MapsObjects = Instantiate(MapsObject,
                    new Vector3(objectController.playerList[i].transform.position.x + (j * 20), 0, 0), Quaternion.identity);
                objectController.mapLish.Add(mapSpawnConut, MapsObjects);
                mapSpawnConut++;
            }
        }
    }

    void ReSpawn()
    {
        // 오브젝트가 플레이어에 의해 사라졌을 때 작동할 함수
    }

    IEnumerator reSpawnTime()
    {
        
        yield return new WaitForSeconds(30.0f);

    }
}