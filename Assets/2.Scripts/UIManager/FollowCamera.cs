using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private Vector3 CamPos = new Vector3(0, 0, -15);

    public void Init()
    {
        targatCamera();
    }

    private void targatCamera()
    {
        Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y, CamPos.z);
        transform.position = targetPosition;
    }

    //ObjectController _objectController;

    //int loginNumber = 0;

    //Vector3 CamPos = new Vector3(0, 0, -10);
    //[SerializeField]
    //GameObject player;

    //[SerializeField]
    //Vector2 center;
    //[SerializeField]
    //Vector2 mapSize;

    //float speed;

    //float height;

    //private void Start()
    //{
    //    _objectController = GameTree.GAME.objectController;

    //    for (int i = 0; i < _objectController.playerList.Count; i++)
    //    {
    //        if (loginNumber != _objectController.playerDataList[i].key)
    //            return;

    //        player = _objectController.playerList[loginNumber];
    //        speed = _objectController.playerDataList[i].moveMaxspeed;
    //    }

    //    height = Camera.main.orthographicSize;
    //}

    //private void FixedUpdate()
    //{
    //    LimitCamArea();
    //}

    //void LimitCamArea()
    //{
    //    transform.position = Vector3.Lerp(transform.position,
    //        player.transform.position + CamPos, Time.fixedDeltaTime * speed);
    //    float ly = mapSize.y - height;
    //    float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);

    //    transform.position = new Vector3(transform.position.x, clampY, -10f);
    //}
}
