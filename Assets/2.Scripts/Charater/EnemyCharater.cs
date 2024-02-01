using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCharater : AttackableCharacter
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

    [SerializeField]
    private float max_health;
    private float cur_health;

    [SerializeField]
    private float scanRange = 2;

    private bool isDie;
    private bool isDamage;

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

        isDie = false;
        isDamage = false;
        sprite.color = Color.white;

        cur_health = max_health;
        health_Slider.maxValue = max_health;
        health_Slider.value = max_health;
    }

    private void OnDestroy()
    {
        key = 99;
    }

    private void Update()
    {
        health_Slider.value = cur_health;

        Enemy_Die();
    }

    private void FixedUpdate()
    {
        Search(enemySearch);
    }

    private void Move()
    {
        if (!gameObject.activeSelf)
            return;

        int nextMove = Random.Range(-1, 2);

        if (nextMove != 0)
            sprite.flipX = nextMove < 0;

        if (sprite.flipX == true)
            enemySearch.transform.position = new Vector3(transform.position.x - 2,
                enemySearch.transform.position.y, enemySearch.transform.position.z);
        else
            enemySearch.transform.position = new Vector3(transform.position.x + 2,
                enemySearch.transform.position.y, enemySearch.transform.position.z);

        float speed = Random.Range(0.1f, 5);
        rigid.velocity = new Vector2(nextMove * speed, rigid.velocity.y);
    }

    private IEnumerator MoveDelay()
    {
        Move();
        float next_MoveTime = Random.Range(1, 6f);
        yield return new WaitForSeconds(next_MoveTime);

        StartCoroutine(MoveDelay());
    }
    private void Hit_Tracking(GameObject target)
    {
        Vector3 playerPos = target.transform.position;
        Vector3 myPos = transform.position;

        float DirX = playerPos.x - myPos.x;

        if (DirX != 0)
            sprite.flipX = DirX < 0;

        if (sprite.flipX == true)
            enemySearch.transform.position = new Vector3(transform.position.x - 2,
                enemySearch.transform.position.y, enemySearch.transform.position.z);
        else
            enemySearch.transform.position = new Vector3(transform.position.x + 2,
                enemySearch.transform.position.y, enemySearch.transform.position.z);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.CompareTag(Player) ||
            collision.gameObject.CompareTag(Player_Attack)) && isDie == false)
        {
            GameObject _player = collision.gameObject;

            Hit_Tracking(_player);

            if (!isDamage)
            {
                isDamage = true;
                cur_health = cur_health - Damage;
                StartCoroutine(OnDamage(sprite));

                isDamage = false;
            }
        }
    }

    private void Enemy_Die()
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

        inTarget = Physics2D.CircleCastAll(enemySearch.transform.position, scanRange, Vector2.zero, 0, targetMask);

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

    private void Attack(Vector3 target)
    {
        Vector3 targetPos = target;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        GameObject Attack = GameManager.SPAWN.SpawnEnemyWapon(gameObject);
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
            Vector3 myPos = transform.position;
            Vector3 targetPos = targets.transform.position;

            float curdiff = Vector3.Distance(myPos, targetPos);

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