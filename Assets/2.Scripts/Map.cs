using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MAPType
{
    RANDOM,
    SETTING,
    EVENT,
}

public class Map : MonoBehaviour
{
    public GameObject mapObject;
    public ObjectType key;
    public long mySpawnNumber;
    public long targetSpawnNumber;

    public long enemyMaxSize;
    public long fishMaxSize;

    private float reSpawn_Time = 10f;
    public float positionNumber;

    public bool isRelocation = false;

    public MAPType mapType;

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

    private void RandomMap()
    {
        mapType = (MAPType)Random.Range(0, 3);

        switch (mapType)
        {
            case MAPType.RANDOM:
                enemyMaxSize = Random.Range(1, 4);
                fishMaxSize = Random.Range(1, 10);

                MapMonsterSpawn(enemyMaxSize, fishMaxSize);
                break;
            case MAPType.SETTING:
                enemyMaxSize = GameManager.OBJECT.player.PlayerLv / 30;
                fishMaxSize = GameManager.OBJECT.player.fishItemCount;

                Debug.Log("고요한 바다");

                MapMonsterSpawn(enemyMaxSize, fishMaxSize);
                break;
            case MAPType.EVENT:
                enemyMaxSize = Random.Range(0, 10);
                fishMaxSize = Random.Range(0, 20);

                Debug.Log("흔들리는 바다");

                MapMonsterSpawn(enemyMaxSize, fishMaxSize);
                break;
        }
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

            RandomMap();
            positionNumber = (int)MapPosition.x / 40;
        }
    }

    public void MapMonsterSpawn(long InEnemySize, long InFishSize)
    {
        GameManager.SPAWN.Spawn(MapPosition, InEnemySize, InFishSize, mySpawnNumber);

        StartCoroutine(ReSpawn(reSpawn_Time));
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

    private const float DeSpawn_Distance = 60f;

    public void SetActiveObject(bool InIsActive)
    {
        mapObject?.SetActive(InIsActive);
    }
}