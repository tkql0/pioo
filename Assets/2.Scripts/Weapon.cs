using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject weaponObject;

    public ObjectType key;
    public long spawnNumberKey;
    public long mySpawnNumber;

    public int damage;
    public int criticalDamage;

    public Rigidbody2D rigid;

    private void OnEnable()
    {
        weaponObject = gameObject;

        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (key == ObjectType.PlayerWeapon && weaponPosition.y < 0)
            SetActiveObject(false);

        transform.right = rigid.velocity;

        WeaponGravity();
    }

    private void WeaponGravity()
    {
        Vector2 gravityPoint = new Vector3(0, 0);

        float DirY = weaponPosition.y - gravityPoint.y;
        float diffY = Mathf.Abs(DirY);
        rigid.drag = DirY <= 0 ? 3 : 0.7f;

        if (diffY > 30)
        {
            SetActiveObject(false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject target = collision.gameObject;

        if (collision.TryGetComponent<EnemyCharacter>(out var OutEnemy) && key == ObjectType.PlayerWeapon)
        {


            SetActiveObject(false);
        }
        else if (collision.TryGetComponent<Player>(out var OutPlayer) && key == ObjectType.EnemyWeapon)
        {
            if (OutPlayer.curHealth > 0)
            {
                // 생각하고 있는 컨셉이 플레이어가 보스 몬스터가 되고 마지막엔 꾸미기만 하는 방치형 게임인데
                // 그러면 데미지에 무적을 안줘도 되려나 몬스터만 무적 주고
                // 받는 데미지는 몬스터의 무기밖에 없고 한번 맞으면 없어지니까 괜찮을꺼같은데
                int Critical = Random.Range(1, 5);

                if (Critical == 4)
                    OutPlayer.Damage(damage + criticalDamage);
                else
                    OutPlayer.Damage(damage);
            }

            SetActiveObject(false);
        }
    }

    //private bool TagCheck(GameObject InTargetObject, string InTargetTag)
    //{
    //    return InTargetObject.CompareTag(InTargetTag);
    //}

    public void SetActiveObject(bool InIsActive)
    {
        weaponObject?.SetActive(InIsActive);
    }

    public bool isActive => weaponObject.activeSelf;

    public Vector2 weaponPosition => weaponObject.transform.position;

    public void SetKey(ObjectType InType) => key = InType;
    public void SetSpawnNumber(long InNumber) => mySpawnNumber = InNumber;

    public const string Player = "Player";
    public const string Enemy = "Enemy";
}