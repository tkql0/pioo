using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapControll : MonoBehaviour
{
    GameObject myObj;

    private void Start()
    {
        myObj = gameObject;
    }

    private void Update()
    {
        if (GameTree.Instance.gameManager.playerControll != null)
        {
            switch(myObj.tag)
            {
                case "SeaLevel":
                    PosDisY();
                    break;
                case "Map":
                        PosDisX(30.0f);
                    break;
            }
        }
    }

    void PosDisX(float dis)
    {
        Vector3 playerPos = GameTree.Instance.gameManager.playerControll.transform.position;
        Vector3 myPos = myObj.transform.position;

        float DirX = playerPos.x - myPos.x;
        float diffX = Mathf.Abs(DirX);

        DirX = DirX > 0 ? 1 : -1;

        if (diffX > dis)
        {
            transform.Translate(Vector3.right * DirX * 60);
            return;
        }
    }

    public void PosDisY()
    {
        Vector3 playerPos = GameTree.Instance.gameManager.playerControll.transform.position;
        Vector3 myPos = myObj.transform.position;

        float DirY = playerPos.y - myPos.y;

        GameTree.Instance.gameManager.playerControll.isJump = DirY >= 0 ? true : false;
    }
}
// 맵마다 정해진 수만큼의 오브젝트만 랜덤 생성되게 하는게 좋으려나
