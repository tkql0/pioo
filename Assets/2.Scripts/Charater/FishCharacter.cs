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