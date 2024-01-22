using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCharater : AttackableCharacter
{
    ObjectController objectController = new ObjectController();

    public int key = 0;

    public Vector2 inputVec;
    public float moveMaxspeed = 20f;

    [SerializeField]
    Rigidbody2D rigid;
    [SerializeField]
    SpriteRenderer sprite;

    void OnEnable()
    {
        //objectController.myCharater = this;
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

            if (rigid.velocity.x > moveMaxspeed)
                rigid.velocity = new Vector2(moveMaxspeed, rigid.velocity.y);
            else if (rigid.velocity.x < moveMaxspeed * (-1))
                rigid.velocity = new Vector2(moveMaxspeed * (-1), rigid.velocity.y);

            if (rigid.velocity.y > moveMaxspeed)
                rigid.velocity = new Vector2(rigid.velocity.x, moveMaxspeed);
            else if (rigid.velocity.y < moveMaxspeed * (-1))
                rigid.velocity = new Vector2(rigid.velocity.x, moveMaxspeed * (-1));
    }

    void ObjectMove(Rigidbody2D rigid, SpriteRenderer sprite)
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");

        if (inputVec.x != 0)
            sprite.flipX = inputVec.x > 0;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
    }
}

public class PlayerData : MyCharater
{

}