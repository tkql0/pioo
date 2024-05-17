using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public Rigidbody2D rigid;
    public SpriteRenderer sprite;

    public ObjectType key;
    //public long targetSpawnNumber;
    // 플레이어, 몬스터, 자연스폰
    //public long mySpawnNumber;
    public GameObject itemObject;

    private float itemWeight;
    // 아이템의 무게

    [SerializeField]
    private Image[] _itemImages;
    // 활성화시 변경될 오브젝트의 이미지들

    //private bool isHeavy = false;
    // 무거우면 가라앉기
    // 스크립터블에 무게 추가하기

    public float spawnTime;
    // 아이템마다 존재 할 수 있는 시간 존재
    // 일단 물고기 밖에 없으니까 10으로 초기화

    public float spawnTimeMax;

    private Vector2 _gravityPoint = new Vector2(0, 0);

    private void OnEnable()
    {
        key = ObjectType.Item_Fish;

        rigid = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        itemObject = gameObject;

        spawnTime = 0f;
        spawnTimeMax = 10f;
        Jump();
        // 조건을 달성할 경우 나올경우 점프
        // 자연 생성이면 처음부터 중력 0으로 변경
    }


    private void OnDisable()
    {
        // 아이템이 비활성화 되었다면 아이템 정보 초기화되도록

        // 아이템의 무게에 따라 떠오르거나 가라앉게는 할거지만 공격하거나 할 건 아니니까
        // 활성화 될때 이미지를 바꾸는걸로 하자
    }

    private void Update()
    {
        // itemWeight가 false면 수면으로 떠오르기
        // 아니면 가라앉기
        bool isJump = itemObject.transform.position.y > _gravityPoint.y ? true : false;

        if (isJump)
        {
            rigid.gravityScale = 0.6f;
        }
        else
        {
            rigid.gravityScale = 0f;

            if (Mathf.Abs(rigid.velocity.y) < 0.5f)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, 0), 0.03f);
            }
        }

        spawnTime += Time.deltaTime;

        if (spawnTimeMax <= spawnTime)
            gameObject.SetActive(false);
        // 아이템마다 존재할 수 있는 시간을 두고 그 시간보다 더 오래 존재한다면 비화성화
    }

    public bool isActive => itemObject.activeSelf;
    public Vector2 itemPosition => itemObject.transform.position;

    public void SetActiveObject(bool InIsActive)
    {
        itemObject?.SetActive(InIsActive);
    }

    void Jump()
    {
        float randomJump = Random.Range(4f, 8f);
        Vector2 jumpPower = Vector2.up * randomJump;
        jumpPower.x = Random.Range(-2f, 2f);

        rigid.AddForce(jumpPower, ForceMode2D.Impulse);
        // 생성 이후 몇초 동안 하얀 테두리가 생기며 못먹는 상태로 변함
        // 하얀 테두리가 사라지면 먹을 수 있음

        // 여기서 나온 아이템은 몬스터도 먹을 수 있음
    }

    //public void SetKey(ItemType InType) => key = InType;
    //public void SetSpawnNumber(long InNumber) => mySpawnNumber = InNumber;
}
