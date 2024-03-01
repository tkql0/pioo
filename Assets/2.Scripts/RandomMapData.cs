using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMapData : MonoBehaviour
{
    public long mySpawnNumber;

    //private float ReSpawn_Time = 10f;

    public void MapMonsterSpawn(long InEnemySize, long InFishSize)
    {
        GameManager.SPAWN.Spawn(MapPosition, InEnemySize, InFishSize, mySpawnNumber);

        //Invoke("ReSpawn", ReSpawn_Time);
    }

    private void ReSpawn()
    {
        long enemyReSpawnSize = GameManager.Map.GetReSpawnSize(mySpawnNumber).Item1;
        long fishReSpawnSize = GameManager.Map.GetReSpawnSize(mySpawnNumber).Item2;

        GameManager.SPAWN.Spawn(MapPosition, enemyReSpawnSize, fishReSpawnSize, mySpawnNumber);
    }

    //public bool isActive => mapObject.activeSelf;

    public Vector2 MapPosition;
}
