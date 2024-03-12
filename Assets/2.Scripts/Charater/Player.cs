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

    public float moveSpeed = 0.0f;
    public float moveMaxSpeed = 0.0f;
    public float moveSpeedX = 0.0f;
    public float moveSpeedY = 0.0f;

    private bool _isMove;
    private bool _isJump;
    private bool _isBreath;
    private bool _isSwimming;
    private bool _isEat;
    private bool _isCharging = false;

    public bool isLv_up;

    public float maxBreath = 0.0f;
    public float curBreath = 0.0f;

    public float maxExperience = 0.0f;
    public float curExperience = 0.0f;

    public int PlayerLv = 1;
    public int LvPoint = 50;

    public int fishItemCount = 0;
    public int fishItemMaxCount = 10;
    public int fishEatCount = 1;

    private float _jumpPower = 0.0f;

    private float _attackPower = 0.0f;
    private float _chargingPower = 0.0f;
    public float attackMinPower = 0.0f;
    public float attackMaxPower = 6.0f;

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

        moveSpeed = 6f;
        moveMaxSpeed = 8f;
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

    private void FixedUpdate()
    {
        if (isDie == true)
            return;

        if (_isSwimming)
            MoveSpeed(moveMaxSpeed);
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
        rigid.AddForce(_inputVector.normalized, ForceMode2D.Impulse);

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

        attackPowerSlider.maxValue = attackMaxPower - attackMinPower;

        if (Input.GetMouseButton(0))
            _isCharging = true;

        if (_isCharging)
        {
            attackPowerSlider.gameObject.SetActive(true);
            _chargingPower += Time.deltaTime;
            attackPowerSlider.value = _chargingPower;
        }

        if (Input.GetMouseButtonUp(0))
        {
            attackPowerSlider.gameObject.SetActive(false);

            if (_chargingPower >= attackMaxPower)
                _chargingPower = attackMaxPower;

            _attackPower += _chargingPower + attackMinPower;

            GameObject Attack = GameManager.SPAWN.GetObjectSpawn
                (characterPosition, weaponSpawnKey, ObjectType.PlayerWeapon);
            Attack.GetComponent<Rigidbody2D>().velocity = InDirection * _attackPower;

            _attackPower = 0.0f;
            _chargingPower = 0.0f;
            _isCharging = false;
        }
    }

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
            //if (!collision.gameObject.TryGetComponent<Weapon>(out var OutWeapon))
            //    return;
            {

            //if (OutWeapon.key != ObjectType.EnemyWeapon)
            //    return;

            isDamage = true;
            collision.gameObject.SetActive(false);

            int Critical = Random.Range(1, 5);

            if (Critical == 4)
                curHealth = curHealth - (damage + enemyCriticalDamage);
            else
                curHealth = curHealth - damage;
            StartCoroutine(OnDamage(sprite));

            isDamage = false;
            }
        }
    }

    private IEnumerator EatDelay()
    {
        _isEat = true;
        curExperience += 5;
        if (fishItemCount < fishEatCount)
            fishItemCount -= fishItemCount;
        else
            fishItemCount -= fishEatCount;
        yield return new WaitForSeconds(1f);
        // 먹는 모습 활성화 시간
        _isEat = false;
    }

    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";
}