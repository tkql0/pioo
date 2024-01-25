using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController
{
    public Dictionary<int, Map> mapList = new Dictionary<int, Map>();
    //public Dictionary<int, GameObject> mapList = new Dictionary<int, GameObject>();

    public int enemyMaxSize = 0;
    public int enemySize = 0;

    Map map;

    public void Init()
    {
        for (int i = 0; i < mapList.Count; i++)
        {
            MapCommand(i);
        }
    }

    public void OnEnable()
    {
        //mapList = new Dictionary<int, Map>();

        Init();
    }

    public void OnDisable()
    {


    }
    public void MapCommand(int InCharacterld)
    {
        if (mapList.TryGetValue(InCharacterld, out var outCharacter) == false)
            return;

        outCharacter.Init();
        //outCharacter.mapRelocation(new MapController());
    }
}