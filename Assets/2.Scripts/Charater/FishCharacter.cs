using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishCharacter : Character
{
    [SerializeField]
    private GameObject _detection;
    [SerializeField]
    private GameObject _scanObject;

    private Vector2 _scanPosition => _scanObject.transform.position;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        targetSpawnNumber = 99;
        characterObject = gameObject;
    }

    private void OnEnable()
    {
        if (_characterData == null)
            return;

        SetKey(ObjectType.Fish);
        StartCoroutine(MoveDelay(_characterData.MinDelayTime, _characterData.MaxDelayTime));
    }
    private void FixedUpdate()
    {
        ObjectScan();
        GravityPoint();
    }

    private void OnDestroy()
    {
        targetSpawnNumber = 99;
    }

    private void ObjectScan()
    {
        _inTarget = Physics2D.CircleCastAll(_scanPosition,
            _characterData.SightRange, Vector2.zero, 0, targetMask);

        Vector2 player;

        if (_inTarget.Length == 0)
            return;

        player = _inTarget[0].transform.position;

        Vector2 myPosition = transform.position;

        Vector2 dir = _scanPosition - player;
        dir.Normalize();

        if (Vector2.Distance(_scanPosition, player) > _characterData.SightRange)
        {
            _detection.SetActive(false);
            return;
        }
        _detection.SetActive(true);
        transform.position += (Vector3)dir * _characterData.moveSpeed * Time.deltaTime;
    }

    private void GravityPoint()
    {
        float gravityPointY = characterPosition.y - _gravityPoint.y;

        bool _isSwimming = gravityPointY < 0 ? true : false;

        if (gravityMaxPointY < gravityPointY)
        {
            gravityMaxPointY = gravityPointY;
        }

        if(_isSwimming)
        {
            rigid.gravityScale = 0f;
            gravityMaxPointY = 0f;
        }
        else
        {
            rigid.gravityScale = gravityMaxPointY;
        }
    }
}