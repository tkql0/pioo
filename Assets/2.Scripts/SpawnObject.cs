using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    int maxSize = 0;
    int playerSpawnConut = 0;
    long enemySpawnConut = 0;
    long fishSpawnConut = 0;

    private string key;

    public void OnEnable()
    {
        //if(GameTree.GAME.gameStrat)
        //{
        //     //������ ���� �ɶ� ������ �ƴ϶� ���� �����ǰų� �̵� ���� �� ��������?
        //    Init();
        //}
    }

    public void OnDisable()
    {

    }

    public void Init()
    {
        SpawnMyPlayer();
        SpawnMonster();
        SpawnExpFish();
    }

    void SpawnMyPlayer()
    {
        maxSize = GameTree.GAME.objectController.playersCount;

        for (int i = 0; i < maxSize; i++)
        {
            GameObject playersObject = Resources.Load<GameObject>("Prefabs/Player");
            Instantiate(playersObject);
            playersObject.GetComponent<MyCharater>().key = playerSpawnConut;

            GameTree.GAME.objectController.playerList.Add(playerSpawnConut, playersObject);
            playerSpawnConut++;
        }
    }

    void SpawnMonster()
    {
        maxSize = Random.Range(1, 21);

        for (int i = 0; i < maxSize; i++)
        {
            GameObject EnemysObject = Resources.Load<GameObject>("Prefabs/Enemy");
            Instantiate(EnemysObject);

            GameTree.GAME.objectController.enemyList.Add(enemySpawnConut, EnemysObject);
            enemySpawnConut++;
        }
    }

    void SpawnExpFish()
    {
        maxSize = Random.Range(1, 21);

        for (int i = 0; i < maxSize; i++)
        {
            GameObject FishsObject = Resources.Load<GameObject>("Prefabs/Fish");
            Instantiate(FishsObject);

            GameTree.GAME.objectController.FishList.Add(fishSpawnConut, FishsObject);
            fishSpawnConut++;
        }
    }

    void ReSpawn()
    {
        // ������Ʈ�� �÷��̾ ���� ������� �� �۵��� �Լ�
    }
}