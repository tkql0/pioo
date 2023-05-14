using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    public Player player;
    public GameObject SeaLevel;

    public void PosDisY()
    {
        Vector3 playerPos = player.transform.position;
        Vector3 SeaLevelPos = SeaLevel.transform.position;

        float DirY = playerPos.y - SeaLevelPos.y;

        player.isJump = DirY <= 0 ? true : false;
    }
}
