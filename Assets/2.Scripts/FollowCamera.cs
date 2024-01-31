using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private Vector3 _camPos;
    private GameObject _player;

    [SerializeField]
    private Vector2 _center;
    [SerializeField]
    private Vector2 _mapSize;

    private float _speed;

    private float _height;

    private void Start()
    {
        if (!GameTree.GAME.objectController.player)
            return;

        _camPos = new Vector3(0, 0, -40f);
        _speed = GameTree.GAME.objectController.player.moveMaxspeed;
        _player = GameTree.GAME.objectController.player.gameObject;

        GameTree.GAME.objectController.player.cam = Camera.main;

        _height = Camera.main.orthographicSize;
    }

    private void FixedUpdate()
    {
        LimitCamArea();
    }

    private void LimitCamArea()
    {
        transform.position = Vector3.Lerp(transform.position,
            _player.transform.position + _camPos, Time.fixedDeltaTime * _speed);
        float ly = _mapSize.y - _height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + _center.y, ly + _center.y);

        transform.position = new Vector3(transform.position.x, clampY, _camPos.z);
    }
}
