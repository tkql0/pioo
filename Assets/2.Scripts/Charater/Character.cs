using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public LayerMask targetMask;

    public ObjectType key;
    public long targetSpawnNumber;
    public long mySpawnNumber;
    public long weaponPower = 10;
    public long weaponSpawnKey = -1;

    public GameObject characterObject;

    public Rigidbody2D rigid;
    public SpriteRenderer sprite;

    public int damage = 5;

    public float coolTimeMax;
    public float coolTime;

    public float maxHealth;
    public float curHealth;

    public bool isDie;
    public bool isDamage;

    public virtual void Move()
    {
        if (isActive == false)
            return;

        int nextMove = GetRandomPosition(Left_Position, Right_Position);
        float speed = GetRandomSpeed(Min_Speed, Max_Speed);

        SetNextMove(nextMove, speed);
    }

    public IEnumerator OnDamage(SpriteRenderer InSprite)
    {
        InSprite.color = Color.red;

        yield return new WaitForSeconds(0.5f);

        InSprite.color = Color.white;

        isDamage = false;
    }

    public void Die(float curHealth)
    {
        if (curHealth <= 0)
        {
            isDie = true;

            SetActiveObject(false);
            return;
        }
        else
            isDie = false;
    }
    public IEnumerator MoveDelay(float InMinDelayTime, float InMaxDelayTime)
    {
        Move();
        float next_MoveTime = GetRandomDelayTime(InMinDelayTime, InMaxDelayTime);
        yield return new WaitForSeconds(next_MoveTime);
        StartCoroutine(MoveDelay(InMinDelayTime, InMaxDelayTime));
    }

    public void SetActiveObject(bool InIsActive)
    {
        characterObject?.SetActive(InIsActive);
    }

    public bool isActive => characterObject.activeSelf;

    public Vector2 characterPosition => characterObject.transform.position;

    public void SetKey(ObjectType InType) => key = InType;
    public void SetSpawnNumber(long InNumber) => mySpawnNumber = InNumber;
    public int GetRandomPosition(int InLeft_Position, int InRight_Position)
    {
        return Random.Range(InLeft_Position, InRight_Position);
    }
    public float GetRandomSpeed(float InMin_Speed, float InMax_Speed)
    {
        return Random.Range(InMin_Speed, InMax_Speed);
    }

    public float GetRandomDelayTime(float InMinDelayTime, float InMaxDelayTime)
    {
        return Random.Range(InMinDelayTime, InMaxDelayTime);
    }

    public void SetNextMove(int InNextPostion, float InNextSpeed)
    {
        transform.localScale = InNextPostion <= 0 ? _leftPosition : _rightPosition;
        rigid.velocity = new Vector2(InNextPostion * InNextSpeed, rigid.velocity.y);
    }

    public const float Max_Speed = 5f;
    public const float Min_Speed = 0.5f;

    public const int Left_Position = -1;
    public const int Right_Position = 2;
    public Vector2 _leftPosition => new Vector2(-1, 1);
    public Vector2 _rightPosition => new Vector2(1, 1);

    public const string Fish = "Fish_exp";
    public const string Player = "Player";
    public const string Enemy = "Enemy";
    public const string Enemy_Attack = "Enemy_Attack";
    public const string Player_Attack = "Player_Attack";
}