using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KeyType = System.String;


public class SpawnObject : MonoBehaviour
{
    [System.Serializable]
    public class SpawnData
    {
        public GameObject objectBase;
        public KeyType key;
    }

    [SerializeField]
    List<SpawnData> objecSpawntList = new List<SpawnData>(4);

    Dictionary<KeyType, GameObject> ObjectdataDic = new Dictionary<KeyType, GameObject>(); // 정보
    Dictionary<KeyType, Stack<GameObject>> cloneObjectDic = new Dictionary<KeyType, Stack<GameObject>>(); // 넣고 뺄 오브젝트

    int maxSize = 0;
    int playerSpawnConut = 0;
    int enemySpawnConut = 0;
    int fishSpawnConut = 0;

    private string key;

    public void Start()
    {
        if(GameTree.GAME.spawnController.gmaeStrat)
        {
            Init();
        }
    }

    public void OnDisable()
    {

    }

    public void Init()
    {
        int len = objecSpawntList.Count;

        if (len == 0)
            return;

        for (int i = 0; i < objecSpawntList.Count; i++)
        {
            ObjectdataDic.Add(objecSpawntList[i].key, objecSpawntList[i].objectBase);
        }

        SpawnMyPlayer();
        SpawnExpFish();
        SpawnMonster();
    }

    public void SpawnMyPlayer()
    {
        //playerSpawnConut++;
        maxSize = Random.Range(1, 5);
        key = "101";

        //objectConut = GameTree.GAME.objectController.playerList.Count;

        for (int i = 0; i < maxSize; i++)
        {
            GameObject objectData = Instantiate(ObjectdataDic[key]);

            //GameTree.GAME.objectController.playerList.Add(playerSpawnConut, objectData);
        }
    }

    void SpawnMonster()
    {
        //enemySpawnConut++;
        maxSize = Random.Range(1, 21);
        key = "102";

        //objectConut = GameTree.GAME.objectController.playerList.Count;

        for (int i = 0; i < maxSize; i++)
        {
            GameObject objectData = Instantiate(ObjectdataDic[key]);

            //GameTree.GAME.objectController.enemyList.Add(enemySpawnConut, objectData);
        }
    }

    void SpawnExpFish()
    {
        //fishSpawnConut++;
        maxSize = Random.Range(1, 21);
        key = "103";

        //objectConut = GameTree.GAME.objectController.playerList.Count;

        for (int i = 0; i < maxSize; i++)
        {
            GameObject objectData = Instantiate(ObjectdataDic[key]);

            //GameTree.GAME.objectController.FishList.Add(fishSpawnConut, objectData);
        }
    }
}