using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController
{
    public void Init()
    {

    }

    public void OnEnable()
    {

    }

    public void OnDisable()
    {


    }
}


public class MapRelocation : MonoBehaviour
{
    ObjectController objectController = GameTree.GAME.objectController;

    public void Init()
    {

    }

    public void OnEnable()
    {

    }

    public void OnDisable()
    {


    }
    void PosDisX()
    {
        for (int i = 0; i < objectController.playerList.Count; i++)
        {
            Vector3 playerPosition = objectController.playerList[i].transform.position;
            for (int j = 0; j < objectController.mapLish.Count; j++)
            {
                Vector3 myPosition = transform.position;

                float DirX = playerPosition.x - playerPosition.x;
                float diffX = Mathf.Abs(DirX);

                DirX = DirX > 0 ? 1 : -1;

                if (diffX > 60.0f)
                {
                    transform.Translate(Vector3.right * DirX * 120);
                    return;
                }
            }
        }
    }
}