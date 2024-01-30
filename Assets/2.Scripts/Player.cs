using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class Player : AttackableCharacter
{
    public GameObject player;

    public int key = 0;

    private Vector2 inputVec;
    public float moveMaxspeed;
    float time;

    private Vector3 gravityPoint;

    [SerializeField]
    private Rigidbody2D rigid;
    [SerializeField]
    private SpriteRenderer sprite;

    public Camera cam;

    private bool isMove;
    private bool isJump;
    public bool isDie;
    bool isLv_up;
    bool isDamage;
    bool mouse_click;

    [SerializeField]
    private float maxHealth;
    private float curHealth;

    [SerializeField]
    private float maxBreath;
    private float curBreath;

    [SerializeField]
    private float maxExperience;
    private float curExperience;

    private int PlayerLv = 1;
    [SerializeField]
    private Text ExpTxt;
    [SerializeField]
    private Text HpTxt;
    [SerializeField]
    private Text BpTxt;
    [SerializeField]
    private Slider healthSlider;
    [SerializeField]
    private Slider breathSlider;
    [SerializeField]
    private Slider expSlider;

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

        healthSlider.value = maxHealth;
        breathSlider.value = maxBreath;
        expSlider.value = 0;

        moveMaxspeed = 20f;
        isDie = false;
        isMove = false;
        isJump = false;
    }

    private void Update()
    {
        if (healthSlider.value <= 0)
        {
            isDie = true;
            Time.timeScale = 0;
        }

        LookAtMouse();
        Value_Update();
        PlayerMove();

        if (expSlider.value == 100 && isLv_up == false)
        {
            isLv_up = true;
            Lv_Up();
        }
        HpTxt.text = (int)curHealth + " / " + maxHealth;
        BpTxt.text = (int)curBreath + " / " + maxBreath;

        //Search();
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
    void Value_Update()
    {
        healthSlider.maxValue = maxHealth;
        breathSlider.maxValue = maxBreath;
        expSlider.maxValue = maxExperience;

        healthSlider.value = curHealth;
        breathSlider.value = curBreath;
        expSlider.value = curExperience;
    }

    private void PlayerMove()
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
            else if (!isMove && Input.GetKey(KeyCode.Space))
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
            else if (!isMove && !Input.GetKey(KeyCode.Space))
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
            GameObject Attack = GameTree.GAME.spawnController.SpawnPlayerWapon(gameObject);
            Attack.GetComponent<Rigidbody2D>().velocity = dir * time;
        }
    }
    void LookAtMouse()
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
                collision.gameObject.SetActive(false);
                curHealth = curHealth - 5;
                StartCoroutine(OnDamage(sprite, isDamage));
            }
        }
    }
    private void Lv_Up()
    {
        curExperience = 0;
        PlayerLv++;
        ExpTxt.text = "Lv. " + PlayerLv;
        isLv_up = false;
        maxHealth += 2;
    }
}