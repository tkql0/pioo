using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;

    Rigidbody2D rigid;
    SpriteRenderer sprite;

    public GameObject Map;

    float maxSpeed;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        GameTree.Instance.gameManager.playerControll.move_Maxspeed = maxSpeed;

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

            if (rigid.velocity.x > maxSpeed)
                rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
            else if (rigid.velocity.x < maxSpeed * (-1))
                rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);

            if (rigid.velocity.y > maxSpeed)
                rigid.velocity = new Vector2(rigid.velocity.x, maxSpeed);
            else if (rigid.velocity.y < maxSpeed * (-1))
                rigid.velocity = new Vector2(rigid.velocity.x, maxSpeed * (-1));
        }
    }

    void LateUpdate()
    {
        if (inputVec.x != 0)
            sprite.flipX = inputVec.x > 0;

        GameTree.Instance.gameManager.cameraControll.speed = maxSpeed;
    }

    void OnEnable()
    {
        maxSpeed = 10;
        GameTree.Instance.gameManager.GameStart = false;
    }

    public void GameStart()
    {
        if (GameTree.Instance.uiManager.GameStart_Panel.activeSelf == false)
        {
            return;
        }

        GameObject PlayObj;

        PlayObj = Instantiate(gameObject, new Vector3(0, 1, 0), Quaternion.identity);
        GameTree.Instance.gameManager.cameraControll.player = PlayObj;
        for (int i = -1; i <= 1; i++)
        {
            Instantiate(Map, new Vector3(transform.position.x + (i * 20), 0, 0), Quaternion.identity);
        }
        GameTree.Instance.uiManager.GameStart_Panel.SetActive(false);
        GameTree.Instance.gameManager.GameStart = true;
    }

    int jump_count = 0;

    void Player_Input()
    {
        if (GameTree.Instance.gameManager.playerControll.isJump == false)
        {
            inputVec.x = Input.GetAxisRaw("Horizontal");
            inputVec.y = Input.GetAxisRaw("Vertical");
            rigid.gravityScale = 1f;
            jump_count = 0;
            if (Input.GetKey(KeyCode.LeftShift))
                maxSpeed = 20;
            else
                maxSpeed = 10;
        }
        else if (GameTree.Instance.gameManager.playerControll.isJump == true && jump_count == 0)
        {
            rigid.gravityScale = 3f;
            rigid.AddForce(Vector2.up * rigid.velocity.y, ForceMode2D.Impulse);
            jump_count = 1;
        }
    }
}
