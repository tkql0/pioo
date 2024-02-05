using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{
    public GameObject player;
    public Slider attackPowerSlider;

    public int key = 0;
    public ObjectType spawnNumber = ObjectType.NULL;

    private Vector2 inputVec;
    public float moveMaxspeed;

    private Vector2 gravityPoint;

    public Camera cam;

    private bool isMove;
    private bool isJump;
    public bool isLv_up;
    private bool mouse_click;

    public float maxBreath;
    public float curBreath;

    public float maxExperience;
    public float curExperience;

    public int PlayerLv = 1;

    float attackPower = 0.0f;
    float attackMinPower = 10.0f;
    float attackMaxPower = 20.0f;


    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";

    public void OnEnable()
    {
        player = gameObject;

        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        gravityPoint = new Vector2(0, 0);

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
        Movement();
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

    public override void Movement()
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
        attackPowerSlider.maxValue = attackMinPower;

        if (Input.GetMouseButton(0))
        {
            attackPowerSlider.gameObject.SetActive(true);
            attackPower += Time.deltaTime;
            attackPowerSlider.value = attackPower;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            attackPowerSlider.gameObject.SetActive(false);

            attackPower = attackPower + attackMinPower;
            if (attackPower > attackMaxPower)
                attackPower = attackMaxPower;

            GameObject Attack = GameManager.SPAWN.SpawnPlayerWapon(transform.position);
            Attack.GetComponent<Rigidbody2D>().velocity = dir * attackPower;
            attackPower = 0.0f;
        }
    }
    // 레벨 업을 했을 때 포인트를 얻을 시 올릴 수 있는 능력치
    // 1. 공격 최대 사거리 : attackMaxPower
    // 2. 공격 최소 사거리 : attackMinPower
    // 생각 했던 것 중에 공격이 비처럼 내리는 게 있었는데 한다면
    // 공격 갯수를 늘리는거랑 스킬로 비를 내리는거랑 머가 나으려나

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