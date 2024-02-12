using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishCharacter : Character
{
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        targetSpawnNumber = 99;
    }

    private void OnEnable()
    {
        characterObject = gameObject;
        key = ObjectType.Fish;

        StartCoroutine(MoveDelay());
    }

    private void OnDestroy()
    {
        targetSpawnNumber = 99;
    }

    public IEnumerator MoveDelay()
    {
        Move();
        float next_MoveTime = Random.Range(1, 3f);
        yield return new WaitForSeconds(next_MoveTime);
        StartCoroutine(MoveDelay());
    }
}