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
    // �÷��̾�, ����, �ڿ�����
    //public long mySpawnNumber;
    public GameObject itemObject;

    private float itemWeight;
    // �������� ����

    [SerializeField]
    private Image[] _itemImages;
    // Ȱ��ȭ�� ����� ������Ʈ�� �̹�����

    //private bool isHeavy = false;
    // ���ſ�� ����ɱ�
    // ��ũ���ͺ� ���� �߰��ϱ�

    public float spawnTime;
    // �����۸��� ���� �� �� �ִ� �ð� ����
    // �ϴ� ����� �ۿ� �����ϱ� 10���� �ʱ�ȭ

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
        // ������ �޼��� ��� ���ð�� ����
        // �ڿ� �����̸� ó������ �߷� 0���� ����
    }


    private void OnDisable()
    {
        // �������� ��Ȱ��ȭ �Ǿ��ٸ� ������ ���� �ʱ�ȭ�ǵ���

        // �������� ���Կ� ���� �������ų� ����ɰԴ� �Ұ����� �����ϰų� �� �� �ƴϴϱ�
        // Ȱ��ȭ �ɶ� �̹����� �ٲٴ°ɷ� ����
    }

    private void Update()
    {
        // itemWeight�� false�� �������� ��������
        // �ƴϸ� ����ɱ�
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
        // �����۸��� ������ �� �ִ� �ð��� �ΰ� �� �ð����� �� ���� �����Ѵٸ� ��ȭ��ȭ
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
        // ���� ���� ���� ���� �Ͼ� �׵θ��� ����� ���Դ� ���·� ����
        // �Ͼ� �׵θ��� ������� ���� �� ����

        // ���⼭ ���� �������� ���͵� ���� �� ����
    }

    //public void SetKey(ItemType InType) => key = InType;
    //public void SetSpawnNumber(long InNumber) => mySpawnNumber = InNumber;
}
