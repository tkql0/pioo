using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Player : Character
{
    public GameObject player;

    public Camera cam;
    public Slider attackPowerSlider;

    private Vector2 _inputVector;
    private Vector2 _gravityPoint;

    public float moveAddSpeed;
    public float moveSpeed;
    public float moveSpeedX;
    public float moveSpeedY;

    private bool _isMove;
    private bool _isJump;
    private bool _isBreath;
    private bool _isSwimming;
    private bool _isEat;
    public bool isLv_up;

    public float maxBreath;
    public float curBreath;

    public float maxExperience;
    public float curExperience;

    public int PlayerLv = 1;

    public int fishItemCount = 0;
    public int fishItemMaxCount = 10;

    private float _jumpPower = 0;

    private float _attackPower = 0.0f;
    public float attackMinPower = 5.0f;
    public float attackMaxPower = 10.0f;

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

        moveAddSpeed = 2f;
        moveSpeed = 6f;
        isDie = false;
        _isMove = false;
        _isJump = false;
        isDamage = false;
    }

    private void Update()
    {
        if (isDie == false && GameManager.UI._isClick == false)
        {
            if(Input.GetKey(KeyCode.F) && fishItemCount > 0 && !_isEat)
                StartCoroutine(EatDelay());
            LookAtMouse();
            Move();
        }
    }
    private IEnumerator EatDelay()
    {
        _isEat = true;
           curExperience += 5;
        fishItemCount--;
        yield return new WaitForSeconds(1f);
        // 먹는 모습 활성화 시간
        _isEat = false;
    }

    private void FixedUpdate()
    {
        if (isDie == true)
            return;

        if (_isSwimming)
        {
            float moveMaxSpeed = moveSpeed + moveAddSpeed;

            MoveSpeed(moveMaxSpeed);
        }
        else
            MoveSpeed(moveSpeed);
    }

    private void MoveSpeed(float MaxSpeed)
    {
        if (!_isMove)
            return;

        moveSpeedX = rigid.velocity.x;
        moveSpeedY = rigid.velocity.y;

        rigid.gravityScale = 0.1f;
        rigid.AddForce((_inputVector).normalized, ForceMode2D.Impulse);

        if (Mathf.Abs(moveSpeedX) > MaxSpeed)
            rigid.velocity = new Vector2(Mathf.Sign(moveSpeedX) * MaxSpeed, moveSpeedY);

        if (Mathf.Abs(moveSpeedY) > MaxSpeed)
            rigid.velocity = new Vector2(moveSpeedX, Mathf.Sign(moveSpeedY) * MaxSpeed);
    }

    public override void Move()
    {
        _inputVector = new Vector2(Input.GetAxisRaw(Horizontal), Input.GetAxisRaw(Vertical));

        if (_inputVector.x != 0)
            sprite.flipX = _inputVector.x > 0;

        float gravityPointY = characterPosition.y - _gravityPoint.y;

        _isBreath = Mathf.Abs(gravityPointY) < 1 ? true : false;
        _isMove = gravityPointY <= 0 ? true : false;

        if (gravityMaxPointY < gravityPointY)
        {
            gravityMaxPointY = gravityPointY + 2;
        }

        if (Input.GetKey(KeyCode.LeftShift) && !_isBreath)
            _isSwimming = true;
        else if(!Input.GetKey(KeyCode.LeftShift))
            _isSwimming = false;

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
            Jump();
    }

    private void Jump()
    {
        curBreath = maxBreath;
        if (curHealth < maxHealth)
            curHealth += Time.deltaTime;

        if (!_isJump)
        {
            if (_isSwimming)
            {
                rigid.gravityScale = gravityMaxPointY;
                rigid.AddForce(Vector2.up * (moveSpeedY + _jumpPower), ForceMode2D.Impulse);
                _isJump = true;
            }
            else
                player.transform.position = new Vector2(characterPosition.x, 0);
        }
    }

    private void Attack(Vector2 InDirection)
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        attackPowerSlider.maxValue = attackMinPower;

        if (Input.GetMouseButton(0))
        {
            attackPowerSlider.gameObject.SetActive(true);
            _attackPower += Time.deltaTime;
            attackPowerSlider.value = _attackPower;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            attackPowerSlider.gameObject.SetActive(false);

            _attackPower += attackMinPower;
            if (_attackPower > attackMaxPower)
                _attackPower = attackMaxPower;

            GameObject Attack = GameManager.SPAWN.GetObjectSpawn
                (characterPosition, weaponSpawnKey, ObjectType.PlayerWeapon);
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
        Vector2 dir = new Vector2(mousePos.x - transform.position.x,
            mousePos.y - transform.position.y);
        dir = dir.normalized;

        Attack(dir);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Fish))
        {
            if (fishItemMaxCount < fishItemCount)
                return;
            fishItemCount++;
            collision.gameObject.SetActive(false);
        }

        if (!isDamage)
        {
            if (collision.gameObject.CompareTag(Enemy_Attack))
            {
                isDamage = true;
                collision.gameObject.SetActive(false);

                int Critical = Random.Range(1, 5);

                if(Critical == 4)
                    curHealth = curHealth - (damage + enemyCriticalDamage);
                else
                    curHealth = curHealth - damage;
                StartCoroutine(OnDamage(sprite));

                isDamage = false;
            }
        }
    }

    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";
}