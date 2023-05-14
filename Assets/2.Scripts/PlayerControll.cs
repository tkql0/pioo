using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    public void GameStart()
    {
        if (GameTree.Instance.uiManager.GameStart_Panel.activeSelf == true)
        {
            GameObject Player = gameObject;

            Instantiate(Player, new Vector3(0, 0, 0), Quaternion.identity);
            GameTree.Instance.uiManager.GameStart_Panel.SetActive(false);
        }
    }

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (GameTree.Instance.gameManager.player.isDie == true)
        {
            GameTree.Instance.uiManager.GameOver_Panel.SetActive(true);
            Time.timeScale = 0;
        }

        Player_Input();
    }

    private void FixedUpdate()
    {
        if (GameTree.Instance.gameManager.player.isDie == false
            && GameTree.Instance.gameManager.player.isJump == false)
        {
            rigid.gravityScale = 0.2f;
            rigid.AddForce(inputVec.normalized, ForceMode2D.Impulse);

            if (rigid.velocity.x > move_Maxspeed)
                rigid.velocity = new Vector2(move_Maxspeed, rigid.velocity.y);
            else if (rigid.velocity.x < move_Maxspeed * (-1))
                rigid.velocity = new Vector2(move_Maxspeed * (-1), rigid.velocity.y);

            if (rigid.velocity.y > move_Maxspeed)
                rigid.velocity = new Vector2(rigid.velocity.x, move_Maxspeed);
            else if (rigid.velocity.y < move_Maxspeed * (-1))
                rigid.velocity = new Vector2(rigid.velocity.x, move_Maxspeed * (-1));
        }
    }

    void LateUpdate()
    {
        if (inputVec.x != 0)
            sprite.flipX = inputVec.x > 0;
    }

    void OnEnable()
    {
        move_Maxspeed = 10;
        GameTree.Instance.gameManager.player.isDie = false;
        GameTree.Instance.gameManager.player.isJump = false;
        GameTree.Instance.gameManager.player.isDamage = false;
        GameTree.Instance.gameManager.player.isLvUp = false;
    }

    public Vector2 inputVec;

    Rigidbody2D rigid;
    SpriteRenderer sprite;

    public float move_Maxspeed;

    void Player_Input()
    {
        if (GameTree.Instance.gameManager.player.isDie == false
            && GameTree.Instance.gameManager.player.isJump == false)
        {
            inputVec.x = Input.GetAxisRaw("Horizontal");
            inputVec.y = Input.GetAxisRaw("Vertical");

        }

        if (GameTree.Instance.gameManager.player.isJump == true)
        {
            rigid.gravityScale = 5f;

            if (GameTree.Instance.gameManager.player.isJump == false)
            {
                rigid.AddForce(Vector2.up * rigid.velocity.y, ForceMode2D.Impulse);
                GameTree.Instance.gameManager.player.isJump = true;
            }
        }
    }
}
