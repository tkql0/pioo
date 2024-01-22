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

    public Dictionary<KeyType, GameObject> ObjectdataDic = new Dictionary<KeyType, GameObject>(); // 오브젝트 정보
    //public Dictionary<KeyType, Stack<GameObject>> cloneObjectDic = new Dictionary<KeyType, Stack<GameObject>>(); // 넣고 뺄 오브젝트

    int maxSize = 0;
    int playerSpawnConut = 0;
    long enemySpawnConut = 0;
    long fishSpawnConut = 0;

    private string key;

    public void Start()
    {
        if(GameTree.GAME.gmaeStrat)
        {
            // 게임이 시작 될때 생성이 아니라 맵이 생성되거나 이동 됬을 때 생성으로?
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
        key = "101";
        maxSize = GameTree.GAME.objectController.playersCount;
        //maxSize = 5;

        //objectConut = GameTree.GAME.objectController.playerList.Count;

        for (int i = 0; i < maxSize; i++)
        {
            GameObject objectData = Instantiate(ObjectdataDic[key], this.transform);
            //GameTree.GAME.objectController.myCharater = objectData;
            objectData.GetComponent<MyCharater>().key = playerSpawnConut;

            GameTree.GAME.objectController.playerList.Add(playerSpawnConut, objectData);
            playerSpawnConut++;
        }
    }

    void SpawnMonster()
    {
        maxSize = Random.Range(1, 21);
        key = "102";

        //objectConut = GameTree.GAME.objectController.playerList.Count;

        for (int i = 0; i < maxSize; i++)
        {
            GameObject objectData = Instantiate(ObjectdataDic[key], this.transform);

            GameTree.GAME.objectController.enemyList.Add(enemySpawnConut, objectData);
            enemySpawnConut++;
        }
    }

    void SpawnExpFish()
    {
        maxSize = Random.Range(1, 21);
        key = "103";

        //objectConut = GameTree.GAME.objectController.playerList.Count;

        for (int i = 0; i < maxSize; i++)
        {
            GameObject objectData = Instantiate(ObjectdataDic[key], this.transform);

            GameTree.GAME.objectController.FishList.Add(fishSpawnConut, objectData);
            fishSpawnConut++;
        }
    }

    void ReSpawn()
    {
        // 오브젝트가 플레이어에 의해 사라졌을 때 작동할 함수
    }
}