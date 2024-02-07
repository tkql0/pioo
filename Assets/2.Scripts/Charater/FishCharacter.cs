using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishCharacter : Character
{
    //public GameObject fish;

    //public long spawnObjectKey;
    //public ObjectType key;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        spawnObjectKey = 99;
    }

    private void OnEnable()
    {
        characterObject = gameObject;
        key = ObjectType.Fish;

        StartCoroutine(MoveDelay());
    }

    private void OnDestroy()
    {
        spawnObjectKey = 99;
    }

    public IEnumerator MoveDelay()
    {
        Movement();
        float next_MoveTime = Random.Range(1, 3f);
        yield return new WaitForSeconds(next_MoveTime);
        StartCoroutine(MoveDelay());
    }
}