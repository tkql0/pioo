using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private Vector3 CamPos;
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private Vector2 center;
    [SerializeField]
    private Vector2 mapSize;

    private float speed;

    private float height;

    private void Start()
    {
        if (!GameTree.GAME.objectController.player)
            return;

        CamPos = new Vector3(0, 0, -40f);
        speed = GameTree.GAME.objectController.player.moveMaxspeed;
        player = GameTree.GAME.objectController.player.gameObject;

        GameTree.GAME.objectController.player.cam = Camera.main;

        height = Camera.main.orthographicSize;
    }

    private void FixedUpdate()
    {
        LimitCamArea();
    }

    private void LimitCamArea()
    {
        transform.position = Vector3.Lerp(transform.position,
            player.transform.position + CamPos, Time.fixedDeltaTime * speed);
        float ly = mapSize.y - height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

        transform.position = new Vector3(transform.position.x, clampY, CamPos.z);
    }
}
