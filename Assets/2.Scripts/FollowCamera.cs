using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private Vector3 _cameraPosition;
    private Player _player;

    [SerializeField]
    private Vector3 _centerPosition;
    [SerializeField]
    private Vector2 _mapSize;

    private float _height;

    private void Start()
    {
        if (!GameManager.OBJECT.player)
            return;

        _cameraPosition = transform.position;
        _centerPosition = new Vector3(0, 0, -20f);

        _player = GameManager.OBJECT.player;

        _player.cam = Camera.main;

        _height = Camera.main.orthographicSize;
    }

    private void FixedUpdate()
    {
        LimitCamArea();
    }

    private void LimitCamArea()
    {
        _cameraPosition = GetLerp();
        float diffY = _mapSize.y - _height;
        float clampY = Mathf.Clamp(_cameraPosition.y,
            -diffY + _centerPosition.y, diffY + _centerPosition.y);

        transform.position = new Vector3(_cameraPosition.x, clampY, _centerPosition.z);
    }

    private Vector3 GetLerp()
    {
        return Vector3.Lerp(_cameraPosition, (Vector3)_player.characterPosition +
            _centerPosition, Time.fixedDeltaTime * _player.moveSpeed);
    }
}
