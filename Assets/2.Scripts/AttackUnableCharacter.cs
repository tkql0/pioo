using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackUnableCharacter : ObjectController
{
    [SerializeField]
    Rigidbody2D rigid;
    [SerializeField]
    SpriteRenderer sprite;

    public int Experience = 0;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        StartCoroutine(MoveDelay());
    }

    IEnumerator MoveDelay()
    {
        ObjectMove(rigid, sprite);
        float next_MoveTime = Random.Range(1, 6f);
        yield return new WaitForSeconds(next_MoveTime);
        StartCoroutine(MoveDelay());
    }
}