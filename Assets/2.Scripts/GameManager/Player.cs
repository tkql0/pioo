using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : AttackableCharacter
{
    public GameObject player;

    public int key = 0;

    public Vector2 inputVec;
    public float moveMaxspeed = 20f;

    Vector3 gravityPoint = new Vector3(0, 0, 0);

    [SerializeField]
    Rigidbody2D rigid;
    [SerializeField]
    SpriteRenderer sprite;

    bool isMove;
    bool isJump;

    void OnEnable()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        //isMove = false;
        isJump = false;
    }

    private void Update()
    {
        //if (!Input.anyKey)
        //    return;

        PlayerMove();
    }
    private void FixedUpdate()
    {
        if (isMove)
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
    }

    void PlayerMove()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");

        if (inputVec.x != 0)
            sprite.flipX = inputVec.x > 0;

        float gravityPointY = transform.position.y - gravityPoint.y;
        isMove = gravityPointY <= 0 ? true : false;

        if (isMove)
        {
            isMove = true;
            isJump = false;
            rigid.gravityScale = 0.2f;
        }
        else if (!isMove && Input.GetKey(KeyCode.Space))
        {
            isMove = false;
            rigid.gravityScale = 4f;

            if (!isJump)
            {
                rigid.AddForce(Vector2.up * rigid.velocity.y, ForceMode2D.Impulse);
                isJump = true;
            }
        }
        else if (!isMove && !Input.GetKey(KeyCode.Space))
        {
            rigid.gravityScale = 7f;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Fish_exp"))
            collision.gameObject.SetActive(false);
    }
}