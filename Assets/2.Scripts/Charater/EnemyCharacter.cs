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
    private float scanRange = 2;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        spawnObjectKey = 99;
        characterObject = gameObject;
    }

    private void OnEnable()
    {
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
        spawnObjectKey = 99;
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

    public override void Movement()
    {
        base.Movement();

        if (sprite.flipX == true)
            _scanObject.transform.position = new Vector2(transform.position.x - 2,
                _scanObject.transform.position.y);
        else
            _scanObject.transform.position = new Vector2(transform.position.x + 2,
                _scanObject.transform.position.y);
    }

    private IEnumerator MoveDelay()
    {
        Movement();
        float next_MoveTime = Random.Range(1, 6f);
        yield return new WaitForSeconds(next_MoveTime);

        StartCoroutine(MoveDelay());
    }
    private void Hit_Tracking(Vector2 targetPosition)
    {
        Vector2 myPos = transform.position;

        float DirX = targetPosition.x - myPos.x;

        if (DirX != 0)
            sprite.flipX = DirX < 0;

        if (sprite.flipX == true)
            _scanObject.transform.position = new Vector2(transform.position.x - 2,
                _scanObject.transform.position.y);
        else
            _scanObject.transform.position = new Vector2(transform.position.x + 2,
                _scanObject.transform.position.y);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.CompareTag(Player) ||
            collision.CompareTag(Player_Attack)) && isDie == false)
        {
            Vector2 _player = collision.transform.position;

            Hit_Tracking(_player);

            if (!isDamage)
            {
                isDamage = true;
                curHealth = curHealth - Damage;
                StartCoroutine(OnDamage(sprite));
            }
        }
    }

    private void ObjectScan(GameObject enemySearch)
    {
        coolTimeMax = 3f;

        _inTarget = Physics2D.CircleCastAll(enemySearch.transform.position,
            scanRange, Vector2.zero, 0, targetMask);

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

    private void Attack(Vector2 targetPosition)
    {
        SpawnController _spawnController = GameManager.SPAWN;

        Vector2 dir = targetPosition - (Vector2)transform.position;
        dir = dir.normalized;

        GameObject Attack = _spawnController.SpawnWeapon(transform.position, ObjectType.EnemyWeapon);
        Attack.transform.position = transform.position;
        Attack.transform.rotation = transform.rotation;
        Attack.GetComponent<Rigidbody2D>().velocity = dir * 10;
    }

    private Transform GetNearest()
    {
        Transform target = null;
        float diff = 15;

        foreach (RaycastHit2D targets in _inTarget)
        {
            Vector2 myPos = transform.position;
            Vector2 targetPos = targets.transform.position;

            float curdiff = Vector2.Distance(myPos, targetPos);

            if (curdiff < diff)
            {
                diff = curdiff;
                target = targets.transform;
            }
        }
        return target;
    }
}