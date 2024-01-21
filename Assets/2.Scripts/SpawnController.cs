using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KeyType = System.String;

public class SpawnController
{
    public GameObject objectGroup = new GameObject("objectGroup");
    public Spawn spawn = new Spawn();

    public GameObject objectBase;
    public KeyType key;

    private void OnEnable()
    {
        spawn.Init();
    }

    public void OnDisable()
    {


    }


}

public class Spawn : MonoBehaviour
{
    public List<SpawnController> objecSpawntList = new List<SpawnController>(4);
    Dictionary<KeyType, GameObject> cloneObjectDic = new Dictionary<KeyType, GameObject>();

    public int maxSize = 0;
    public int objectConut = 0;

    private string key;

    public void Init()
    {
        int len = objecSpawntList.Count;

        if (len == 0)
            return;

        for (int i = 0; i < objecSpawntList.Count; i++)
        {
            cloneObjectDic.Add(objecSpawntList[i].key, objecSpawntList[i].objectBase);
        }
    }

    public void SpawnMyPlayer()
    {
        maxSize = Random.Range(1, 5);
        key = "101";

        objectConut = GameTree.GAME.objectController.playerList.Count;

        for (int i = 0; i < maxSize; i++)
        {
            GameObject objectData = Instantiate(cloneObjectDic[key], GameTree.GAME.spawnController.objectGroup.transform);

            GameTree.GAME.objectController.playerList.Add(objectConut, objectData);
        }
    }

    void SpawnExpFish()
    {
        int randomPosX = Random.Range(-69, 70);
        int randomPosY = Random.Range(-25, -5);


    }

    void SpawnMonster()
    {
        int randomPosX = Random.Range(-20, 20);


    }
}