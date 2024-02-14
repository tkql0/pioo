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
        _weaponMaxCount = Random.Range(1, 5);
        _weaponCurCount = _weaponMaxCount;

        StartCoroutine(MoveDelay());
        key = ObjectType.Enemy;

        maxHealth = 20;
        isDie = false;
        isDamage = false;
        sprite.color = Color.white;

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

        Die(_healthSlider);
    }

    private void FixedUpdate()
    {
        ObjectScan(_scanObject);
    }

    //public override void Move()
    //{
    //    base.Move();

    //    //SetScanObjectPosition(nextMove <= 0 ? _leftPosition : _rightPosition);
    //}

    //private void SetScanObjectPosition(Vector2 InPosition)
    //{
    //    if (_scanObject == null)
    //        return;

    //    _scanObject.transform.position = InPosition;
    //}

    private IEnumerator MoveDelay()
    {
        Move();
        float next_MoveTime = Random.Range(1, 6f);
        yield return new WaitForSeconds(next_MoveTime);

        StartCoroutine(MoveDelay());
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

            if (!isDamage)
            {
                isDamage = true;
                curHealth = curHealth - damage;
                StartCoroutine(OnDamage(sprite));
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(_scanObject.transform.position, _scanRange);
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
        float reLoadTime = _weaponMaxCount * 2;
        sprite.color = Color.green;

        yield return new WaitForSeconds(reLoadTime);
        _weaponCurCount = _weaponMaxCount;
        sprite.color = Color.white;
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
}