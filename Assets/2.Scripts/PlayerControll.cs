using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    public Vector2 inputVec;

    Rigidbody2D rigid;
    SpriteRenderer sprite;

    public float move_Maxspeed;

    public GameObject Map;

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

        if(GameTree.Instance.gameManager.player.isDie == false)
            Player_Input();
    }

    private void FixedUpdate()
    {
        if (GameTree.Instance.gameManager.player.isDie == false
            && GameTree.Instance.gameManager.player.isJump == false)
        {
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
        GameTree.Instance.gameManager.GameStart = false;
    }

    public void GameStart()
    {
        if (GameTree.Instance.uiManager.GameStart_Panel.activeSelf == false)
        {
            return;
        }

        GameObject Player = gameObject;

        Instantiate(Player, new Vector3(0, 1, 0), Quaternion.identity);
        for (int i = -1; i <= 1; i++)
        {
            Instantiate(Map, new Vector3(Player.transform.position.x + (i * 20), 0, 0), Quaternion.identity);
        }
        GameTree.Instance.uiManager.GameStart_Panel.SetActive(false);
        GameTree.Instance.gameManager.GameStart = true;
    }

    int jump_count = 0;

    void Player_Input()
    {
        if (GameTree.Instance.gameManager.player.isJump == false)
        {
            inputVec.x = Input.GetAxisRaw("Horizontal");
            inputVec.y = Input.GetAxisRaw("Vertical");
            rigid.gravityScale = 1f;
            jump_count = 0;
            if(Input.GetKey(KeyCode.LeftShift))
                move_Maxspeed = 20;
            else
                move_Maxspeed = 10;
        }
        else if (GameTree.Instance.gameManager.player.isJump == true && jump_count == 0)
        {
            rigid.gravityScale = 3f;
            rigid.AddForce(Vector2.up * rigid.velocity.y, ForceMode2D.Impulse);
            jump_count = 1;
        }
    }
}
