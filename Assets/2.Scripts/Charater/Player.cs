using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class Player : Character
{
    public event Action OnPlayerHP;

    public GameObject player;

    public Camera cam;
    public Slider attackPowerSlider;

    private Animator _anim;

    private Vector2 _inputVector;

    public Vector2 playerPosition;

    public float moveSpeed = 0.0f;
    public float moveMaxSpeed = 0.0f;
    public float moveSpeedX = 0.0f;
    public float moveSpeedY = 0.0f;

    public bool isSwimming;
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

    private float _attackPower = 0.0f;
    private float _chargingPower = 0.0f;
    public float attackMinPower = 0.0f;
    public float attackMaxPower = 6.0f;

    private float _gravityPointY = 0;

    private float _recoveryTime = 0f;
    private float _recoveryCoolTime = 2f;

    private float _drownTime = 0f;
    private float _drownCoolTime = 2f;

    public void OnEnable()
    {
        player = gameObject;
        characterObject = gameObject;
        key = ObjectType.Player;

        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>();

        _gravityPoint = new Vector2(0, 0);

        curHealth = maxHealth;
        curBreath = maxBreath;

        moveSpeed = 6f;
        moveMaxSpeed = 8f;
        isDie = false;
        isSwimming = false;
        _isJump = false;
        isDamage = false;
        _isSwimmingTest = true;
    }

    private void Update()
    {
        playerPosition = transform.position;

        playerPosition.y = Mathf.Clamp(playerPosition.y, -38f, 40f);

        transform.position = playerPosition;

        if (_inputVector.magnitude == 0 && _isSwimmingTest)
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f,
                rigid.velocity.normalized.y * 0.5f);

        if (isDie == false && GameManager.UI._isClick == false)
        {
            if(Input.GetKey(KeyCode.F) && fishItemCount > 0 && !_isEat)
                StartCoroutine(EatDelay());
            // 버튼을 끌어다가 경험치 실린더로 끌어오면 오르는 걸로 바꾸고 싶어

            SwimmingPoint();

            Jump();

            if (transform.position.y < -1)
            {
                _drownTime += Time.deltaTime;

                if (_drownTime >= _drownCoolTime)
                {
                    if (curBreath > 0.0f)
                        curBreath--;
                    // 조이스틱이 호흡량에 따라 제한이 될텐데
                    // 물속에 들어가자마자 줄어들면 아무것도 할 수가 없어
                    // 일정 시간마다 늘어나는 걸로 하자
                    else
                        Damage(1);
                    //    // 호흡게이지가 0이상이라면 호흡게이지 감소
                    //    // 0 이하라면 체력 감소


                    _drownTime = 0;
                }
            }
            else
            {
                if (_isSwimmingTest)
                {
                    curBreath = maxBreath;
                }

                _recoveryTime += Time.deltaTime;

                if (curHealth < maxHealth && _recoveryTime >= _recoveryCoolTime)
                    Recovery(1);
            }

            LookAtMouse();
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
        // 움직임에 대해 최대 속도를 사용 할 건지 확인후 적용
        // 최대 속도가 아니면 호흡게이지가 다는 속도는 0.7정도 최대 속도면 1
    }

    private void MoveSpeed(float MaxSpeed)
    {
        if (!isSwimming)
            return;

        moveSpeedX = rigid.velocity.x;
        moveSpeedY = rigid.velocity.y;

        if(isBreathTest)
            rigid.gravityScale = 0f;
        else
            rigid.gravityScale = 0.2f;

        rigid.AddForce(_inputVector.normalized, ForceMode2D.Impulse);

        if (Mathf.Abs(moveSpeedX) > MaxSpeed)
            rigid.velocity = new Vector2(Mathf.Sign(moveSpeedX) * MaxSpeed, moveSpeedY);

        if (Mathf.Abs(moveSpeedY) > MaxSpeed)
            rigid.velocity = new Vector2(moveSpeedX, Mathf.Sign(moveSpeedY) * MaxSpeed);
    }

    public void PlayerMove(Vector2 InInputDirection)
    {
        _inputVector = InInputDirection;

        if (_inputVector.x != 0)
            spriteRenderer.flipX = _inputVector.x > 0;

        _anim.SetBool("isRun", Mathf.Abs(_inputVector.x) > 0);
    }

    public bool isBreathTest;

    void SwimmingPoint()
    {
        // 수영할 수 있는 단계를 따로 나눠 놓자
        _gravityPointY = characterPosition.y - _gravityPoint.y;
        // 수면과 플레이어와의 거리

        //bool isBreathTest;
        // 점프를 하면 이것과 관계없이 숨을 쉴 수 있어

        if(_isSwimmingTest)
        {
            // 수영 버튼이 눌려있다면
            isBreathTest = Mathf.Abs(_gravityPointY) < 1 ? true : false;
            // 수면와 플레이어의 거리가 절대값 1만큼 있다면 호흡 true
        }
        else
        {
            isBreathTest = false;
        }

        isSwimming = _gravityPointY <= 0 ? true : false;
        _anim.SetBool("isDiving", isBreathTest);
    }

    private void Jump()
    {
        if (isSwimming)
        {
            // Player의 Y좌표가 0을 넘지않았다면
            _isJump = false;
            // 수영 중일 때 점프 가능 상태 만들기
        }
        else
        {
            // Player의 Y좌표가 0을 넘었을 때
            if (_isSwimmingTest)
            { // 수영 버튼이 눌려있다면
                transform.position = new Vector2(transform.position.x, 0);
                // Player의 Y좌표를 0으로 고정
            }
            else
            { // 수영 버튼이 눌려있지 않았다면
                rigid.gravityScale = 5f;
                // 중력값을 변경

                if (!_isJump)
                {
                    rigid.AddForce(Vector2.up * moveSpeedY, ForceMode2D.Impulse);
                    _isJump = true;
                    _anim.SetBool("isRun", true);
                    _anim.SetBool("isDiving", false);
                    // 점프 중일 때는 수영중인 애니메이션
                }
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

        if(!_isSwimmingTest)
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
    }

    public void Damage(int InDamage)
    {
        isDamage = true;

        curHealth -= InDamage;
        OnPlayerHP?.Invoke();

        if (curHealth <= 0 && !isDie)
        {
            curHealth = 0;
            Debug.Log("Die");
            Death();
        }
    }

    public void Recovery(int InRecovery)
    {
        _recoveryTime = 0;

        curHealth += InRecovery;
        OnPlayerHP?.Invoke();
    }

    private void Death()
    {
        isDie = true;

        // 죽는 애니메이션
        // 다른 게임들 해보니까 딱히 게임이 끝났다고 멈추지 않아도 될거같아
        // 끝난 뒤에 남아있는 캐릭터들이 싸우고 있거나 움직이고 있는거 보고 있는 것도 재미있더라
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
}