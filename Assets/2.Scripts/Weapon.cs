using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject weapon;

    public int key;

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
        if (key == 1 && transform.position.y < 0)
        {
            gameObject.SetActive(false);
        }

        transform.right = rigid.velocity;

        lance_gravity();
    }

    private void lance_gravity()
    {
        Vector3 myPos = transform.position;
        Vector3 gravityPoint = new Vector3(0, 0, 0);

        float DirY = myPos.y - gravityPoint.y;
        float diffY = Mathf.Abs(DirY);
        rigid.drag = DirY <= 0 ? 3 : 1;

        if (diffY > 30)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Enemy) || collision.gameObject.CompareTag(Player))
            gameObject.SetActive(false);
    }
}