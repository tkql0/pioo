using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject weapon;

    public CharacterType key;

    [SerializeField]
    private Rigidbody2D rigid;

    public const string Player = "Player";
    public const string Enemy = "Enemy";

    private void OnEnable()
    {
        weapon = gameObject;

        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (key == CharacterType.Player && transform.position.y < 0)
            gameObject.SetActive(false);

        transform.right = rigid.velocity;

        weapon_gravity();
    }

    private void weapon_gravity()
    {
        Vector3 myPos = transform.position;
        Vector3 gravityPoint = new Vector3(0, 0, 0);

        float DirY = myPos.y - gravityPoint.y;
        float diffY = Mathf.Abs(DirY);
        rigid.drag = DirY <= 0 ? 3 : 0.2f;

        if (diffY > 30)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject target = collision.gameObject;

        if (target.CompareTag(Enemy) && key == CharacterType.Player)
            gameObject.SetActive(false);

        else if (target.CompareTag(Player) && key == CharacterType.Enemy)
            gameObject.SetActive(false);
    }
    public void SetActiveObject(bool InIsActive)
    {
        weapon?.SetActive(InIsActive);
    }
}