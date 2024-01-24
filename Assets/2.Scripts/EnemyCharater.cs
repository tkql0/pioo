using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharater : AttackableCharacter
{
    [SerializeField]
    Rigidbody2D rigid;
    [SerializeField]
    SpriteRenderer sprite;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {

    }

    public void Move()
    {
        int nextMove = Random.Range(-1, 2);
        if (nextMove != 0)
            sprite.flipX = nextMove < 0;

        float speed = Random.Range(0.1f, 5);
        rigid.velocity = new Vector2(nextMove * speed, rigid.velocity.y);

        StartCoroutine(MoveDelay());
    }

    public IEnumerator MoveDelay()
    {
        float next_MoveTime = Random.Range(1, 6f);
        //var wfs = new WaitForSeconds(next_MoveTime);
        yield return new WaitForSeconds(next_MoveTime);
        Move();
    }
}

public class EnemyData : EnemyCharater
{

}