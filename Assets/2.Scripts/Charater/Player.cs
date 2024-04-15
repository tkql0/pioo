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

    private Animator _anim;

    private Vector2 _inputVector;

    public float moveSpeed = 0.0f;
    public float moveMaxSpeed = 0.0f;
    public float moveSpeedX = 0.0f;
    public float moveSpeedY = 0.0f;

    private bool _isSwimming;
    private bool _isJump;
    // �÷��̾ ������ �ϴ� ����
    private bool _isBreath;
    // �÷��̾ ���� ���� ����
    private bool _isEat;
    private bool _isCharging = false;

    public bool _isSwimmingTest;
    // ��ư�� �������� Player�� position.y�� 0�� ���� ����
    // �׸��� ��ư�� ������ �ִ� ���� �߷��� 0�ǰ� ������ ���� ����
    // Position.y�� 0�̸� ȣ��������� �ʱ�ȭ��

    // ��ư�� ������ �ʾ����� position.y�� 0�� ������ rigid.velocity��ŭ ������
    // Position.y�� 0�̻��̷��� ȣ��������� �ʱ�ȭ���� �ʰ�
    // �پ�� ȣ�� ��������ŭ ���� ����

    // �ش� ������� ����� �Ǹ����� ����ؼ� ����ϰ� ���� �ʿ����
    // ü���� ��Ʈ ȣ��������� ����� ������� ���� �� ������ ���� ������
    public bool _isMoveChangeTest;
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
    public int fishItemMaxCount = 10;
    public int fishEatCount = 1;

    private float _attackPower = 0.0f;
    private float _chargingPower = 0.0f;
    public float attackMinPower = 0.0f;
    public float attackMaxPower = 6.0f;

    private float _gravityPointY = 0;

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

            _gravityPointY = characterPosition.y - _gravityPoint.y;

            _isBreath = Mathf.Abs(_gravityPointY) < 1 ? true : false;
            _isSwimming = _gravityPointY <= 0 ? true : false;
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
        // �����ӿ� ���� �ִ� �ӵ��� ��� �� ���� Ȯ���� ����
        // �ִ� �ӵ��� �ƴϸ� ȣ��������� �ٴ� �ӵ��� 0.7���� �ִ� �ӵ��� 1
    }

    private void MoveSpeed(float MaxSpeed)
    {
        if (!_isSwimming)
            return;
        // Player�� �� �ٱ������� ���ӵ����� �̵��� �پ������
        // �������� ���� ���ϰ� �Ǿ�����

        moveSpeedX = rigid.velocity.x;
        moveSpeedY = rigid.velocity.y;
        // �����ӿ� ���� ���ӵ��� ����

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
        // Player�� ������

        if (_inputVector.x != 0)
            spriteRenderer.flipX = _inputVector.x > 0;
        // Player�� ���� ��ȯ

        if (_isBreath)
            _anim.SetFloat("MoveUpSpeed", Mathf.Abs(_inputVector.x));
        if(!_isBreath)
            _anim.SetFloat("MoveDownSpeed", Mathf.Abs(_inputVector.x));

        Jump();

        //if (gravityMaxPointY < gravityPointY)
        //{
        //    gravityMaxPointY = gravityPointY + 2;
        //}

        if (_isSwimmingTest && transform.position.y < 0)
        {
            // ���� ��ư�� �����ְ� Player�� y��ǥ�� 0�̸��̶��
            rigid.gravityScale = 0f;
            // �߷��� 0���� �ʱ�ȭ

            if(transform.position.y > 0)
                transform.position = new Vector2(transform.position.x, 0);

            if (curBreath > 0.0f)
                curBreath -= Time.deltaTime;
            else
                curHealth -= Time.deltaTime;
            // ȣ��������� 0�̻��̶�� ȣ������� ����
            // 0 ���϶�� ü�� ����

            _isJump = false;
        }
        else if (_isSwimmingTest && transform.position.y >= 0)
        {
            // ���� ��ư�� �����ְ� Player�� y��ǥ�� 0�̻��϶�
            if (_isJump)
                return;

            transform.position = new Vector2(transform.position.x, 0);
            // ���� ���� �ƴ϶�� Player�� y��ǥ�� 0���� ����

            rigid.gravityScale = 0f;
            // �߷��� 0���� �ʱ�ȭ

            curBreath = maxBreath;
            // ������ ȣ�� �������� �ִ�ġ�� ȸ��

            if (curHealth < maxHealth)
                curHealth += Time.deltaTime;
            // ü���� �������ִٸ� õõ�� ȸ��
        }
        else if(!_isSwimmingTest && transform.position.y < 0)
        {
            // ���� ��ư�� �������� ���� �� Player�� y��ǥ�� 0�̸��̶��
            rigid.gravityScale = 0.1f;
            // �߷��� 0.1�� �ʱ�ȭ

            if (transform.position.y > 0)
                transform.position = new Vector2(transform.position.x, 0);

            if (curBreath > 0.0f)
                curBreath -= Time.deltaTime;
            else
                curHealth -= Time.deltaTime;
            // ȣ��������� 0�̻��̶�� ȣ������� ����
            // 0 ���϶�� ü�� ����

            _isJump = false;
        }
        else if (!_isSwimmingTest && transform.position.y >= 0)
        {
            // ���� ��ư�� �������� ���� �� Player�� y��ǥ�� 0�̻��϶�
            if (_isJump)
                return;

            rigid.gravityScale = gravityMaxPointY;
            rigid.AddForce(Vector2.up * moveSpeedY, ForceMode2D.Impulse);
            _isJump = true;
            // ���� ���� �ƴ϶�� ���� ���ӵ���ŭ ������ ���� ���� true�� ����

            if (curHealth < maxHealth)
                curHealth += Time.deltaTime;
            // ü���� �������ִٸ� õõ�� ȸ��
        }
    }

    private void Jump()
    {
        if (_isSwimming)
        {
            _isJump = false;
        }
        else
        {
            rigid.gravityScale = 5f;

            if (!_isJump)
            {
                rigid.AddForce(Vector2.up * moveSpeedY, ForceMode2D.Impulse);
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
        // �Դ� ��� Ȱ��ȭ �ð�
        _isEat = false;
    }
}