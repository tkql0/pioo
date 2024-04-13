using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public CharacterData _characterData;
    public CharacterData characterData { set { _characterData = value; } }

    public LayerMask targetMask;
    public Rigidbody2D rigid;
    public SpriteRenderer spriteRenderer;

    public ObjectType key;
    public long targetSpawnNumber;
    public long mySpawnNumber;
    public GameObject characterObject;

    public long weaponPower = 10;
    public long weaponSpawnKey = -1;

    public int damage = 4;
    public int playerDamage = 4;
    public int playerCriticalDamage = 3;
    public int enemyCriticalDamage = 2;

    public float coolTimeMax;
    public float coolTime;

    public float maxHealth = 0.0f;
    public float curHealth = 0.0f;

    public bool isDie;
    public bool isDamage;
    public bool isBattle;

    public RaycastHit2D[] _inTarget;

    public Vector2 _gravityPoint;

    public float gravityMaxPointY = 0;

    public virtual void Move()
    {
        if (isActive == false)
            return;

        int nextMove = GetRandomPosition(Left_Position, Right_Position);
        float speed = GetRandomSpeed(Min_Speed, Max_Speed);

        SetNextMove(nextMove, speed);
    }

    private void SetNextMove(int InNextPostion, float InNextSpeed)
    {
        transform.localScale = InNextPostion <= 0 ? _leftPosition : _rightPosition;
        rigid.velocity = new Vector2(InNextPostion * InNextSpeed, rigid.velocity.y);
    }

    public IEnumerator MoveDelay(float InMinDelayTime, float InMaxDelayTime)
    {
        Move();
        float next_MoveTime = GetRandomDelayTime(InMinDelayTime, InMaxDelayTime);
        yield return new WaitForSeconds(next_MoveTime);
        StartCoroutine(MoveDelay(InMinDelayTime, InMaxDelayTime));
    }

    public IEnumerator OnDamage(SpriteRenderer InSprite)
    {
        SetColor(InSprite, Color.red);

        yield return new WaitForSeconds(0.5f);

        SetColor(InSprite, Color.white);

        isDamage = false;
    }

    public void Dead(float InCurHealth)
    {
        if (InCurHealth <= 0)
        {
            isDie = true;

            SetActiveObject(false);
            return;
        }
        else
            isDie = false;
    }

    public bool isActive => characterObject.activeSelf;

    public Vector2 characterPosition => characterObject.transform.position;

    public void SetActiveObject(bool InIsActive)
    {
        characterObject?.SetActive(InIsActive);
    }

    public void SetKey(ObjectType InType) => key = InType;

    public void SetSpawnNumber(long InNumber) => mySpawnNumber = InNumber;

    public void SetColor(SpriteRenderer InSprite, Color Incolor)
    {
        InSprite.color = Incolor;
    }

    public int GetRandomPosition(int InLeftPosition, int InRightPosition)
    {
        return Random.Range(InLeftPosition, InRightPosition);
    }

    public int GetRandomCount(int InMinCount, int InMaxCount)
    {
        return Random.Range(InMinCount, InMaxCount);
    }

    public float GetRandomSpeed(float InMinSpeed, float InMaxSpeed)
    {
        return Random.Range(InMinSpeed, InMaxSpeed);
    }

    public float GetRandomDelayTime(float InMinDelayTime, float InMaxDelayTime)
    {
        return Random.Range(InMinDelayTime, InMaxDelayTime);
    }

    public const float Max_Speed = 5f;
    public const float Min_Speed = 0.5f;

    public const int Left_Position = -1;
    public const int Right_Position = 2;
    public Vector2 _leftPosition => new Vector2(-1, 1);
    public Vector2 _rightPosition => new Vector2(1, 1);

    public const string Fish = "Fish";
    public const string Player = "Player";
    public const string Enemy = "Enemy";
    public const string Enemy_Attack = "Enemy_Attack";
    public const string Player_Attack = "Player_Attack";
}