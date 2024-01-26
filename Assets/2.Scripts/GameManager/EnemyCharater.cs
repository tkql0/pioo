using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharater : AttackableCharacter
{
    [SerializeField]
    Rigidbody2D rigid;
    [SerializeField]
    SpriteRenderer sprite;

    public int key;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {

    }

    private void OnEnable()
    {
        key = 0;

        StartCoroutine(MoveDelay());
    }

    public void Move()
    {
        if (!gameObject.activeSelf)
            return;
        int nextMove = Random.Range(-1, 2);
        if (nextMove != 0)
            sprite.flipX = nextMove < 0;

        float speed = Random.Range(0.1f, 5);
        rigid.velocity = new Vector2(nextMove * speed, rigid.velocity.y);
    }

    public IEnumerator MoveDelay()
    {
        Move();
        float next_MoveTime = Random.Range(1, 6f);
        yield return new WaitForSeconds(next_MoveTime);

        StartCoroutine(MoveDelay());
    }
}