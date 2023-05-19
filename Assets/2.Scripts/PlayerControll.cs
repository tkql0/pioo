using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    public Vector2 inputVec;

    Rigidbody2D rigid;
    SpriteRenderer sprite;

    public bool isDie;
    public bool isJump;
    public bool isDamage;
    public bool isLvUp;

    public int melee_damage = 5;
    public int ranged_damage = 5;

    int jump_count = 0;

    public float move_Maxspeed = 0;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        GameTree.Instance.gameManager.playerControll = this;
    }

    private void Update()
    {
        if (GameTree.Instance.gameManager.playerControll.isDie == true)
        {
            GameTree.Instance.uiManager.GameOver_Panel.SetActive(true);
            Time.timeScale = 0;
        }

        if (GameTree.Instance.gameManager.playerControll.isDie == false)
            Player_Input();
    }

    private void FixedUpdate()
    {
        if (GameTree.Instance.gameManager.playerControll.isDie == false
            && GameTree.Instance.gameManager.playerControll.isJump == false)
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

        GameTree.Instance.gameManager.cameraControll.speed = move_Maxspeed;
    }

    void OnEnable()
    {
        move_Maxspeed = 10;
        GameTree.Instance.gameManager.GameStart = false;
        isDie = false;
        isJump = false;
        isDamage = false;
        isLvUp = false;
    }

    int jump_power = 1;
    // 올릴수록 점프를 높이하도록 하기

    void Player_Input()
    {
        if (GameTree.Instance.gameManager.playerControll.isJump == false)
        {
            inputVec.x = Input.GetAxisRaw("Horizontal");
            inputVec.y = Input.GetAxisRaw("Vertical");
            rigid.gravityScale = 0.2f;
            jump_count = 0;
            if (Input.GetKey(KeyCode.LeftShift))
                move_Maxspeed = 20;
            else
                move_Maxspeed = 10;
        }
        else if (GameTree.Instance.gameManager.playerControll.isJump == true && jump_count == 0)
        {
            rigid.gravityScale = 3f;
            rigid.AddForce(Vector2.up * rigid.velocity.y * jump_power, ForceMode2D.Impulse);
            jump_count = 1;
        }
    }
}
