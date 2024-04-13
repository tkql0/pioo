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

    private Vector2 _playerPosition;

    private Animator _anim;

    private Vector2 _inputVector;

    public float moveSpeed = 0.0f;
    public float moveMaxSpeed = 0.0f;
    public float moveSpeedX = 0.0f;
    public float moveSpeedY = 0.0f;

    private bool _isSwimming;
    private bool _isJump;
    // 플레이어가 점프를 하는 구간
    private bool _isBreath;
    // 플레이어가 숨을 쉬는 구간
    private bool _isEat;
    private bool _isCharging = false;

    public bool _isSwimmingTest;
    // 버튼이 눌렸을때 Player는 position.y가 0을 넘지 못함
    // 그리고 버튼을 누르고 있는 동안 중력이 0되고 공격을 하지 못함
    // Position.y가 0이면 호흡게이지가 초기화됨

    // 버튼을 누르지 않았을때 position.y가 0을 넘으면 rigid.velocity만큼 점프함
    // Position.y가 0이상이려도 호흡게이지가 초기화되지 않고
    // 줄어든 호흡 게이지만큼 공격 가능

    // 해당 방법으로 만들면 실린더를 사용해서 어색하게 만들 필요없이
    // 체력은 하트 호흡게이지는 물방울 모양으로 만들 수 있을거 같다 생각함
    public bool _isMoveChangeTest;
    // 버튼을 눌렀다면 Player의 속도가 최대 가속도 까지 늘어남

    // 모바일은 키보드가 없기 때문에 기능이 간편해야 된다 생각함

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

    public void OnEnable()
    {
        player = gameObject;
        characterObject = gameObject;
        key = ObjectType.Player;

        _playerPosition = transform.position;

        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();

        _gravityPoint = new Vector2(0, 0);

        curHealth = maxHealth;
        curBreath = maxBreath;

        moveSpeed = 6f;
        moveMaxSpeed = 8f;
        isDie = false;
        _isSwimming = false;
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
            //Move();
        }
    }

    private void FixedUpdate()
    {
        if (isDie == true)
            return;

        if (_isMoveChangeTest)
            MoveSpeed(moveMaxSpeed);
        else
            MoveSpeed(moveSpeed);
    }

    private void MoveSpeed(float MaxSpeed)
    {
        if (!_isSwimming)
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

    public void PlayerMove(Vector2 InInputDirection)
    {
        // 이동 함수가 조이스틱으로 이동
        //_inputVector = new Vector2(Input.GetAxisRaw(Horizontal), Input.GetAxisRaw(Vertical));
        _inputVector = InInputDirection;

        if (_isBreath)
        {
            _anim.SetFloat("Speed", Mathf.Abs(_inputVector.x));
        }

        if (_inputVector.x != 0)
            spriteRenderer.flipX = _inputVector.x > 0;

        float gravityPointY = characterPosition.y - _gravityPoint.y;

        _isBreath = Mathf.Abs(gravityPointY) < 1 ? true : false;
        _isSwimming = gravityPointY <= 0 ? true : false;

        if (gravityMaxPointY < gravityPointY)
        {
            gravityMaxPointY = gravityPointY + 2;
        }

        if (_isSwimming)
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

        if (_isSwimmingTest)
        {
            if (_playerPosition.y < 0)
            {
                _playerPosition.y = 0;
            }
            return;
        }
        // 상태가 수영중이라면 점프 불가
        // 
        else
        {
            if (!_isJump && _playerPosition.y < 0)
            {
                rigid.gravityScale = gravityMaxPointY;
                rigid.AddForce(Vector2.up * (moveSpeedY + _jumpPower), ForceMode2D.Impulse);
                _isJump = true;
            }
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
            //Weapon weapon = Attack.GetComponent<Rigidbody2D>().velocity = InDirection * _attackPower;
            Weapon weapon = Attack.GetComponent<Weapon>();

            weapon.damage = playerDamage;
            weapon.criticalDamage = playerCriticalDamage;

            weapon.rigid.velocity = InDirection * _attackPower;

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
        if (collision.TryGetComponent<FishCharacter>(out var OutFish))
        {
            if (fishItemMaxCount < fishItemCount)
                return;

            fishItemCount++;
            OutFish.SetActiveObject(false);
        }

        if (!isDamage)
        {
            if (!collision.TryGetComponent<Weapon>(out var OutWeapon))
                return;

            if (OutWeapon.key != ObjectType.EnemyWeapon)
                return;

            isDamage = true;
            collision.gameObject.SetActive(false);

            int Critical = Random.Range(1, 5);

            isDamage = true;

            if (Critical == 4)
                curHealth -= OutWeapon.damage + OutWeapon.criticalDamage;
            else
                curHealth -= OutWeapon.damage;

            StartCoroutine(OnDamage(spriteRenderer));
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