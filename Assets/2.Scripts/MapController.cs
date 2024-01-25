using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController
{
    private ObjectController objectController = GameTree.GAME.objectController;
    public int enemyMaxSize = 0;
    public int enemySize = 0;

    public void Init()
    {
        //for (int i = 0; i < objectController.mapList.Count; i++)
        //{
        //    MapCommand(i);
        //}
    }

    public void OnEnable()
    {
        //mapList = new Dictionary<int, Map>();

        Init();
    }

    public void OnDisable()
    {


    }
    //public void MapCommand(int InCharacterld)
    //{
    //    if (objectController.mapList.TryGetValue(InCharacterld, out var outCharacter) == false)
    //        return;

    //    outCharacter.mapRelocation();
    //}
}