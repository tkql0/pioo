using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCharacter : Character
{
    [SerializeField]
    private GameObject _scanObject;
    [SerializeField]
    private GameObject _detection;
    [SerializeField]
    private GameObject _bait;

    private RaycastHit2D[] _inTarget;
    [SerializeField]
    private Slider _healthSlider;

    private float _RandomBaitPosition;

    public int _weaponCurCount;
    private int _weaponMaxCount;

    private Vector2 _battleSightRange;

    private Vector2 _scanPosition => _scanObject.transform.position;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        targetSpawnNumber = 99;
        characterObject = gameObject;
    }

    private void OnEnable()
    {
        if (_characterData == null)
            return;

        _battleSightRange = new Vector2(_scanObject.transform.localScale.x * 3f,
            _scanObject.transform.localScale.y * 1.5f);

        _weaponMaxCount = GetRandomCount(_Min_Weapon_Count, _Max_Weapon_Count);
        _weaponCurCount = _weaponMaxCount;

        StartCoroutine(MoveDelay(_characterData.MinDelayTime, _characterData.MaxDelayTime));
        SetKey(ObjectType.Enemy);

        isDie = false;
        isDamage = false;
        SetColor(sprite, Color.white);

        curHealth = _characterData.MaxHp;
        _healthSlider.maxValue = _characterData.MaxHp;
        _healthSlider.value = _characterData.MaxHp;

        _RandomBaitPosition = Random.Range(-0.5f, -30f);
    }

    private void OnDestroy()
    {
        targetSpawnNumber = 99;
        isDie = true;
    }

    private void Update()
    {
        _healthSlider.value = curHealth;

        Dead(curHealth);
    }

    private void FixedUpdate()
    {
        ObjectScan(_scanPosition);
    }

    private void HitTracking(Vector2 InTargetPosition)
    {
        float DirX = InTargetPosition.x - _scanPosition.x;

        transform.localScale = DirX <= 0 ? _leftPosition : _rightPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag(Player) ||
            collision.CompareTag(Player_Attack)) && isDie == false)
        {
            isBattle = true;

            Vector2 _player = collision.transform.position;

            HitTracking(_player);

            if (isDamage == false)
            {
                int Critical = Random.Range(1, 5);

                isDamage = true;

                if (Critical == 4)
                    curHealth -= (playerDamage + playerCriticalDamage);
                else
                    curHealth -= playerDamage;

                StartCoroutine(OnDamage(sprite));
            }
        }
    }

    private void ObjectScan(Vector2 InEnemyScan)
    {
        coolTimeMax = 3f;

        if (isBattle)
        {
            _scanObject.transform.localScale = _battleSightRange;

            _inTarget = Physics2D.CircleCastAll(InEnemyScan,
            _characterData.SightRange + 2f, Vector2.zero, 0, targetMask);
        }
        else
            _inTarget = Physics2D.CircleCastAll(InEnemyScan,
            _characterData.SightRange, Vector2.zero, 0, targetMask);

        Transform target = GetNearest();

        if (_inTarget.Length != 0)
        {
            coolTime += Time.deltaTime;
            _detection.SetActive(true);

            if (coolTimeMax < coolTime)
            {
                if (!target)
                    return;

                coolTime = 0f;
                Attack(target.position);
            }
        }
        else
            _detection.SetActive(false);
    }

    private void Attack(Vector2 InTargetPosition)
    {
        if (_weaponCurCount == 0)
        {
            StartCoroutine(ReLoad());
            return;
        }
        Vector2 dir = InTargetPosition - characterPosition;
        dir = dir.normalized;

        GameObject Attack = GameManager.SPAWN.GetObjectSpawn(characterPosition, weaponSpawnKey, ObjectType.EnemyWeapon);
        Attack.GetComponent<Rigidbody2D>().velocity = dir * weaponPower;
        _weaponCurCount--;
    }

    private IEnumerator ReLoad()
    {
        coolTime = 2f;

        float reLoadTime = _weaponMaxCount * coolTime;
        SetColor(sprite, Color.green);

        yield return new WaitForSeconds(reLoadTime);
        _weaponCurCount = _weaponMaxCount;
        SetColor(sprite, Color.white);
        // 색이 겹치면 금방 끝나니까 오브젝트로 표현해야겠다
    }

    private Transform GetNearest()
    {
        Transform target = null;
        float diff = 15;

        foreach (RaycastHit2D targets in _inTarget)
        {
            Vector2 targetPosition = targets.transform.position;

            float curdiff = Vector2.Distance(characterPosition, targetPosition);

            if (curdiff < diff)
            {
                diff = curdiff;
                target = targets.transform;
            }
        }
        return target;
    }

    private const int _Max_Weapon_Count = 5;
    private const int _Min_Weapon_Count = 1;
}