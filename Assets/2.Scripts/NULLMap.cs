using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum MapType
//{
//    NULL,
//    RandomMap,
//    EventMap,
//    PlayerSettingMap,
//}
// 다시.. 이 SpawnMap class의 목적은 맵의 이동과 맵의 생성

public class NULLMap : MonoBehaviour
{
    //public List<GameObject> mapObjects = new List<GameObject>();
    // 맵 오브젝트는 각자 플레이어 수의 3배씩 생성되고
    // 맵 이동시 stayPlayer가 2이상이 아니라면
    // 비활성화 되어있는 맵 오브잭트 중에서 랜덤으로 활성화
    // 굳이 전체 오브젝트중 비활성화된 맵을 찾는것보다
    // 3개의 맵을 할당해서 그 3개 중에서 확률로 고르게하면 되겠네

    public ObjectType key;
    public long mySpawnNumber;
    public long targetSpawnNumber;
    // Player의 mySpawnNumber을 받을 변수

    public float mapPositionKey;

    public GameObject mabObject;

    //private bool isRelocation = false;
    public int stayPlayer;
    // 해당 맵에 머무르고 있는 Player가 0이라면 비활성화
    // 해당 맵에 Player가 머물고있는지 확인을 하는 법은
    // 맵의 x좌표의 40(맵과의 간격)을 나눠서
    // MapController 클래스에 있는 mapPositionKey[mySpawnNumber]에 저장 후
    // 같은 숫자가 있는지 for문으로 확인


    private void Update()
    {
        MapPosition();
    }

    private void MapPosition()
    { // 맵이 아닌 맵이 있을 공간을 이동
        mapPositionKey = myMapPosition.x / 40f;

        //GameManager.Map.SetMapPositionKey(mySpawnNumber, myMapPosition);
        // 맵의 현재 위치 확인

        Vector2 targetPosition = GameManager.OBJECT.player.characterPosition;
        // Dictionary로 만들 Player들의 mySpawnNumber을 받고
        // GameManager.OBJECT.player[targetSpawnNumber].characterPosition
        // 각자 지정된 Player를 따라다니는 MapPosition()

        float DistanceX = targetPosition.x - myMapPosition.x;
        float differenceX = Mathf.Abs(DistanceX);

        DistanceX = DistanceX > 0 ? 1 : -1;

        MapRelocation(DistanceX, targetPosition);

        //GameManager.SPAWN.DeSpawn(targetPosition, DeSpawn_Distance + 15);
        // 맵의 이동과는 별개로 몬스터는 거리에 따라 비활성화

        if (differenceX > DeSpawn_Distance)
        { // 맵이 Palyer에게서 멀어졌을때
            MapRelocation(DistanceX, targetPosition);
        }
    }

    private void MapRelocation(float InDistance, Vector2 targetPosition)
    {
        //foreach (KeyValuePair<long, NULLMap> outMapData in GameManager.OBJECT.testMapDataList)
        //{
        //    if (outMapData.Value.mapPositionKey == mapPositionKey)
        //    {
        //        mabObject = outMapData.Value.mabObject;
        //        stayPlayer++;
        //        // 생각
        //        // 맵의 포지션과 맵 오브젝트를 독자적으로 두고
        //        // stayPlayer가 1이상인 오브젝트는 활성화
        //        // 아니면 비활성화
        //    }
        //}

            //if (GameManager.Map.StayPlayerCount(mySpawnNumber) <= 1)
            //{ // 이동 전 맵에 존재하는 플레이어가 1 이하일 때 (자신 포함)
            //foreach (GameObject map in mapObjects)
            //{ // 위 조건이 맞을 시 맵 이동시 리스트에 있는 오브젝트들을 모두 비활성화하기
            //    map.SetActive(false);
            //    // 이 상황에서 2번째 맵 이동을 할때 아직도 맵에 플레이어가 있었다면
            //    // 갑자기 맵이 사라지는 상황이 나오겠지?
            //    // 딕셔너리로 맵을 저장하고 비활성화 됬을때 꺼낸다 해도 초기화 되니까
            //    // 비활성화 된 오브젝트 중에서 꺼내는게 좋으려나
            //}
            //GameManager.SPAWN.DeSpawn(targetPosition, DeSpawn_Distance - 15);
            // 맵과 몬스터를 비활성화
        //}

        mapSpawnObject.Translate(Vector2.right * InDistance * 120);
        // MapPosition()이 이동

        //if (GameManager.Map.StayPlayerCount(mySpawnNumber) <= 1)
        //{ // 이동 후 맵에 존재하는 플레이어가 1 이하일 때 (자신 포함)

        //}
        //else
        //{
        //    //otherMabObject = 
        //}
    }

    private void MapRandomSpawn()
    { // 비활성화된 맵 오브젝트중 하나 랜덤으로 활성화

    }

    public Vector2 myMapPosition => transform.position;

    public Transform mapSpawnObject => transform;

    private const float DeSpawn_Distance = 60f;
}
