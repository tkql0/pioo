using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{
    public GameObject player;
    public Slider attackPowerSlider;

    private Vector2 _inputVector;
    public float moveMaxspeed;

    private Vector2 _gravityPoint;

    public Camera cam;

    private bool _isMove;
    private bool _isJump;
    public bool isLv_up;

    public float maxBreath;
    public float curBreath;

    public float maxExperience;
    public float curExperience;

    public int PlayerLv = 1;

    private float _attackPower = 0.0f;
    private float _attackMinPower = 10.0f;
    private float _attackMaxPower = 20.0f;


    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";

    public void OnEnable()
    {
        player = gameObject;
        key = ObjectType.Player;

        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        _gravityPoint = new Vector2(0, 0);

        curHealth = maxHealth;
        curBreath = maxBreath;

        moveMaxspeed = 20f;
        isDie = false;
        _isMove = false;
        _isJump = false;
        isDamage = false;
    }

    private void Update()
    {
        LookAtMouse();
        Movement();
    }

    private void FixedUpdate()
    {
        if (isDie == false && _isMove)
        {
            rigid.gravityScale = 0.2f;
            rigid.AddForce(_inputVector.normalized, ForceMode2D.Impulse);

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
            _inputVector.x = Input.GetAxisRaw(Horizontal);
            _inputVector.y = Input.GetAxisRaw(Vertical);

            if (_inputVector.x != 0)
                sprite.flipX = _inputVector.x > 0;

            float gravityPointY = transform.position.y - _gravityPoint.y;
            _isMove = gravityPointY <= 0 ? true : false;

            if (_isMove)
            {
                _isMove = true;
                _isJump = false;
                rigid.gravityScale = 0.2f;

                if (curBreath > 0.0f)
                    curBreath -= Time.deltaTime;
                else
                    curHealth -= Time.deltaTime;
            }
            else if (!_isMove && Input.GetKey(KeyCode.LeftShift))
            {
                _isMove = false;
                rigid.gravityScale = 4f;
                curBreath = maxBreath;
                if (curHealth < maxHealth)
                    curHealth += Time.deltaTime;

                if (!_isJump)
                {
                    rigid.AddForce(Vector2.up * rigid.velocity.y, ForceMode2D.Impulse);
                    _isJump = true;
                }
            }
            else if (!_isMove && !Input.GetKey(KeyCode.LeftShift))
            {
                rigid.gravityScale = 7f;
                curBreath = maxBreath;
                if (curHealth < maxHealth)
                    curHealth += Time.deltaTime;
            }
        }
    }

    private void Attack(Vector2 InDirection)
    {
        SpawnController _spawnController = GameManager.SPAWN;

        attackPowerSlider.maxValue = _attackMinPower;

        if (Input.GetMouseButton(0))
        {
            attackPowerSlider.gameObject.SetActive(true);
            _attackPower += Time.deltaTime;
            attackPowerSlider.value = _attackPower;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            attackPowerSlider.gameObject.SetActive(false);

            _attackPower = _attackPower + _attackMinPower;
            if (_attackPower > _attackMaxPower)
                _attackPower = _attackMaxPower;

            GameObject Attack = _spawnController.ObjectSpawn(transform.position, spawnWeaponKey, ObjectType.PlayerWeapon);
            Attack.GetComponent<Rigidbody2D>().velocity = InDirection * _attackPower;
            _attackPower = 0.0f;
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

        Attack(dir);
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