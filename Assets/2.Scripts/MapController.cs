using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController
{
    public Dictionary<int, Map> mapList = new Dictionary<int, Map>();

    public void Init()
    {
        for(int i = 0; i < mapList.Count; i++)
        {
            GetMapData(i);
        }
    }

    public void OnEnable()
    {
        Init();
    }

    public void OnDisable()
    {


    }

    private void GetMapData(int InCharacterld)
    {
        if (mapList.TryGetValue(InCharacterld, out var outCharacter) == false)
            return;

        outCharacter.Init();
    }
}