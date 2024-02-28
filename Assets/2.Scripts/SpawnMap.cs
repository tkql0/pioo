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
    public GameObject mapObject;
    // 맵 오브젝트는 각자 플레이어 수의 3배씩 생성되고
    // 맵 이동시 stayPlayer가 2이상이 아니라면
    // 비활성화 되어있는 맵 오브잭트 중에서 랜덤으로 활성화

    public ObjectType key;
    public long mySpawnNumber;
    public long targetSpawnNumber;
    // Player의 mySpawnNumber을 받을 변수

    //private bool isRelocation = false;
    //public int stayPlayer;
    // 해당 맵에 머무르고 있는 Player가 0이라면 비활성화
    // 해당 맵에 Player가 머물고있는지 확인을 하는 법은
    // 맵의 x좌표의 40(맵과의 간격)을 나눠서
    // MapController 클래스에 있는 mapPositionKey[mySpawnNumber]에 저장 후
    // 같은 숫자가 있는지 for문으로 확인



    //private void Update()
    //{
    //    MapPosition();
    //}

    //private void MapRelocation()
    private void MapPosition()
    { // 맵이 아닌 맵이 있을 공간을 이동
        GameManager.Map.SetMapPositionKey(mySpawnNumber, myMapPosition);
        // 맵의 현재 위치 확인

        Vector2 targetPosition = GameManager.OBJECT.player.characterPosition;
        // Dictionary로 만들 Player들의 mySpawnNumber을 받고
        // GameManager.OBJECT.player[targetSpawnNumber].characterPosition
        // 각자 지정된 Player를 따라다니는 MapPosition()

        float DistanceX = targetPosition.x - myMapPosition.x;
        float differenceX = Mathf.Abs(DistanceX);

        DistanceX = DistanceX > 0 ? 1 : -1;

        GameManager.SPAWN.DeSpawn(targetPosition, DeSpawn_Distance + 15);
        // 맵의 이동과는 별개로 몬스터는 거리에 따라 비활성화

        if (differenceX > DeSpawn_Distance)
        { // 맵이 Palyer에게서 멀어졌을때
            if (GameManager.Map.StayPlayerCount(mySpawnNumber) <= 1)
            { // 이동 전 맵에 존재하는 플레이어가 1 이하일 때 (자신 포함)
                mapObject.SetActive(false);
                GameManager.SPAWN.DeSpawn(targetPosition, DeSpawn_Distance - 15);
                // 맵과 몬스터를 비활성화
            }

            mapSpawnObject.Translate(Vector2.right * DistanceX * 120);
            // MapPosition()이 이동

            if (GameManager.Map.StayPlayerCount(mySpawnNumber) <= 1)
            { // 이동 후 맵에 존재하는 플레이어가 1 이하일 때 (자신 포함)

            }
        }
    }

    private void MapRandomSpawn()
    { // 비활성화된 맵 오브젝트중 하나 랜덤으로 활성화

    }

    public bool isActive => mapObject.activeSelf;

    public Vector2 myMapPosition => transform.position;
    public Transform mapSpawnObject => transform;

    private const float DeSpawn_Distance = 60f;
}
