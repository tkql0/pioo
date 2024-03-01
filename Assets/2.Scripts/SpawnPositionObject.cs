using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPositionObject : MonoBehaviour
{
    public float mapPositionKey;

    public int stayPlayer;

    private void Update()
    {
        MyPosition();
    }

    private void MyPosition()
    { // 맵이 아닌 맵이 있을 공간을 이동
        mapPositionKey = myMapPosition.x / 40f;

        Vector2 targetPosition = GameManager.OBJECT.player.characterPosition;

        float DistanceX = targetPosition.x - myMapPosition.x;
        float differenceX = Mathf.Abs(DistanceX);

        DistanceX = DistanceX > 0 ? 1 : -1;

        Relocation(DistanceX);

        if (differenceX > DeSpawn_Distance)
        {
            Relocation(DistanceX);
        }
    }
    private void Relocation(float InDistance)
    {
        stayPlayer = 0;

        foreach (KeyValuePair<long, SpawnPositionObject> outMapData in GameManager.OBJECT.testSpawmPositionDataList)
        {
            if (outMapData.Value.mapPositionKey == mapPositionKey)
            {
                stayPlayer++;
            }
        }

        mapSpawnObject.Translate(Vector2.right * InDistance * 120);
    }

    public Vector2 myMapPosition => transform.position;
    public Transform mapSpawnObject => transform;

    private const float DeSpawn_Distance = 60f;
}
