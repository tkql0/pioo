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

    public ObjectType key;
    public long mySpawnNumber;
    public long targetSpawnNumber;
    // Player의 mySpawnNumber을 받을 변수

    //private bool isRelocation = false;
    public int stayPlayer = 0;
    // 해당 맵에 머무르고 있는 Player가 0이라면 비활성화
    // 해당 맵에 Player가 머물고있는지 확인을 하는 법은
    // 맵의 x좌표의 40(맵과의 간격)을 나눠서
    // MapController 클래스에 있는 mapPositionKey[mySpawnNumber]에 저장 후
    // 같은 숫자가 있는 for문으로 확인



    private void Update()
    {
        MapPosition();
    }

    //private void MapRelocation()
    private void MapPosition()
    { // 맵이 아닌 맵이 있을 공간을 이동
        Vector2 targetPosition = GameManager.OBJECT.player.characterPosition;
        // Dictionary로 만들 Player들의 mySpawnNumber을 받고
        // GameManager.OBJECT.player[targetSpawnNumber].characterPosition
        // 각자 지정된 Player를 따라다니는 MapPosition()

        float DistanceX = targetPosition.x - mapPosition.x;
        float differenceX = Mathf.Abs(DistanceX);

        DistanceX = DistanceX > 0 ? 1 : -1;

        GameManager.SPAWN.DeSpawn(targetPosition, DeSpawn_Distance + 15);
        // 맵의 이동과는 별개로 몬스터는 거리에 따라 비활성화

        if (differenceX > DeSpawn_Distance)
        { // 맵이 Palyer에게서 멀어졌을때
            mapObject.SetActive(false);
            GameManager.SPAWN.DeSpawn(targetPosition, DeSpawn_Distance - 15);
            // 맵과 몬스터를 비활성화

            transform.Translate(Vector2.right * DistanceX * 120);
            // MapPosition()이 이동
        }
    }

    public bool isActive => mapObject.activeSelf;

    public Vector2 mapPosition => transform.position;

    private const float DeSpawn_Distance = 60f;
}
