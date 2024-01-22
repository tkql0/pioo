using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    Vector3 CamPos = new Vector3(0, 0, -10);
    public GameObject player;
    public float speed;

    [SerializeField]
    Vector2 center;
    [SerializeField]
    Vector2 mapSize;

    float height;

    private void OnEnable()
    {
        //GameTree.MAP.cameraControll = this;

        //player = GameTree.GAME.objectController.myCharater;
        //speed = player.GetComponent<MyCharater>().moveMaxspeed;

        height = Camera.main.orthographicSize;

        player = GameTree.GAME.objectController.playerList[0].GetComponent<MyCharater>().gameObject;
        speed = player.GetComponent<MyCharater>().moveMaxspeed;
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

        transform.position = new Vector3(transform.position.x, clampY, -10f);
    }
}
