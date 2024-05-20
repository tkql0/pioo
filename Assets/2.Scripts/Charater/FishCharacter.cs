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

    private bool _isJump;

    private float _ranMoveSpeed;

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

        _ranMoveSpeed = 10f;

        SetKey(ObjectType.Fish);
        StartCoroutine(MoveDelay(_characterData.MinDelayTime, _characterData.MaxDelayTime));

        _gravityPoint = new Vector2(0, 0);
    }

    private void Update()
    {
        Jump();
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

    private void Jump()
    {
        float _gravityPointY = characterPosition.y - _gravityPoint.y;

        bool isSwimming = _gravityPointY <= 0 ? true : false;

        float moveSpeedY = rigid.velocity.y;

        if (isSwimming)
        {
            rigid.gravityScale = 0f;
            _isJump = false;
        }
        else
        {
            if (!_isJump)
            {
                rigid.gravityScale = 2f;
                rigid.AddForce(Vector2.up * moveSpeedY, ForceMode2D.Impulse);
                _isJump = true;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;

        Gizmos.DrawSphere(_scanObject.transform.position, _characterData.SightRange);
    }

    private void ObjectScan()
    {
        _inTarget = Physics2D.CircleCastAll(_scanPosition,
            _characterData.SightRange, Vector2.zero, 0, targetMask);

        Vector2 player;

        if (_inTarget.Length == 0)
            return;

        player = _inTarget[0].transform.position;

        //Vector2 myPosition = transform.position;

        Vector2 dir = _scanPosition - player;
        dir.Normalize();

        if (Vector2.Distance(_scanPosition, player) > _characterData.SightRange)
        {
            _detection.SetActive(false);
            return;
        }
        _detection.SetActive(true);

        int randomRanPosition = Random.Range(0, 3);

        transform.localScale = dir.x <= 0 ? _leftPosition : _rightPosition;

        // 도망중에는 탐색을 하지 못하게 바꾸자

        switch (randomRanPosition)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
        }

        transform.position += (Vector3)dir * _ranMoveSpeed * Time.deltaTime;

        // Player에게서 도망칠때 이동 함수는 뒤로 미룰수 있으면 좋겠네
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