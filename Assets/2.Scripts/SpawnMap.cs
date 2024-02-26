using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MapType
{
    NULL,
    RandomMap,
    EventMap,
    PlayerSettingMap,
}

public class SpawnMap : MonoBehaviour
{
    public Transform mapSpawnObject;
    public GameObject mapObject;

    public LayerMask targetMask;
    private RaycastHit2D[] _inTarget;

    public ObjectType key;
    public long mySpawnNumber;
    public long targetSpawnNumber;
    // 플레이어의 key를 받아올 예정

    private bool isRelocation = false;
    public bool isStayPlayer = false;

    public long enemyMaxSize;
    public long fishMaxSize;

    private float ReSpawn_Time = 10f;

    public void OnEnable()
    {
        isStayPlayer = true;

        enemyMaxSize = Random.Range(1, 6);
        fishMaxSize = Random.Range(1, 15);
    }

    private void Update()
    {
        MapRelocation();
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
            mapObject.SetActive(false);
            GameManager.SPAWN.DeSpawn(targetPosition, DeSpawn_Distance - 15);

            transform.Translate(Vector2.right * DistanceX * 120);
        }
    }

    public bool isActive => mapObject.activeSelf;

    public Vector2 MapPosition => mapObject.transform.position;

    private const float DeSpawn_Distance = 60f;

    private void SetMapSetting(GameObject mapObject)
    {
        if (isRelocation == false)
            return;

        _inTarget = Physics2D.CircleCastAll(mapSpawnObject.position, 1, Vector2.zero, 0, targetMask);

        if (_inTarget == null)
        {
            // 1.
            // int RandomTypeMap = Random.Range(1, 4);
            // 3가지 맵중 하나를 생성
            // 2.
            // foreach (KeyValuePair<long, SpawnMap> outMapData in GameManager.OBJECT.spawnMapDataList)
            // 비활성화 되어있던 맵중 하나를 불러오기
            foreach (KeyValuePair<long, Map> outMapData in GameManager.OBJECT.mapDataList)
            {
                if (outMapData.Value.isActive == true)
                    break;

                enemyMaxSize = Random.Range(1, 6);
                fishMaxSize = Random.Range(1, 15);

                outMapData.Value.MapMonsterSpawn(enemyMaxSize, fishMaxSize);
            }

            return;
        }

        isStayPlayer = true;

        Transform target = _inTarget[0].transform;
        mapObject = target.gameObject;

        switch (mapObject.tag)
        {
            case "RandomMap":
                new RandomMapData();
                break;
            case "EventMap":
                break;
            case "PlayerSettingMap":
                break;
        }
    }

    //private void SetSpawnMap(long InIndex)
    //{
    //    if (GameManager.OBJECT.spawnMapDataList.TryGetValue(InIndex, out var outMapData) == false)
    //        return;
    //}
}
