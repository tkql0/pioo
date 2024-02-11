using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject weaponObject;

    public ObjectType key;
    public long spawnNumberKey;
    public long mySpawnNumber;

    public Rigidbody2D rigid;

    public const string Player = "Player";
    public const string Enemy = "Enemy";

    private void OnEnable()
    {
        weaponObject = gameObject;

        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (key == ObjectType.PlayerWeapon && transform.position.y < 0)
            gameObject.SetActive(false);

        transform.right = rigid.velocity;

        WeaponGravity();
    }

    private void WeaponGravity()
    {
        Vector2 myPos = transform.position;
        Vector2 gravityPoint = new Vector3(0, 0);

        float DirY = myPos.y - gravityPoint.y;
        float diffY = Mathf.Abs(DirY);
        rigid.drag = DirY <= 0 ? 3 : 0.7f;

        if (diffY > 30)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject target = collision.gameObject;

        if (target.CompareTag(Enemy) && key == ObjectType.PlayerWeapon)
            gameObject.SetActive(false);

        else if (target.CompareTag(Player) && key == ObjectType.EnemyWeapon)
            gameObject.SetActive(false);
    }
    public void SetActiveObject(bool InIsActive)
    {
        weaponObject?.SetActive(InIsActive);
    }

    public bool isActive => weaponObject.activeSelf;
}