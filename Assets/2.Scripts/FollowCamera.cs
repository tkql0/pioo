using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    Vector3 CamPos;
    [SerializeField]
    GameObject player;

    [SerializeField]
    Vector2 center;
    [SerializeField]
    Vector2 mapSize;

    float speed;

    float height;

    private void Start()
    {
        if (GameTree.GAME.objectController.playerDataList.Count == 0)
            return;

        CamPos = new Vector3(0, 0, -15);
        speed = GameTree.GAME.objectController.playerDataList[0].moveMaxspeed;
        player = GameTree.GAME.objectController.playerDataList[0].gameObject;

        GameTree.GAME.objectController.playerDataList[0].cam = Camera.main;

        height = Camera.main.orthographicSize;
    }

    private void FixedUpdate()
    {
        LimitCamArea();
    }

    void LimitCamArea()
    {
        transform.position = Vector3.Lerp(transform.position,
            player.transform.position + CamPos, Time.fixedDeltaTime * speed);
        float ly = mapSize.y - height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

        transform.position = new Vector3(transform.position.x, clampY, -40f);
    }
}
