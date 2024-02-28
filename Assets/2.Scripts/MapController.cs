using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController
{
    public float[] mapPositionKey;
    // 해당 배열의 크기는 맵을 생성하며 mapSpawnSize로 초기화
    // 맵마다의 position key로써 Player가 맵이 겹치는지 확인용

    public void OnEnable()
    {
        GameManager.SPAWN.spawnObject.SpawnMapPool();
    }

    public (long, long) GetReSpawnSize(long InKey)
    {
        long enemyReSpawnSize = 0;
        long fishReSpawnSize = 0;

        foreach (KeyValuePair<long, Character> outCharacterData in GameManager.OBJECT.characterDataList)
        {
            if (outCharacterData.Value.targetSpawnNumber == InKey)
            {
                if (GameManager.OBJECT.GetisActive(outCharacterData.Key, ObjectType.Enemy) == false)
                    enemyReSpawnSize++;
                else if (GameManager.OBJECT.GetisActive(outCharacterData.Key, ObjectType.Fish) == false)
                    fishReSpawnSize++;
            }
        }

        return (enemyReSpawnSize, fishReSpawnSize);
    }

    public void SetMapPositionKey(long SpawnNumber, Vector2 mapPosition)
    {
        GameManager.Map.mapPositionKey[SpawnNumber] = (int)mapPosition.x / 40;
    }

    public int StayPlayerCount(long SpawnNumber)
    {
        int stayPlayer = 0;
        // 확인전 초기화

        foreach(int i in mapPositionKey)
        {
            if (mapPositionKey[i] == mapPositionKey[SpawnNumber])
                stayPlayer++;
            // 맵의 Position Key를 돌며 같은 Key가 있는지 확인 후 저장
        }

        return stayPlayer;
    }
}
