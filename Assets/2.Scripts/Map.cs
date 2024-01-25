using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    private ObjectController objectController;

    public void Init()
    {
        objectController = GameTree.GAME.objectController;
    }

    public void OnEnable()
    {

    }

    public void OnDisable()
    {


    }

    public void mapRelocation()
    {
        //for (int i = 0; i < objectController.playerList.Count; i++)
        //{
        //    Vector3 targetPosition = objectController.playerList[i].transform.position;
        //        Vector3 myPosition = this.transform.position;

        //        float DistanceX = targetPosition.x - myPosition.x;
        //        float differenceX = Mathf.Abs(DistanceX);

        //        DistanceX = DistanceX > 0 ? 1 : -1;

        //        if (differenceX > 60.0f)
        //        {
        //            transform.Translate(Vector3.right * DistanceX * 120);
        //            return;
        //        }
        //        else
        //            return;
        //}
    }
}
