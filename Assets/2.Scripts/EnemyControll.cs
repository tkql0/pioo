using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControll : MonoBehaviour
{
    GameObject myObj;

    Rigidbody2D rigid;
    SpriteRenderer sprite;

    int nextMove;

    private void Awake()
    {
        myObj = gameObject;

        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (GameTree.Instance.gameManager.GameStart == true)
        {
            switch (myObj.tag)
            {
                case "Enemy":

                    break;
                case "Enemy_exp":

                    break;
                case "Fish_exp":

                    break;
            }
        }
    }

    void PosDisX(float dis)
    {
        Vector3 playerPos = GameTree.Instance.gameManager.playerControll.transform.position;
        Vector3 myPos = myObj.transform.position;

        float DirX = playerPos.x - myPos.x;
        float diffX = Mathf.Abs(DirX);

        DirX = DirX > 0 ? 1 : -1;

        if (diffX > dis)
        {
            transform.Translate(Vector3.right * DirX * 60);
            return;
        }
    }

    void Enemy_Move()
    {
        nextMove = Random.Range(-1, 2);
        if (nextMove != 0)
            sprite.flipX = nextMove < 0;


        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        float next_MoveTime = Random.Range(2f, 5f);
        Invoke("Enemy_Move", next_MoveTime);
    }
}
