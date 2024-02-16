using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public GameObject mapObject;

    public ObjectType key;
    public long mySpawnNumber;

    public long enemyMaxSize;
    public long fishMaxSize;

    private float ReSpawn_Time = 10f;

    private const float DeSpawn_Distance = 60f;

    private void Update()
    {
        MapRelocation();
    }

    public void OnEnable()
    {
        mapObject = gameObject;
        enemyMaxSize = Random.Range(1, 6);
        fishMaxSize = Random.Range(1, 15);
    }

    private void MapRelocation()
    {
        Vector2 targetPosition = GameManager.OBJECT.player.characterPosition;

        float DistanceX = targetPosition.x - MapPosition.x;
        float differenceX = Mathf.Abs(DistanceX);

        DistanceX = DistanceX > 0 ? 1 : -1;

        GameManager.SPAWN.DeSpawn(targetPosition, DeSpawn_Distance + 15);

        if (differenceX > DeSpawn_Distance)
        {
            GameManager.SPAWN.DeSpawn(targetPosition, DeSpawn_Distance - 15);

            transform.Translate(Vector2.right * DistanceX * 120);

            enemyMaxSize = Random.Range(1, 6);
            fishMaxSize = Random.Range(1, 15);

            MapMonsterSpawn(enemyMaxSize, fishMaxSize);
        }
    }

    public void MapMonsterSpawn(long InEnemySize, long InFishSize)
    {
        GameManager.SPAWN.Spawn(MapPosition, InEnemySize, InFishSize, mySpawnNumber);

        StartCoroutine(ReSpawn(ReSpawn_Time));
    }

    private IEnumerator ReSpawn(float InReSpawnTime)
    {
        yield return new WaitForSeconds(InReSpawnTime);

        long enemyReSpawnSize = GameManager.Map.GetReSpawnSize(mySpawnNumber).Item1;
        long fishReSpawnSize = GameManager.Map.GetReSpawnSize(mySpawnNumber).Item2;

        GameManager.SPAWN.Spawn(MapPosition, enemyReSpawnSize, fishReSpawnSize, mySpawnNumber);

        StartCoroutine(ReSpawn(InReSpawnTime));
    }

    public bool isActive => mapObject.activeSelf;

    public Vector2 MapPosition => mapObject.transform.position;
}