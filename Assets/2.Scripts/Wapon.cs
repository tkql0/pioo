using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wapon : MonoBehaviour
{
    public GameObject wapon;

    public int key;

    [SerializeField]
    Rigidbody2D rigid;

    void OnEnable()
    {
        wapon = gameObject;

        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (key == 1 && transform.position.y < 0)
        {
            gameObject.SetActive(false);
        }

        transform.right = rigid.velocity;

        lance_gravity();
    }

    void lance_gravity()
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
        if (collision.gameObject.CompareTag("Enemy") && key == 1)
        {
            gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Player") && key == 2)
            gameObject.SetActive(false);
    }
}