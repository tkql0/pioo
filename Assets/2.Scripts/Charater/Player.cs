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
    private bool _isBreath;
    public bool isLv_up;

    public float maxBreath;
    public float curBreath;

    public float maxExperience;
    public float curExperience;

    public int PlayerLv = 1;
    private float _jumpPower = 0;

    private float _attackPower = 0.0f;
    private float _attackMinPower = 5.0f;
    private float _attackMaxPower = 10.0f;


    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";

    public float gravityMaxPointY = 0;

    public void OnEnable()
    {
        player = gameObject;
        characterObject = gameObject;
        key = ObjectType.Player;

        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        _gravityPoint = new Vector2(0, 0);

        curHealth = maxHealth;
        curBreath = maxBreath;

        moveMaxspeed = 10f;
        isDie = false;
        _isMove = false;
        _isJump = false;
        isDamage = false;
    }

    private void Update()
    {
        if (isDie == false)
        {
            LookAtMouse();
            Move();
        }
    }

    private void FixedUpdate()
    {
        if (isDie == false && _isMove)
        {
            rigid.gravityScale = 0.1f;
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

    public override void Move()
    {
        _inputVector.x = Input.GetAxisRaw(Horizontal);
        _inputVector.y = Input.GetAxisRaw(Vertical);

        if (_inputVector.x != 0)
            sprite.flipX = _inputVector.x > 0;

        float gravityPointY = characterPosition.y - _gravityPoint.y;

        _isBreath = Mathf.Abs(gravityPointY) < 1 ? true : false;
        _isMove = gravityPointY <= 0 ? true : false;

        if (gravityMaxPointY < gravityPointY)
        {
            gravityMaxPointY = gravityPointY + 2;
        }

        if (_isMove)
        {
            _isJump = false;

            rigid.gravityScale = 0.1f;
            gravityMaxPointY = 0f;

            if (!_isBreath)
            {
                if (curBreath > 0.0f)
                    curBreath -= Time.deltaTime;
                else
                    curHealth -= Time.deltaTime;
            }
        }
        else
        {
            curBreath = maxBreath;
            if (curHealth < maxHealth)
                curHealth += Time.deltaTime;

            if (!_isJump && Input.GetKey(KeyCode.LeftShift))
            {
                rigid.gravityScale = gravityMaxPointY;
                rigid.AddForce(Vector2.up * (rigid.velocity.y + _jumpPower), ForceMode2D.Impulse);
                _isJump = true;
            }
            else if(!_isJump && !Input.GetKey(KeyCode.LeftShift))
            {
                player.transform.position = new Vector2(characterPosition.x, 0);
            }
        }
    }

    private void Attack(Vector2 InDirection)
    {
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

            GameObject Attack = GameManager.SPAWN.GetObjectSpawn
                (characterPosition, weaponSpawnKey, ObjectType.PlayerWeapon);
            Attack.GetComponent<Rigidbody2D>().velocity = InDirection * _attackPower;

            _attackPower = 0.0f;
        }
    }
    // ���� ���� ���� �� ����Ʈ�� ���� �� �ø� �� �ִ� �ɷ�ġ
    // 1. ���� �ִ� ��Ÿ� : attackMaxPower
    // 2. ���� �ּ� ��Ÿ� : attackMinPower
    // ���� �ߴ� �� �߿� ������ ��ó�� ������ �� �־��µ� �Ѵٸ�
    // ���� ������ �ø��°Ŷ� ��ų�� �� �����°Ŷ� �Ӱ� ��������

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
                curHealth = curHealth - damage;
                StartCoroutine(OnDamage(sprite));

                isDamage = false;
            }
        }
    }
}