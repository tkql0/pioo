using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public GameObject player;

    public int key = 0;
    public ObjectType spawnNumber = ObjectType.NULL;

    private Vector2 inputVec;
    public float moveMaxspeed;
    private float time;

    private Vector3 gravityPoint;

    public Camera cam;

    private bool isMove;
    private bool isJump;
    public bool isDie;
    public bool isLv_up;
    private bool isDamage;
    private bool mouse_click;

    public float maxHealth;
    public float curHealth;

    public float maxBreath;
    public float curBreath;

    public float maxExperience;
    public float curExperience;

    public int PlayerLv = 1;

    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";

    public void OnEnable()
    {
        player = gameObject;

        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        gravityPoint = new Vector3(0, 0, 0);

        curHealth = maxHealth;
        curBreath = maxBreath;

        moveMaxspeed = 20f;
        isDie = false;
        isMove = false;
        isJump = false;
        isDamage = false;
    }

    private void Update()
    {
        LookAtMouse();
        PlayerMovement();
    }
    private void FixedUpdate()
    {
        if (isDie == false && isMove)
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

    private void PlayerMovement()
    {
        if (isDie == false)
        {
            inputVec.x = Input.GetAxisRaw(Horizontal);
            inputVec.y = Input.GetAxisRaw(Vertical);

            if (inputVec.x != 0)
                sprite.flipX = inputVec.x > 0;

            float gravityPointY = transform.position.y - gravityPoint.y;
            isMove = gravityPointY <= 0 ? true : false;

            if (isMove)
            {
                isMove = true;
                isJump = false;
                rigid.gravityScale = 0.2f;

                if (curBreath > 0.0f)
                    curBreath -= Time.deltaTime;
                else
                    curHealth -= Time.deltaTime;
            }
            else if (!isMove && Input.GetKey(KeyCode.LeftShift))
            {
                isMove = false;
                rigid.gravityScale = 4f;
                curBreath = maxBreath;
                if (curHealth < maxHealth)
                    curHealth += Time.deltaTime;

                if (!isJump)
                {
                    rigid.AddForce(Vector2.up * rigid.velocity.y, ForceMode2D.Impulse);
                    isJump = true;
                }
            }
            else if (!isMove && !Input.GetKey(KeyCode.LeftShift))
            {
                rigid.gravityScale = 7f;
                curBreath = maxBreath;
                if (curHealth < maxHealth)
                    curHealth += Time.deltaTime;
            }
        }
    }

    private void PlayerAttack(Vector2 dir)
    {
        if (Input.GetMouseButtonDown(0) && mouse_click == false)
        {
            mouse_click = true;
            time = 10f;
        }
        else if (Input.GetMouseButton(0))
        {
            time += Time.deltaTime;
        }
        else if (Input.GetMouseButtonUp(0) && isJump == true && mouse_click == true)
        {
            mouse_click = false;
            GameObject Attack = GameManager.SPAWN.SpawnPlayerWapon(transform.position);
            Attack.GetComponent<Rigidbody2D>().velocity = dir * time;
        }
    }
    private void LookAtMouse()
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        dir = dir.normalized;

        PlayerAttack(dir);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Fish))
        {
            curExperience += 5;
            collision.gameObject.SetActive(false);
        }
        if (!isDamage)
        {
            if (collision.gameObject.CompareTag(Enemy_Attack))
            {
                isDamage = true;
                collision.gameObject.SetActive(false);
                curHealth = curHealth - Damage;
                StartCoroutine(OnDamage(sprite));

                isDamage = false;
            }
        }
    }
}