using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCharater : AttackAbleCharacter
{
    public Vector2 inputVec;
    public float move_Maxspeed = 20f;

    [SerializeField]
    Rigidbody2D rigid;
    [SerializeField]
    SpriteRenderer sprite;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        ObjectMove(rigid, sprite);
    }
    private void FixedUpdate()
    {
            rigid.gravityScale = 0.2f;
            rigid.AddForce(inputVec.normalized, ForceMode2D.Impulse);

            if (rigid.velocity.x > move_Maxspeed)
                rigid.velocity = new Vector2(move_Maxspeed, rigid.velocity.y);
            else if (rigid.velocity.x < move_Maxspeed * (-1))
                rigid.velocity = new Vector2(move_Maxspeed * (-1), rigid.velocity.y);

            if (rigid.velocity.y > move_Maxspeed)
                rigid.velocity = new Vector2(rigid.velocity.x, move_Maxspeed);
            else if (rigid.velocity.y < move_Maxspeed * (-1))
                rigid.velocity = new Vector2(rigid.velocity.x, move_Maxspeed * (-1));
    }

    public override void ObjectMove(Rigidbody2D rigid, SpriteRenderer sprite)
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");

        if (inputVec.x != 0)
            sprite.flipX = inputVec.x > 0;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Experience"))
        {
            //collision.
        }
    }
}

public class PlayerData : MyCharater
{

}