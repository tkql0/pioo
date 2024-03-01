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

public class TestMap : MonoBehaviour
{
    public GameObject mabObject;

    public void RandomMap()
    {

    }

    public void SpawnMap(MapType InMapType)
    {
        switch (InMapType)
        {
            case MapType.RandomMap:

                break;
        }
    }

    public void DeSpawnMap()
    {

    }
}
