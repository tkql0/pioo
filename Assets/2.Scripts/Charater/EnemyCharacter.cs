using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCharacter : Character
{
    public GameObject enemy;

    [SerializeField]
    private GameObject enemySearch;
    [SerializeField]
    private GameObject Danger;

    private RaycastHit2D[] inTarget;

    [SerializeField]
    private Slider health_Slider;

    public int key;
    public ObjectType spawnNumber = ObjectType.NULL;

    [SerializeField]
    private float scanRange = 2;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        key = 99;
        enemy = gameObject;
    }

    private void OnEnable()
    {
        StartCoroutine(MoveDelay());

        maxHealth = 20;
        isDie = false;
        isDamage = false;
        sprite.color = Color.white;

        curHealth = maxHealth;
        health_Slider.maxValue = maxHealth;
        health_Slider.value = maxHealth;
    }

    private void OnDestroy()
    {
        key = 99;
    }

    private void Update()
    {
        health_Slider.value = curHealth;

        //Enemy_Die();
    }

    private void FixedUpdate()
    {
        Search(enemySearch);
    }

    public override void Movement()
    {
        base.Movement();

        if (sprite.flipX == true)
            enemySearch.transform.position = new Vector2(transform.position.x - 2,
                enemySearch.transform.position.y);
        else
            enemySearch.transform.position = new Vector2(transform.position.x + 2,
                enemySearch.transform.position.y);
    }

    private IEnumerator MoveDelay()
    {
        Movement();
        float next_MoveTime = Random.Range(1, 6f);
        yield return new WaitForSeconds(next_MoveTime);

        StartCoroutine(MoveDelay());
    }
    private void Hit_Tracking(Vector2 target)
    {
        Vector2 myPos = transform.position;

        float DirX = target.x - myPos.x;

        if (DirX != 0)
            sprite.flipX = DirX < 0;

        if (sprite.flipX == true)
            enemySearch.transform.position = new Vector2(transform.position.x - 2,
                enemySearch.transform.position.y);
        else
            enemySearch.transform.position = new Vector2(transform.position.x + 2,
                enemySearch.transform.position.y);

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

    public void Enemy_Die()
    {
        if (health_Slider.value <= 0)
        {
            isDie = true;

            gameObject.SetActive(false);
            return;
        }
        else
            isDie = false;
    }

    private void Search(GameObject enemySearch)
    {
        Rate_Of_Fire = 3f;

        inTarget = Physics2D.CircleCastAll(enemySearch.transform.position,
            scanRange, Vector2.zero, 0, targetMask);

        Transform target = GetNearest();

        if (inTarget.Length != 0)
        {
            CoolDown_Time += Time.deltaTime;
            Danger.SetActive(true);

            if (Rate_Of_Fire < CoolDown_Time)
            {
                if (!target)
                    return;

                CoolDown_Time = 0f;
                Attack(target.position);
            }
        }
        else
            Danger.SetActive(false);
    }

    private void Attack(Vector2 targetPos)
    {
        Vector2 dir = targetPos - (Vector2)transform.position;
        dir = dir.normalized;

        GameObject Attack = GameManager.SPAWN.SpawnEnemyWapon(transform.position);
        Attack.transform.position = transform.position;
        Attack.transform.rotation = transform.rotation;
        Attack.GetComponent<Rigidbody2D>().velocity = dir * 10;
    }

    private Transform GetNearest()
    {
        Transform target = null;
        float diff = 15;

        foreach (RaycastHit2D targets in inTarget)
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

    public void SetActiveObject(bool InIsActive)
    {
        enemy?.SetActive(InIsActive);
    }
}