using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SpawnController : MonoBehaviour
{
    private void Start()
    {
        SpawnMyPlayer();
    }

    public void SpawnMyPlayer()
    {
        var playerSpawn = GameTree.Instance.objectPool.GetPool("102");
        playerSpawn.transform.position = this.transform.position;
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