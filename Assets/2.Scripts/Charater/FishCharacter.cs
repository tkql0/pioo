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

        StartCoroutine(MoveDelay(Min_DelayTime, Max_DelayTime));
    }

    private void OnDestroy()
    {
        targetSpawnNumber = 99;
    }

    private const float Min_DelayTime = 1f;
    private const float Max_DelayTime = 3f;
}