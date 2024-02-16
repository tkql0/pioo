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

    private RaycastHit2D[] _inTarget;
    [SerializeField]
    private Slider _healthSlider;

    [SerializeField]
    private float _scanRange = 0.8f;

    public int _weaponCurCount;
    private int _weaponMaxCount;

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
        _weaponMaxCount = GetRandomCount(_Min_Weapon_Count, _Max_Weapon_Count);
        _weaponCurCount = _weaponMaxCount;

        StartCoroutine(MoveDelay(_Min_DelayTime, _Max_DelayTime));
        SetKey(ObjectType.Enemy);

        maxHealth = 20;
        isDie = false;
        isDamage = false;
        SetColor(sprite, Color.white);

        curHealth = maxHealth;
        _healthSlider.maxValue = maxHealth;
        _healthSlider.value = maxHealth;
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
        ObjectScan(_scanObject);
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
            Vector2 _player = collision.transform.position;

            HitTracking(_player);

            if (isDamage == false)
            {
                isDamage = true;
                curHealth -= damage;
                StartCoroutine(OnDamage(sprite));
            }
        }
    }

    private void ObjectScan(GameObject InEnemyScan)
    {
        coolTimeMax = 3f;

        _inTarget = Physics2D.CircleCastAll(InEnemyScan.transform.position,
            _scanRange, Vector2.zero, 0, targetMask);

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

    private const float _Max_DelayTime = 6f;
    private const float _Min_DelayTime = 1f;

    private const int _Max_Weapon_Count = 5;
    private const int _Min_Weapon_Count = 1;
}