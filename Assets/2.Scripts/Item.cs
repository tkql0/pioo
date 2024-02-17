using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Rigidbody2D rigid;
    public SpriteRenderer sprite;

    public ItemType key;
    public long targetSpawnNumber;
    // 플레이어, 몬스터, 자연스폰
    public long mySpawnNumber;
    public GameObject itemObject;

    public float spawnTime;

    public bool isActive => itemObject.activeSelf;
    public Vector2 itemPosition => itemObject.transform.position;

    public void SetActiveObject(bool InIsActive)
    {
        itemObject?.SetActive(InIsActive);
    }

    public void SetKey(ItemType InType) => key = InType;
    public void SetSpawnNumber(long InNumber) => mySpawnNumber = InNumber;
}
