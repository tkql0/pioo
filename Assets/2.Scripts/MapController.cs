using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController
{
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
}
