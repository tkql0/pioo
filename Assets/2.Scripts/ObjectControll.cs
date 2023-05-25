using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectControll : MonoBehaviour
{
    GameObject myObj;

    private void Awake()
    {
        myObj = gameObject;
    }

    public void GameStart()
    {
            switch (myObj.tag)
            {
                case "Enemy":
                    break;
                case "Enemy_exp":
                    break;
                case "Fish_exp":
                    break;
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
}
