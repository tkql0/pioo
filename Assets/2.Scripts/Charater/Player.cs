using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
//using UnityEngine.InputSystem;

public class Player : Character
{
    public event Action OnPlayerHP;

    public GameObject player;

    public Camera cam;
    public Slider attackPowerSlider;

    private Animator _anim;

    public Vector2 _inputVector;

    public Vector2 playerPosition;

    public float moveSpeed = 0.0f;
    public float moveMaxSpeed = 0.0f;
    public float moveSpeedX = 0.0f;
    public float moveSpeedY = 0.0f;

    public bool isSwimming;
    private bool _isJump;
    // �÷��̾ ������ �ϴ� ����
    private bool _isBreath;
    // �÷��̾ ���� ���� ����
    //private bool _isEat;
    private bool _isCharging = false;

    public bool _isSwimmingJump;
    // ��ư�� �������� Player�� position.y�� 0�� ���� ����
    // �׸��� ��ư�� ������ �ִ� ���� �߷��� 0�ǰ� ������ ���� ����
    // Position.y�� 0�̸� ȣ��������� �ʱ�ȭ��

    // ��ư�� ������ �ʾ����� position.y�� 0�� ������ rigid.velocity��ŭ ������
    // Position.y�� 0�̻��̷��� ȣ��������� �ʱ�ȭ���� �ʰ�
    // �پ�� ȣ�� ��������ŭ ���� ����

    // �ش� ������� ����� �Ǹ����� ����ؼ� ����ϰ� ���� �ʿ����
    // ü���� ��Ʈ ȣ��������� ����� ������� ���� �� ������ ���� ������
    public bool _isRun;
    // ��ư�� �����ٸ� Player�� �ӵ��� �ִ� ���ӵ� ���� �þ

    // ������� Ű���尡 ���� ������ ����� �����ؾ� �ȴ� ������

    public bool isLv_up;

    public float maxBreath = 0.0f;
    public float curBreath = 0.0f;

    public float maxExperience = 0.0f;
    public float curExperience = 0.0f;

    public int PlayerLv = 1;
    public int LvPoint = 50;

    public int fishItemCount = 0;
    public int fishItemMaxCount;
    //public int fishEatCount = 1;


    float DigestionDelay = 20f;
    public float DigestionTime = 0f;

    public int digestionCount = 0;
    public int digestionMaxCount;

    private float _attackPower = 0.0f;
    private float _chargingPower = 0.0f;
    public float attackMinPower = 0.0f;
    public float attackMaxPower = 6.0f;

    private float _gravityPointY = 0;

    private float _recoveryTime = 0f;
    private float _recoveryCoolTime = 2f;

    private float _drownTime = 0f;
    private float _drownCoolTime = 2f;

    // �ʹ� ���� ���߿� ��ũ��Ʈ �������߰ڴ�

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
        _isSwimmingJump = true;

        fishItemMaxCount = 5;
        digestionMaxCount = 5;
    }

    private void Update()
    {
        playerPosition = transform.position;

        playerPosition.y = Mathf.Clamp(playerPosition.y, -38f, 40f);

        transform.position = playerPosition;

        if (_inputVector.magnitude == 0 && _isSwimmingJump)
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f,
                rigid.velocity.normalized.y * 0.5f);

        if (isDie == false && GameManager.UI._isStatusUpdate == false)
        {
            Digestion();

            SwimmingPoint();

            Jump();

            if (transform.position.y < -1)
            {
                _drownTime += Time.deltaTime;

                if (_drownTime >= _drownCoolTime)
                {
                    if (curBreath > 0.0f)
                        curBreath--;
                    // ���̽�ƽ�� ȣ���� ���� ������ ���ٵ�
                    // ���ӿ� ���ڸ��� �پ��� �ƹ��͵� �� ���� ����
                    // ���� �ð����� �þ�� �ɷ� ����
                    else
                        Damage(1);
                    //    // ȣ��������� 0�̻��̶�� ȣ������� ����
                    //    // 0 ���϶�� ü�� ����


                    _drownTime = 0;
                }
            }
            else
            {
                if (_isSwimmingJump)
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
        
        if (_isRun)
            MoveSpeed(moveMaxSpeed);
        else
            MoveSpeed(moveSpeed);
        // �����ӿ� ���� �ִ� �ӵ��� ��� �� ���� Ȯ���� ����
        // �ִ� �ӵ��� �ƴϸ� ȣ��������� �ٴ� �ӵ��� 0.7���� �ִ� �ӵ��� 1
    }

    public void PlayerMove(Vector2 InInputDirection)
    {
        _inputVector = InInputDirection;

        if (_inputVector.x != 0)
            spriteRenderer.flipX = _inputVector.x > 0;

        _anim.SetBool("isRun", Mathf.Abs(_inputVector.x) > 0);
    }

    private void MoveSpeed(float MaxSpeed)
    {
        if (!isSwimming)
            return;

        moveSpeedX = rigid.velocity.x;
        moveSpeedY = rigid.velocity.y;

        if(_isBreath)
            rigid.gravityScale = 0f;
        else
            rigid.gravityScale = 0.2f;

        rigid.AddForce(_inputVector.normalized, ForceMode2D.Impulse);

        if (Mathf.Abs(moveSpeedX) > MaxSpeed)
            rigid.velocity = new Vector2(Mathf.Sign(moveSpeedX) * MaxSpeed, moveSpeedY);

        if (Mathf.Abs(moveSpeedY) > MaxSpeed)
            rigid.velocity = new Vector2(moveSpeedX, Mathf.Sign(moveSpeedY) * MaxSpeed);
    }

    void SwimmingPoint()
    {
        // ������ �� �ִ� �ܰ踦 ���� ���� ����
        _gravityPointY = characterPosition.y - _gravityPoint.y;
        // ����� �÷��̾���� �Ÿ�

        //bool isBreathTest;
        // ������ �ϸ� �̰Ͱ� ������� ���� �� �� �־�

        if(_isSwimmingJump)
        {
            // ���� ��ư�� �����ִٸ�
            _isBreath = Mathf.Abs(_gravityPointY) < 1 ? true : false;
            // ����� �÷��̾��� �Ÿ��� ���밪 1��ŭ �ִٸ� ȣ�� true
        }
        else
        {
            _isBreath = false;
        }

        isSwimming = _gravityPointY <= 0 ? true : false;
        _anim.SetBool("isDiving", _isBreath);
    }

    private void Jump()
    {
        if (isSwimming)
        {
            // Player�� Y��ǥ�� 0�� �����ʾҴٸ�
            _isJump = false;
            // ���� ���� �� ���� ���� ���� �����
        }
        else
        {
            // Player�� Y��ǥ�� 0�� �Ѿ��� ��
            if (_isSwimmingJump)
            { // ���� ��ư�� �����ִٸ�
                transform.position = new Vector2(transform.position.x, 0);
                // Player�� Y��ǥ�� 0���� ����
            }
            else
            { // ���� ��ư�� �������� �ʾҴٸ�
                rigid.gravityScale = 5f;
                // �߷°��� ����

                if (!_isJump)
                {
                    rigid.AddForce(Vector2.up * moveSpeedY, ForceMode2D.Impulse);
                    _isJump = true;
                    _anim.SetBool("isRun", true);
                    _anim.SetBool("isDiving", false);
                    // ���� ���� ���� �������� �ִϸ��̼�
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

            if (!Attack.TryGetComponent<Weapon>(out var OutWeapon))
                return;

            OutWeapon.damage = playerDamage;
            OutWeapon.criticalDamage = playerCriticalDamage;

            OutWeapon.rigid.velocity = InDirection * _attackPower;

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

        if (_isSwimmingJump)
            return;

        Attack(dir);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent<FishCharacter>(out var OutFish))
        {
            if (fishItemMaxCount <= fishItemCount)
                return;

            fishItemCount++;
            OutFish.SetActiveObject(false);
        }
        else if(collision.TryGetComponent<Item>(out var OutItem))
        {
            if (collision.tag != "Fish_exp" || !OutItem.isDrop)
                return;

            fishItemCount++;
            OutItem.SetActiveObject(false);
        }
    }

    public void Damage(int InDamage)
    {
        isDamage = true;

        curHealth -= InDamage;
        OnPlayerHP?.Invoke();

        if(fishItemCount >= 2)
        {
            int RandomDropCount = UnityEngine.Random.Range(1, 3);

            //Debug.Log(RandomDropCount);

            for (int i = 0; i < RandomDropCount; i++)
            {
                GameManager.SPAWN.ItmeSpwan(playerPosition, ObjectType.Item_Fish);
            }

            fishItemCount -= RandomDropCount;
        }
        // ���� ���ݿ��� �����ϱ�

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

        // �״� �ִϸ��̼�
        // �ٸ� ���ӵ� �غ��ϱ� ���� ������ �����ٰ� ������ �ʾƵ� �ɰŰ���
        // ���� �ڿ� �����ִ� ĳ���͵��� �ο�� �ְų� �����̰� �ִ°� ���� �ִ� �͵� ����ִ���
    }

    //private IEnumerator EatDelay()
    //{
    //    _isEat = true;
    //    curExperience += 5;
    //    if (fishItemCount < fishEatCount)
    //        fishItemCount -= fishItemCount;
    //    else
    //        fishItemCount -= fishEatCount;
    //    yield return new WaitForSeconds(1f);
    //    // �Դ� ��� Ȱ��ȭ �ð�
    //    _isEat = false;
    //}

    void Digestion()
    {
        // a
        if (fishItemCount <= 0 || digestionCount >= digestionMaxCount)
        {
            DigestionTime = 0f;
            return;
        }

        // a(10) / b(10)
        // a
        // �����ִ� ����ġ ������ // max������ ���� �������� �� �ִ� ����ġ �����ۿ� ������ �ش�
        // �̺�Ʈ �ʿ��� ������ ���� ����� ������ ������ �ش�
        // b
        // �������� ���� �� �ִ� ����ġ ������ // ������ �������ε�  ������� ������
        // �޴� ��ư�� ���� ���� ���� ���Ǹ� ���鿡 ���� �� a�� �����ð����� ��ȭ�Ѵ�
        // �̰� ���� max������ ������ ���� ������ �̺�Ʈ ���� ��Ÿ���� �� ��꿡 ���Ե��� �ʴ´�

        DigestionTime += Time.deltaTime;

        if(DigestionTime >= DigestionDelay)
        {
            DigestionTime = 0f;
            // b
            digestionCount++;
            fishItemCount--;
        }

        // ���� �̰� ������� �ǰ� �ִٸ� �̹����� �ð����̳� �Դ� �ִϸ��̼� ������ �ص� �ǰڴ�
    }

    public void ExperienceUp()
    {
        if(_isBreath)
            curExperience += digestionCount * 5;
        else
            curExperience += digestionCount * 3;

        digestionCount = 0;
    }
}