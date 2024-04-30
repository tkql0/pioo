using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCharacter : Character
{
    [SerializeField]
    private GameObject _scanObject;
    [SerializeField]
    private GameObject _bait;

    private Vector2 _scanObjectBaseScale;

    [SerializeField]
    private GameObject _detection;
    [SerializeField]
    private GameObject _reLoad;

    [SerializeField]
    private Slider _healthSlider;

    public int _weaponCurCount;
    private int _weaponMaxCount;

    private Vector2 _battleSightRange;
    private Vector2 _baitPosition;

    [SerializeField]
    private Sprite[] ObjectImg;

    private Vector2 _scanPosition => _scanObject.transform.position;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        targetSpawnNumber = 99;
        characterObject = gameObject;
        
        _scanObjectBaseScale = _scanObject.transform.localScale;
    }

    private void OnEnable()
    {
        if (_characterData == null)
            return;

        _scanObject.transform.localScale = _scanObjectBaseScale;

        _battleSightRange = new Vector2(_scanObject.transform.localScale.x * 2f,
            _scanObject.transform.localScale.y * 1.5f);

        _weaponMaxCount = GetRandomCount(_Min_Weapon_Count, _Max_Weapon_Count);
        _weaponCurCount = _weaponMaxCount;

        StartCoroutine(MoveDelay(_characterData.MinDelayTime, _characterData.MaxDelayTime));
        SetKey(ObjectType.Enemy);

        isDie = false;
        isDamage = false;
        SetColor(spriteRenderer, Color.white);
        spriteRenderer.sprite = ObjectImg[0];

        curHealth = _characterData.MaxHp;
        _healthSlider.maxValue = _characterData.MaxHp;
        _healthSlider.value = _characterData.MaxHp;

        trackingCount = 0;

        //_RandomBaitPosition = Random.Range(-0.5f, -30f);
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

    private IEnumerator ReTracking(Vector2 InTargetPosition)
    {
        HitTracking(InTargetPosition);
        trackingCount++;

        yield return new WaitForSeconds(5f);

        StartCoroutine(ReTracking(InTargetPosition));
        //Debug.Log("ÀçÅ½»ö");

        if (trackingCount >= 10)
        {
            trackingCount = 0;

            StopCoroutine(ReTracking(InTargetPosition));
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out var OutPlayer))
            Damage(OutPlayer.playerDamage, OutPlayer.playerCriticalDamage, OutPlayer.transform.position);
        if (collision.TryGetComponent<Weapon>(out var OutWeapon) && collision.CompareTag(Player_Attack))
            Damage(OutWeapon.damage, OutWeapon.criticalDamage, OutWeapon.transform.position);
    }

    private int trackingCount;

    private void Damage(float InDamage, float InCriticalDamage, Vector2 player)
    {
        if (isDie == true)
            return;

        trackingCount = 0;

        isBattle = true;

        StartCoroutine(ReTracking(player));

        if (isDamage == false)
        {
            int Critical = Random.Range(1, 5);

            isDamage = true;

            if (Critical == 4)
                curHealth -= InDamage + InCriticalDamage;
            else
                curHealth -= InDamage;

            StartCoroutine(OnDamage(spriteRenderer));
        }
    }

    private void ObjectScan(Vector2 InEnemyScan)
    {
        coolTimeMax = 3f;

        if (isBattle)
        {
            _scanObject.transform.localScale = _battleSightRange;

            _inTarget = Physics2D.CircleCastAll(InEnemyScan,
            _characterData.SightRange + 1f, Vector2.zero, 0, targetMask);
            if(spriteRenderer.sprite != ObjectImg[2])
                spriteRenderer.sprite = ObjectImg[1];
        }
        else
            _inTarget = Physics2D.CircleCastAll(InEnemyScan,
            _characterData.SightRange, Vector2.zero, 0, targetMask);

        if (_inTarget.Length != 0)
        {
            Transform target = GetNearest();

            coolTime += Time.deltaTime;
            if(_reLoad.activeSelf == false)
                _detection.SetActive(true);
            else
                _detection.SetActive(false);

            if (coolTimeMax < coolTime)
                Attack(target.position);
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

        coolTime = 0f;
        Vector2 dir = InTargetPosition - characterPosition;
        dir = dir.normalized;

        GameObject Attack = GameManager.SPAWN.GetObjectSpawn(characterPosition, weaponSpawnKey, ObjectType.EnemyWeapon);
        if (!Attack.TryGetComponent<Weapon>(out var OutWeapon))
            return;

        OutWeapon.damage = _characterData.damage;
        OutWeapon.criticalDamage = _characterData.criticalDamage;
        if (isBattle)
            OutWeapon.rigid.velocity = dir * (weaponPower * 1.5f);
        else
            OutWeapon.rigid.velocity = dir * weaponPower;
        _weaponCurCount--;
    }

    private IEnumerator ReLoad()
    {
        coolTime = 2f;

        float reLoadTime = _weaponMaxCount * coolTime;
        spriteRenderer.sprite = ObjectImg[2];

        yield return new WaitForSeconds(reLoadTime);
        _weaponCurCount = _weaponMaxCount;

        if(isBattle)
            spriteRenderer.sprite = ObjectImg[1];
        else
            spriteRenderer.sprite = ObjectImg[0];
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