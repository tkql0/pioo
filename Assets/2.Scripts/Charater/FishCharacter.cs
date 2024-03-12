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
        characterObject = gameObject;
    }

    private void OnEnable()
    {
        if (_characterData == null)
            return;

        SetKey(ObjectType.Fish);
        StartCoroutine(MoveDelay(_characterData.MinDelayTime, _characterData.MaxDelayTime));
    }

    private void OnDestroy()
    {
        targetSpawnNumber = 99;
    }
}