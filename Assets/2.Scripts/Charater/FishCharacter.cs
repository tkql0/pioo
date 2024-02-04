using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishCharacter : Character
{
    public GameObject fish;

    public int key;
    public ObjectType spawnNumber = ObjectType.NULL;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        fish = gameObject;
        key = 99;

        StartCoroutine(MoveDelay());
    }

    private void OnDestroy()
    {
        key = 99;
    }

    public void Movement()
    {
        if (!gameObject.activeSelf)
            return;
        int nextMove = Random.Range(-1, 2);
        if (nextMove != 0)
            sprite.flipX = nextMove < 0;

        float speed = Random.Range(0.5f, 5f);
        rigid.velocity = new Vector2(nextMove * speed, rigid.velocity.y);
    }

    public IEnumerator MoveDelay()
    {
        Movement();
        float next_MoveTime = Random.Range(1, 3f);
        yield return new WaitForSeconds(next_MoveTime);
        StartCoroutine(MoveDelay());
    }

    public void SetActiveObject(bool InIsActive)
    {
        fish?.SetActive(InIsActive);
    }
}