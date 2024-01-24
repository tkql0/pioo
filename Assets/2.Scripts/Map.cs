using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    //ObjectController objectController = GameTree.GAME.objectController;
    //SpawnController spawnController = GameTree.GAME.spawnController;

    public void Init()
    {

    }

    public void OnEnable()
    {

    }

    public void OnDisable()
    {


    }

    //void mapRelocation()
    //{
    //    for (int i = 0; i < objectController.playerList.Count; i++)
    //    {
    //        Vector3 targetPosition = objectController.playerList[i].transform.position;
    //        for (int j = 0; j < objectController.mapLish.Count; j++)
    //        {
    //            Vector3 myPosition = objectController.mapLish[j].transform.position;

    //            float DistanceX = targetPosition.x - myPosition.x;
    //            float differenceX = Mathf.Abs(DistanceX);

    //            DistanceX = DistanceX > 0 ? 1 : -1;

    //            if (differenceX > 60.0f)
    //            {
    //                transform.Translate(Vector3.right * DistanceX * 120);
    //                return;
    //            }
    //            else
    //                return;
    //        }
    //    }
    //}
}
